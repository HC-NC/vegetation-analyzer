using vegetation_analyzer.DataClasses;
using vegetation_analyzer.Properties;

namespace vegetation_analyzer.Forms
{
    public partial class ComputeIndexForm : Form
    {
        private readonly RasterData _raster;

        public ComputeIndexForm(RasterData raster)
        {
            InitializeComponent();

            _raster = raster;
        }

        private void ComputeIndexForm_Load(object sender, EventArgs e)
        {
            PopulateIndexList();
            UpdateBandMapping();
        }

        private void PopulateIndexList()
        {
            indexListBox.Items.Clear();
            foreach (VegetationIndex idx in Enum.GetValues<VegetationIndex>())
            {
                indexListBox.Items.Add(idx);
            }
            indexListBox.SelectedIndex = 0;
        }

        private void UpdateBandMapping()
        {
            if (indexListBox.SelectedItem is not VegetationIndex selectedIndex) return;

            var requiredBands = IndexDefinition.GetRequiredBands(selectedIndex);

            // Описание
            descriptionTextBox.Text =
                $"{Resources.Formula}\r\n{IndexDefinition.GetFormula(selectedIndex)}\r\n\r\n" +
                $"{IndexDefinition.GetDescription(selectedIndex)}";

            // Заполняем маппинг
            bandMappingPanel.Controls.Clear();

            int y = 16;
            foreach (var role in requiredBands)
            {
                var label = new Label
                {
                    Text = $"{role}:",
                    Location = new Point(10, y + 5),
                    Size = new Size(80, 25),
                    TextAlign = ContentAlignment.MiddleRight
                };

                var comboBox = new ComboBox
                {
                    Name = $"cmb_{role}",
                    DropDownStyle = ComboBoxStyle.DropDownList,
                    Location = new Point(95, y + 3),
                    Size = new Size(200, 25),
                    Tag = role
                };

                // Добавляем каналы растра
                var bands = _raster.GetBands();
                for (int i = 0; i < bands.Count; i++)
                {
                    comboBox.Items.Add($"[{i}] {bands[i].Name}");
                }

                bandMappingPanel.Controls.Add(label);
                bandMappingPanel.Controls.Add(comboBox);

                y += 30;
            }

            // Авто-маппинг
            TryAutoMap(requiredBands);

            // Обновляем высоту панели
            bandMappingPanel.Height = Math.Max(y + 10, 60);
        }

        private void TryAutoMap(SpectralBandRole[] requiredBands)
        {
            var bands = _raster.GetBands();
            Dictionary<SpectralBandRole, int>? autoMap = null;

            // Пробуем Landsat 8/9
            autoMap = IndexDefinition.AutoMapLandsat89(bands);

            // Если не получилось — пробуем Sentinel-2
            if (autoMap == null)
                autoMap = IndexDefinition.AutoMapSentinel2(bands);

            if (autoMap != null)
            {
                foreach (var role in requiredBands)
                {
                    if (autoMap.TryGetValue(role, out int bandIndex) && bandIndex < bands.Count)
                    {
                        var cmb = bandMappingPanel.Controls.Find($"cmb_{role}", false).FirstOrDefault() as ComboBox;
                        if (cmb != null)
                            cmb.SelectedIndex = bandIndex;
                    }
                }
            }
        }

        public VegetationIndex SelectedIndex
        {
            get => indexListBox.SelectedItem is VegetationIndex idx ? idx : VegetationIndex.NDVI;
        }

        public Dictionary<SpectralBandRole, int> GetBandMapping()
        {
            var mapping = new Dictionary<SpectralBandRole, int>();
            foreach (Control ctrl in bandMappingPanel.Controls)
            {
                if (ctrl is ComboBox cmb && cmb.Tag is SpectralBandRole role && cmb.SelectedIndex >= 0)
                {
                    mapping[role] = cmb.SelectedIndex;
                }
            }
            return mapping;
        }

        private void indexListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateBandMapping();
        }

        private void acceptButton_Click(object sender, EventArgs e)
        {
            var mapping = GetBandMapping();
            var selectedIndex = SelectedIndex;
            var requiredBands = IndexDefinition.GetRequiredBands(selectedIndex);

            foreach (var role in requiredBands)
            {
                if (!mapping.ContainsKey(role))
                {
                    MessageBox.Show(this, string.Format(Resources.ErrorBandSelect, role), Resources.Error,
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    DialogResult = DialogResult.None;
                    return;
                }
            }

            DialogResult = DialogResult.OK;
        }
    }
}
