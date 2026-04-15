using vegetation_analyzer.DataClasses;

namespace vegetation_analyzer.Forms
{
    public partial class ExportForm : Form
    {
        public enum ExportTarget { RasterData, IndexRaster, ClassifiedRaster }

        private readonly object _data;
        private readonly ExportTarget _target;

        public ExportForm(object data, ExportTarget target)
        {
            InitializeComponent();

            _data = data;
            _target = target;
        }

        private void ExportForm_Load(object sender, EventArgs e)
        {
            formatComboBox.Items.Clear();
            formatComboBox.Items.Add("GeoTIFF");

            compressionComboBox.Items.Clear();
            compressionComboBox.Items.Add("None");
            compressionComboBox.Items.Add("LZW");
            compressionComboBox.Items.Add("DEFLATE");
            compressionComboBox.Items.Add("PACKBITS");

            compressionComboBox.SelectedIndex = 1; // LZW по умолчанию

            // Для классифицированных — по умолчанию включаем палитру
            includePaletteCheckBox.Checked = _target == ExportTarget.ClassifiedRaster;
            includePaletteCheckBox.Enabled = _target == ExportTarget.ClassifiedRaster;

            // Для IndexRaster — опция масштабирования в Byte
            exportAsByteCheckBox.Visible = _target == ExportTarget.IndexRaster;
            exportAsByteCheckBox.Enabled = _target == ExportTarget.IndexRaster;

            // Обновляем описание
            string info = _target switch
            {
                ExportTarget.RasterData => "Экспорт растра в GeoTIFF",
                ExportTarget.IndexRaster => "Экспорт индекса в GeoTIFF (Float32 или Byte)",
                ExportTarget.ClassifiedRaster => "Экспорт классификации в GeoTIFF с палитрой",
                _ => ""
            };
            infoLabel.Text = info;
        }

        private void browseButton_Click(object sender, EventArgs e)
        {
            string ext = "tif";
            saveFileDialog1.Filter = "GeoTIFF|*.tif;*.tiff|All files|*.*";
            saveFileDialog1.DefaultExt = ext;
            saveFileDialog1.FileName = GetDefaultFileName();

            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                pathTextBox.Text = saveFileDialog1.FileName;
        }

        private string GetDefaultFileName()
        {
            return _target switch
            {
                ExportTarget.RasterData => ((RasterData)_data).Name + ".tif",
                ExportTarget.IndexRaster => ((IndexRaster)_data).Name + ".tif",
                ExportTarget.ClassifiedRaster => ((ClassifiedRaster)_data).Name + ".tif",
                _ => "export.tif"
            };
        }

        private void exportButton_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(pathTextBox.Text))
            {
                MessageBox.Show(this, "Укажите путь для экспорта.", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                DialogResult = DialogResult.None;
                return;
            }

            string compression = compressionComboBox.SelectedItem?.ToString() ?? "NONE";
            if (compression == "None") compression = "NONE";

            DialogResult = DialogResult.OK;
        }

        public string FilePath => pathTextBox.Text;
        public string Compression => compressionComboBox.SelectedItem?.ToString() ?? "NONE";
        public bool IncludePalette => includePaletteCheckBox.Checked;
        public bool ExportAsByte => exportAsByteCheckBox.Checked;
    }
}
