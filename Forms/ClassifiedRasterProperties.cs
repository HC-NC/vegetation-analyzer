using System.Drawing.Drawing2D;
using vegetation_analyzer.DataClasses;

namespace vegetation_analyzer.Forms
{
    public partial class ClassifiedRasterProperties : Form
    {
        private readonly ClassifiedRaster _classified;

        public ClassifiedRasterProperties(ClassifiedRaster classified)
        {
            InitializeComponent();
            _classified = classified;
        }

        private void ClassifiedRasterProperties_Load(object sender, EventArgs e)
        {
            infoTextBox.Text =
                $"Классификация: {_classified.Name}\r\n" +
                $"Схема: {_classified.Scheme.Name}\r\n" +
                $"Индекс: {IndexDefinition.GetName(_classified.SourceIndex.IndexType)}\r\n" +
                $"Источник: {_classified.SourceIndex.SourceRaster}/{_classified.SourceIndex.Name}\r\n" +
                $"Ширина: {_classified.Width}\r\n" +
                $"Высота: {_classified.Height}\r\n" +
                $"Классов: {_classified.Scheme.Classes.Count}\r\n" +
                $"\r\nКлассы:\r\n" +
                string.Join("\r\n", _classified.Scheme.Classes.Select((c, i) => $"  {i + 1}. {c.Name} [{c.Min:F3} - {c.Max:F3}]"));

            infoTextBox.SelectionStart = 0;
            infoTextBox.ScrollToCaret();

            interpolationComboBox.Items.Clear();
            interpolationComboBox.Items.Add("Nearest Neighbor");
            interpolationComboBox.Items.Add("Bilinear");
            interpolationComboBox.Items.Add("Bicubic");
            interpolationComboBox.Items.Add("High Quality Bilinear");
            interpolationComboBox.Items.Add("High Quality Bicubic");

            var mode = _classified.InterpolationMode;
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
            var mode = interpolationComboBox.SelectedIndex switch
            {
                0 => InterpolationMode.NearestNeighbor,
                1 => InterpolationMode.Bilinear,
                2 => InterpolationMode.Bicubic,
                3 => InterpolationMode.HighQualityBilinear,
                4 => InterpolationMode.HighQualityBicubic,
                _ => InterpolationMode.NearestNeighbor
            };

            _classified.InterpolationMode = mode;
            DialogResult = DialogResult.OK;
        }
    }
}
