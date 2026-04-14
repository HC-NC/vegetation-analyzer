using System.Drawing.Drawing2D;
using vegetation_analyzer.DataClasses;

namespace vegetation_analyzer.Forms
{
    public partial class IndexRasterProperties : Form
    {
        private readonly IndexRaster _indexRaster;

        public IndexRasterProperties(IndexRaster indexRaster)
        {
            InitializeComponent();
            _indexRaster = indexRaster;
        }

        private void IndexRasterProperties_Load(object sender, EventArgs e)
        {
            infoTextBox.Text =
                $"Индекс: {IndexDefinition.GetName(_indexRaster.IndexType)}\r\n" +
                $"Формула: {IndexDefinition.GetFormula(_indexRaster.IndexType)}\r\n" +
                $"Источник: {_indexRaster.SourceRaster.Name}\r\n" +
                $"Ширина: {_indexRaster.Width}\r\n" +
                $"Высота: {_indexRaster.Height}\r\n" +
                $"Min: {_indexRaster.Minimum:F4}\r\n" +
                $"Max: {_indexRaster.Maximum:F4}\r\n" +
                $"Display Min: {_indexRaster.DisplayMin:F4}\r\n" +
                $"Display Max: {_indexRaster.DisplayMax:F4}";

            infoTextBox.SelectionStart = 0;
            infoTextBox.ScrollToCaret();

            minNumericUpDown.Value = (decimal)_indexRaster.DisplayMin;
            maxNumericUpDown.Value = (decimal)_indexRaster.DisplayMax;

            interpolationComboBox.Items.Clear();
            interpolationComboBox.Items.Add("Nearest Neighbor");
            interpolationComboBox.Items.Add("Bilinear");
            interpolationComboBox.Items.Add("Bicubic");
            interpolationComboBox.Items.Add("High Quality Bilinear");
            interpolationComboBox.Items.Add("High Quality Bicubic");

            var mode = _indexRaster.InterpolationMode;
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
            _indexRaster.DisplayMin = (float)minNumericUpDown.Value;
            _indexRaster.DisplayMax = (float)maxNumericUpDown.Value;

            var mode = interpolationComboBox.SelectedIndex switch
            {
                0 => InterpolationMode.NearestNeighbor,
                1 => InterpolationMode.Bilinear,
                2 => InterpolationMode.Bicubic,
                3 => InterpolationMode.HighQualityBilinear,
                4 => InterpolationMode.HighQualityBicubic,
                _ => InterpolationMode.NearestNeighbor
            };

            _indexRaster.InterpolationMode = mode;
            DialogResult = DialogResult.OK;
        }

        private void resetButton_Click(object sender, EventArgs e)
        {
            minNumericUpDown.Value = (decimal)_indexRaster.Minimum;
            maxNumericUpDown.Value = (decimal)_indexRaster.Maximum;
        }
    }
}
