using System.Drawing;
using vegetation_analyzer.DataClasses;
using vegetation_analyzer.Properties;

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
            presetComboBox.Items.Clear();

            // Встроенные пресеты
            var builtinPresets = ClassificationPresets.GetDefaultPresets()
                .Where(p => p.IndexType == _indexRaster.IndexType).ToList();

            foreach (var preset in builtinPresets)
                presetComboBox.Items.Add(preset.Name);

            // Пользовательские пресеты (с префиксом)
            var customPresets = ClassificationPresets.LoadCustomPresets()
                .Where(p => p.IndexType == _indexRaster.IndexType).ToList();

            if (customPresets.Count > 0)
                presetComboBox.Items.Add("---"); // Разделитель

            foreach (var preset in customPresets)
                presetComboBox.Items.Add($"★ {preset.Name}");

            if (builtinPresets.Count + customPresets.Count == 0)
                presetComboBox.SelectedIndex = -1;
            else 
                presetComboBox.SelectedIndex = 0;
        }

        private void presetComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (presetComboBox.SelectedItem is not string presetName) return;

            // Пропускаем разделитель
            if (presetName == "---") return;

            // Убираем префикс ★ для пользовательских
            string cleanName = presetName.StartsWith("★ ") ? presetName.Substring(2) : presetName;

            var presets = ClassificationPresets.GetPresetsForIndex(_indexRaster.IndexType);
            _currentScheme = presets.FirstOrDefault(p => p.Name == cleanName)?.Clone()
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
                row.Cells["colClassName"].ToolTipText = cls.Description;
                row.Cells["colMin"].Value = cls.Min;
                row.Cells["colMax"].Value = cls.Max;
                row.Tag = cls;
            }
        }

        private void createEmptyButton_Click(object sender, EventArgs e)
        {
            using (var inputForm = new Form
            {
                Text = Resources.NewPreset,
                FormBorderStyle = FormBorderStyle.FixedDialog,
                StartPosition = FormStartPosition.CenterParent,
                Size = new Size(350, 130),
                MaximizeBox = false,
                MinimizeBox = false
            })
            {
                var label = new Label { Text = Resources.PresetName, Location = new Point(14, 15), AutoSize = true };
                var textBox = new TextBox { Location = new Point(14, 40), Width = 300 };
                var okBtn = new Button { Text = Resources.Create, DialogResult = DialogResult.OK, Location = new Point(170, 70) };
                var cancelBtn = new Button { Text = Resources.Cancel, DialogResult = DialogResult.Cancel, Location = new Point(250, 70) };

                inputForm.Controls.AddRange(new Control[] { label, textBox, okBtn, cancelBtn });
                inputForm.AcceptButton = okBtn;
                inputForm.CancelButton = cancelBtn;

                if (inputForm.ShowDialog() == DialogResult.OK)
                {
                    string name = textBox.Text.Trim();
                    if (string.IsNullOrEmpty(name))
                    {
                        MessageBox.Show(this, Resources.ErrorName, Resources.Error,
                            MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    var existing = ClassificationPresets.LoadCustomPresets()
                        .FirstOrDefault(p => p.Name == name);
                    if (existing != null)
                    {
                        MessageBox.Show(this, string.Format(Resources.ErrorPresetExist, name), Resources.Error,
                            MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    // Создаём пустую схему
                    _currentScheme = new ClassificationScheme
                    {
                        Name = name,
                        IndexType = _indexRaster.IndexType,
                        Region = "Custom",
                        Classes = new List<ClassificationClass>(),
                        IsCustom = true
                    };

                    ClassificationPresets.SaveCustomPreset(_currentScheme);
                    PopulatePresets();
                    presetComboBox.SelectedItem = $"★ {name}";
                    UpdateClassTable();
                }
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

                // Значения могут прийти как double, decimal или float
                if (row.Cells["colMin"].Value != null)
                    cls.Min = Convert.ToSingle(row.Cells["colMin"].Value);
                if (row.Cells["colMax"].Value != null)
                    cls.Max = Convert.ToSingle(row.Cells["colMax"].Value);
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

        private void savePresetButton_Click(object sender, EventArgs e)
        {
            if (_currentScheme == null || _currentScheme.Classes.Count == 0)
            {
                MessageBox.Show(this, Resources.ErrorEmptyScheme, Resources.Error,
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            using (var inputForm = new Form
            {
                Text = Resources.SavePreset,
                FormBorderStyle = FormBorderStyle.FixedDialog,
                StartPosition = FormStartPosition.CenterParent,
                Size = new Size(350, 130),
                MaximizeBox = false,
                MinimizeBox = false,
                AcceptButton = null,
                CancelButton = null
            })
            {
                var label = new Label { Text = Resources.PresetName, Location = new Point(14, 15), AutoSize = true };
                var textBox = new TextBox { Location = new Point(14, 40), Width = 300, Text = _currentScheme.Name };
                var saveBtn = new Button { Text = Resources.Save, DialogResult = DialogResult.OK, Location = new Point(170, 70) };
                var cancelBtn = new Button { Text = Resources.Cancel, DialogResult = DialogResult.Cancel, Location = new Point(250, 70) };

                inputForm.Controls.AddRange(new Control[] { label, textBox, saveBtn, cancelBtn });
                inputForm.AcceptButton = saveBtn;
                inputForm.CancelButton = cancelBtn;

                if (inputForm.ShowDialog() == DialogResult.OK)
                {
                    string newName = textBox.Text.Trim();
                    if (string.IsNullOrEmpty(newName))
                    {
                        MessageBox.Show(this, Resources.ErrorEmptyPresetName, Resources.Error,
                            MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    // Проверяем на дубликат
                    var existing = ClassificationPresets.LoadCustomPresets()
                        .FirstOrDefault(p => p.Name == newName);
                    if (existing != null && !_currentScheme.IsCustom)
                    {
                        var result = MessageBox.Show(this, string.Format(Resources.QReplacePresetName, newName),
                            Resources.Confirmation, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                        if (result != DialogResult.Yes) return;

                        ClassificationPresets.DeleteCustomPreset(newName);
                    }

                    _currentScheme.Name = newName;
                    _currentScheme.IsCustom = true;
                    ClassificationPresets.SaveCustomPreset(_currentScheme);
                    PopulatePresets();

                    // Выбираем только что сохранённый пресет
                    presetComboBox.SelectedItem = $"★ {newName}";

                    MessageBox.Show(this, string.Format(Resources.PresetSaved, newName), Resources.Ready,
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private void applyButton_Click(object sender, EventArgs e)
        {
            if (_currentScheme == null || _currentScheme.Classes.Count == 0)
            {
                MessageBox.Show(this, Resources.ErrorClassDefined, Resources.Error,
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                DialogResult = DialogResult.None;
                return;
            }

            DialogResult = DialogResult.OK;
        }

        public ClassificationScheme GetScheme() => _currentScheme!;
    }
}
