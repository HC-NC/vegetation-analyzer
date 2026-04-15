using System.Drawing;
using vegetation_analyzer.DataClasses;

namespace vegetation_analyzer.Forms
{
    public partial class ClassifyForm : Form
    {
        private readonly IndexRaster _indexRaster;
        private ClassificationScheme _currentScheme;
        private Bitmap? _previewBitmap;

        public ClassifyForm(IndexRaster indexRaster)
        {
            InitializeComponent();

            _indexRaster = indexRaster;
        }

        private void ClassifyForm_Load(object sender, EventArgs e)
        {
            PopulatePresets();
        }

        private void PopulatePresets()
        {
            var presets = ClassificationPresets.GetPresetsForIndex(_indexRaster.IndexType);
            presetComboBox.Items.Clear();

            foreach (var preset in presets)
                presetComboBox.Items.Add(preset.Name);

            if (presets.Count > 0)
                presetComboBox.SelectedIndex = 0;
            else
                MessageBox.Show(this, "Нет пресетов для данного индекса. Создайте пользовательскую схему.",
                    "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        private void presetComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (presetComboBox.SelectedItem is not string presetName) return;

            var presets = ClassificationPresets.GetPresetsForIndex(_indexRaster.IndexType);
            _currentScheme = presets.FirstOrDefault(p => p.Name == presetName)?.Clone()
                ?? ClassificationPresets.GetDefaultForIndex(_indexRaster.IndexType)?.Clone();

            if (_currentScheme != null)
                UpdateClassTable();
        }

        private void UpdateClassTable()
        {
            if (_currentScheme == null) return;

            classTable.Rows.Clear();
            foreach (var cls in _currentScheme.Classes)
            {
                int idx = classTable.Rows.Add();
                var row = classTable.Rows[idx];
                // Создаём 16x16 изображение цвета
                Bitmap colorSwatch = new Bitmap(16, 16);
                using (Graphics g = Graphics.FromImage(colorSwatch))
                {
                    g.Clear(cls.Color);
                }
                row.Cells["colColor"].Value = colorSwatch;
                row.Cells["colClassName"].Value = cls.Name;
                row.Cells["colMin"].Value = cls.Min;
                row.Cells["colMax"].Value = cls.Max;
                row.Tag = cls;
            }
        }

        private void classTable_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == colColor.Index && e.RowIndex >= 0)
            {
                var row = classTable.Rows[e.RowIndex];
                if (row.Tag is ClassificationClass classificationClass)
                {
                    using (ColorDialog dlg = new ColorDialog { Color = classificationClass.Color })
                    {
                        if (dlg.ShowDialog() == DialogResult.OK)
                        {
                            Bitmap colorSwatch = new Bitmap(16, 16);
                            using (Graphics g = Graphics.FromImage(colorSwatch))
                            {
                                g.Clear(dlg.Color);
                            }
                            row.Cells["colColor"].Value = colorSwatch;
                            classificationClass.Color = dlg.Color;
                        }
                    }
                }
            }
        }

        private void classTable_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.RowIndex >= classTable.Rows.Count) return;

            var row = classTable.Rows[e.RowIndex];
            if (row.Tag is ClassificationClass cls)
            {
                if (row.Cells["colClassName"].Value is string name) cls.Name = name;
                if (row.Cells["colMin"].Value is float min) cls.Min = min;
                if (row.Cells["colMax"].Value is float max) cls.Max = max;
            }
        }

        private void addClassButton_Click(object sender, EventArgs e)
        {
            if (_currentScheme == null) return;

            var newCls = new ClassificationClass("New Class", 0, 1, Color.Gray);
            _currentScheme.Classes.Add(newCls);
            UpdateClassTable();
        }

        private void removeClassButton_Click(object sender, EventArgs e)
        {
            if (classTable.SelectedRows.Count == 0 || _currentScheme == null) return;

            int idx = classTable.SelectedRows[0].Index;
            if (idx < _currentScheme.Classes.Count)
                _currentScheme.Classes.RemoveAt(idx);

            UpdateClassTable();
        }

        private void moveUpButton_Click(object sender, EventArgs e)
        {
            if (classTable.SelectedRows.Count == 0 || _currentScheme == null) return;

            int idx = classTable.SelectedRows[0].Index;
            if (idx > 0)
            {
                var item = _currentScheme.Classes[idx];
                _currentScheme.Classes.RemoveAt(idx);
                _currentScheme.Classes.Insert(idx - 1, item);
                UpdateClassTable();
                classTable.Rows[idx - 1].Selected = true;
            }
        }

        private void moveDownButton_Click(object sender, EventArgs e)
        {
            if (classTable.SelectedRows.Count == 0 || _currentScheme == null) return;

            int idx = classTable.SelectedRows[0].Index;
            if (idx < _currentScheme.Classes.Count - 1)
            {
                var item = _currentScheme.Classes[idx];
                _currentScheme.Classes.RemoveAt(idx);
                _currentScheme.Classes.Insert(idx + 1, item);
                UpdateClassTable();
                classTable.Rows[idx + 1].Selected = true;
            }
        }

        private void previewButton_Click(object sender, EventArgs e)
        {
            if (_currentScheme == null) return;

            previewPictureBox.Image?.Dispose();
            var classified = ClassifiedRaster.Classify(_indexRaster, _currentScheme);
            _previewBitmap = classified.GetBitmap();
            previewPictureBox.Image = new Bitmap(_previewBitmap);
            classified.Dispose();
        }

        private void applyButton_Click(object sender, EventArgs e)
        {
            if (_currentScheme == null || _currentScheme.Classes.Count == 0)
            {
                MessageBox.Show(this, "Не определено ни одного класса.", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                DialogResult = DialogResult.None;
                return;
            }

            DialogResult = DialogResult.OK;
        }

        public ClassificationScheme GetScheme() => _currentScheme!;
    }
}
