using vegetation_analyzer.DataClasses;
using vegetation_analyzer.Properties;

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

            saveFileDialog1.Filter = "GeoTIFF|*.tif;*.tiff";
            saveFileDialog1.DefaultExt = "tif";
        }

        private void ExportForm_Load(object sender, EventArgs e)
        {
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

            string fileName = _data.ToString() + ".tif";
            saveFileDialog1.FileName = fileName;

            string info = "";
            string path = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);

            switch (_data)
            {
                case RasterData rasterData:
                    info = Resources.RasterToGTIFF;
                    path = Path.Combine(Path.GetDirectoryName(rasterData.Path) ?? path, fileName);
                    break;
                case IndexRaster indexRaster:
                    info = Resources.IndexToGTIFF;
                    path = Path.Combine(Path.GetDirectoryName(indexRaster.SourceRaster.Path) ?? path, fileName);
                    break;
                case ClassifiedRaster classifiedRaster:
                    info = Resources.ClassifiedToGTIFF;
                    path = Path.Combine(Path.GetDirectoryName(classifiedRaster.SourceIndex.SourceRaster.Path) ?? path, fileName);
                    break;
            }

            infoLabel.Text = info;
            pathTextBox.Text = path;
        }

        private void browseButton_Click(object sender, EventArgs e)
        {
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                pathTextBox.Text = saveFileDialog1.FileName;
        }

        private void exportButton_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(pathTextBox.Text))
            {
                MessageBox.Show(this, Resources.ErrorExportPath, Resources.Error,
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
