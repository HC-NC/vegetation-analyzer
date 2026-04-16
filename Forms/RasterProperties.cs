using System.Drawing.Drawing2D;
using vegetation_analyzer.DataClasses;
using vegetation_analyzer.Properties;

namespace vegetation_analyzer.Forms
{
    public partial class RasterProperties : Form
    {
        private readonly RasterData _raster;

        public RasterProperties(RasterData raster)
        {
            InitializeComponent();

            _raster = raster;
        }

        private void RasterProperties_Load(object sender, EventArgs e)
        {
            PopulateInfo();
            PopulateBandSelectors();
            PopulateInterpolation();
        }

        private void PopulateInfo()
        {
            infoTextBox.Text =
                $"{Resources.Name}: {_raster.Name}\r\n" +
                $"{Resources.Path}: {_raster.Path}\r\n" +
                $"{Resources.Width}: {_raster.Width}\r\n" +
                $"{Resources.Height}: {_raster.Height}\r\n" +
                $"{Resources.BandsCount}: {_raster.BandsCount}\r\n" +
                $"{Resources.Projection}: {_raster.Projection}\r\n" +
                $"{Resources.GeoTransform}: [{string.Join(", ", _raster.GeoTransform)}]\r\n" +
                $"{Resources.IgnoreZero}: {_raster.IgnoreZero}\r\n" +
                $"\r\n{Resources.Bands}:\r\n" +
                string.Join("\r\n", _raster.GetBands().Select((b, i) => $"  [{i}] {b.Name}"));

            infoTextBox.SelectionStart = 0;
            infoTextBox.ScrollToCaret();
        }

        private void PopulateBandSelectors()
        {
            var bandItems = _raster.GetBands().Select((b, i) => new { Index = i, Name = b.Name }).ToArray();

            redComboBox.Items.Clear();
            greenComboBox.Items.Clear();
            blueComboBox.Items.Clear();

            foreach (var item in bandItems)
            {
                redComboBox.Items.Add($"[{item.Index}] {item.Name}");
                greenComboBox.Items.Add($"[{item.Index}] {item.Name}");
                blueComboBox.Items.Add($"[{item.Index}] {item.Name}");
            }

            redComboBox.SelectedIndex = _raster.RedID;
            greenComboBox.SelectedIndex = _raster.GreenID;
            blueComboBox.SelectedIndex = _raster.BlueID;
        }

        private void PopulateInterpolation()
        {
            interpolationComboBox.Items.Clear();
            interpolationComboBox.Items.Add(Resources.NearestNeighbor);
            interpolationComboBox.Items.Add(Resources.Bilinear);
            interpolationComboBox.Items.Add(Resources.Bicubic);
            interpolationComboBox.Items.Add(Resources.HighQualityBilinear);
            interpolationComboBox.Items.Add(Resources.HighQualityBicubic);

            var mode = _raster.InterpolationMode;
            interpolationComboBox.SelectedIndex = mode switch
            {
                InterpolationMode.NearestNeighbor => 0,
                InterpolationMode.Bilinear => 1,
                InterpolationMode.Bicubic => 2,
                InterpolationMode.HighQualityBilinear => 3,
                InterpolationMode.HighQualityBicubic => 4,
                _ => 0
            };
        }

        private void applyButton_Click(object sender, EventArgs e)
        {
            if (redComboBox.SelectedIndex < 0 || greenComboBox.SelectedIndex < 0 || blueComboBox.SelectedIndex < 0)
            {
                MessageBox.Show(this, Resources.ErrorRGBBands, Resources.Error,
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            _raster.SetViewBands(redComboBox.SelectedIndex, greenComboBox.SelectedIndex, blueComboBox.SelectedIndex);

            var mode = interpolationComboBox.SelectedIndex switch
            {
                0 => InterpolationMode.NearestNeighbor,
                1 => InterpolationMode.Bilinear,
                2 => InterpolationMode.Bicubic,
                3 => InterpolationMode.HighQualityBilinear,
                4 => InterpolationMode.HighQualityBicubic,
                _ => InterpolationMode.NearestNeighbor
            };

            _raster.InterpolationMode = mode;

            DialogResult = DialogResult.OK;
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }
    }
}
