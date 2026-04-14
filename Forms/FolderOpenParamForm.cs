using OSGeo.GDAL;
using System.ComponentModel;
using vegetation_analyzer.DataClasses;

namespace vegetation_analyzer.Forms
{
    public partial class FolderOpenParamForm : Form
    {
        private readonly string _folderPath;

        public FolderOpenParamForm(string folderPath)
        {
            InitializeComponent();

            _folderPath = folderPath;
            folderPathTextBox.Text = folderPath;
        }

        private void FolderOpenParamForm_Load(object sender, EventArgs e)
        {
            selectAllCheckBox.Checked = false;

            AnalyzeFolder();
        }

        private void AnalyzeFolder()
        {
            Gdal.AllRegister();

            var tiffFiles = Directory.GetFiles(_folderPath, "*.tif", SearchOption.TopDirectoryOnly)
                .OrderBy(f => f)
                .ToList();

            var fileInfos = new List<LandsatFileInfo>();

            foreach (var file in tiffFiles)
            {
                string fileName = Path.GetFileName(file);
                string bandName = ExtractBandName(fileName);
                string description = ExtractDescription(fileName);

                LandsatFileInfo info = new LandsatFileInfo
                {
                    FilePath = file,
                    FileName = fileName,
                    BandName = bandName,
                    Description = description,
                    Selected = IsDefaultBandSelected(bandName)
                };

                try
                {
                    using (Dataset ds = Gdal.Open(file, Access.GA_ReadOnly))
                    {
                        if (ds != null)
                        {
                            info.Width = ds.RasterXSize;
                            info.Height = ds.RasterYSize;

                            using (Band band = ds.GetRasterBand(1))
                            {
                                if (band != null)
                                {
                                    double min, max, mean, stdDev;
                                    band.GetStatistics(0, 1, out min, out max, out mean, out stdDev);
                                    info.Min = min;
                                    info.Max = max;
                                }
                            }
                        }
                    }
                }
                catch
                {
                    // Если не удалось открыть, оставляем пустые значения
                }

                fileInfos.Add(info);
            }

            filesDataGridView.Rows.Clear();
            foreach (var info in fileInfos)
            {
                int rowIndex = filesDataGridView.Rows.Add();
                var row = filesDataGridView.Rows[rowIndex];
                row.Cells["colSelected"].Value = info.Selected;
                row.Cells["colBandName"].Value = info.BandName;
                row.Cells["colFileName"].Value = info.FileName;
                row.Cells["colWidth"].Value = info.Width > 0 ? info.Width.ToString() : "—";
                row.Cells["colHeight"].Value = info.Height > 0 ? info.Height.ToString() : "—";
                row.Cells["colMin"].Value = info.Min.HasValue ? info.Min.Value.ToString("F2") : "—";
                row.Cells["colMax"].Value = info.Max.HasValue ? info.Max.Value.ToString("F2") : "—";
                row.Cells["colDescription"].Value = info.Description;
                row.Tag = info;
            }

            if (fileInfos.Count == 0)
            {
                MessageBox.Show(this, "В указанной папке не найдено TIFF файлов.", "Внимание",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private string ExtractBandName(string fileName)
        {
            // Landsat файлы: LC08_L1TP_XXX_YYY_MMDD_YYYYMMDD_02_T1_B{n}.tif
            // Sentinel-2 и другие: ..._B0{n}.tif, B0{n}.tif
            // Также возможны: band{n}.tif, B{n}.tif
            var match = System.Text.RegularExpressions.Regex.Match(fileName, @"[Bb](?:and)?[_]?(\d+)", System.Text.RegularExpressions.RegexOptions.IgnoreCase);
            if (match.Success)
            {
                int bandNum = int.Parse(match.Groups[1].Value);
                return $"Band {bandNum}";
            }

            // Если не удалось извлечь номер, используем имя файла без расширения
            return Path.GetFileNameWithoutExtension(fileName);
        }

        private string ExtractDescription(string fileName)
        {
            // Попытка определить описание по имени файла (Landsat band naming)
            var bandNumMatch = System.Text.RegularExpressions.Regex.Match(fileName, @"[Bb](?:and)?[_]?(\d+)", System.Text.RegularExpressions.RegexOptions.IgnoreCase);
            if (bandNumMatch.Success)
            {
                int bandNum = int.Parse(bandNumMatch.Groups[1].Value);
                return bandNum switch
                {
                    1 => "Ultra Blue / Coastal Aerosol",
                    2 => "Blue",
                    3 => "Green",
                    4 => "Red",
                    5 => "Near Infrared (NIR)",
                    6 => "Shortwave Infrared (SWIR) 1",
                    7 => "Shortwave Infrared (SWIR) 2",
                    8 => "Panchromatic",
                    9 => "Cirrus",
                    10 => "Thermal Infrared 1",
                    11 => "Thermal Infrared 2",
                    _ => $"Spectral Band {bandNum}"
                };
            }
            return "";
        }

        private bool IsDefaultBandSelected(string bandName)
        {
            return bandName.Contains("Band 2", StringComparison.OrdinalIgnoreCase) ||
                   bandName.Contains("Band 3", StringComparison.OrdinalIgnoreCase) ||
                   bandName.Contains("Band 4", StringComparison.OrdinalIgnoreCase) ||
                   bandName.Contains("Band 5", StringComparison.OrdinalIgnoreCase) ||
                   bandName.Contains("Band 6", StringComparison.OrdinalIgnoreCase) ||
                   bandName.Contains("Band 7", StringComparison.OrdinalIgnoreCase);
        }

        private void selectAllCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in filesDataGridView.Rows)
            {
                row.Cells["colSelected"].Value = selectAllCheckBox.Checked;
            }
        }

        public List<BandFileInfo> SelectedBands
        {
            get
            {
                var selected = new List<BandFileInfo>();
                foreach (DataGridViewRow row in filesDataGridView.Rows)
                {
                    if (row.Cells["colSelected"].Value is bool isSelected && isSelected)
                    {
                        if (row.Tag is LandsatFileInfo info)
                            selected.Add(new BandFileInfo { FilePath = info.FilePath, BandName = info.BandName });
                    }
                }
                return selected;
            }
        }

        public bool IgnoreZero => ignoreZeroCheckBox.Checked;

        private void acceptButton_Click(object sender, EventArgs e)
        {
            if (SelectedBands.Count == 0)
            {
                MessageBox.Show(this, "Не выбрано ни одного файла.", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                DialogResult = DialogResult.None;
            }
        }
    }

    internal class LandsatFileInfo
    {
        public string FilePath { get; set; } = "";
        public string FileName { get; set; } = "";
        public string BandName { get; set; } = "";
        public string Description { get; set; } = "";
        public int Width { get; set; }
        public int Height { get; set; }
        public double? Min { get; set; }
        public double? Max { get; set; }
        public bool Selected { get; set; }
    }
}
