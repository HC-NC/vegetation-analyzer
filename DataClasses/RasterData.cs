using OSGeo.GDAL;
using System.Collections.ObjectModel;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;

namespace vegetation_analyzer.DataClasses
{
    public class BandFileInfo
    {
        public string FilePath { get; set; } = "";
        public string BandName { get; set; } = "";
    }

    internal class RasterData : IDisposable
    {
        private string _name;
        private string _path;

        private int _width;
        private int _height;

        private string _projection;
        private double[] _geoTransform;

        private bool _ignoreZero;

        private List<BandData> _bands;

        private int _redID;
        private int _greenID;
        private int _blueID;

        private Bitmap _bitmap;

        private bool _isBitmapValid = false;
        private bool _isDisposed = false;

        public string Name => _name;
        public string Path => _path;
        public int Width => _width;
        public int Height => _height;
        public string Projection => _projection;
        public double[] GeoTransform => _geoTransform;
        public bool IgnoreZero => _ignoreZero;
        public int BandsCount => _bands.Count;
        public int RedID => _redID;
        public int GreenID => _greenID;
        public int BlueID => _blueID;

        public InterpolationMode InterpolationMode { get; set; } = InterpolationMode.NearestNeighbor;

        public RasterData(string name, string path, int width, int height, string projection, double[] geoTransform, bool ignoreZero)
        {
            _name = name;
            _path = path;
            _width = width;
            _height = height;
            _projection = projection;
            _geoTransform = geoTransform;

            _bitmap = new Bitmap(width > 0 ? width : 1, height > 0 ? height : 1);
            _ignoreZero = ignoreZero;
            _bands = new List<BandData>();
        }

        public ReadOnlyCollection<BandData> GetBands() => _bands.AsReadOnly();

        public static RasterData LoadFile(string filePath, string fileName, bool ignoreZero)
        {
            if (!File.Exists(filePath))
                throw new FileNotFoundException($"Error GDAL {filePath}");

            Gdal.AllRegister();

            RasterData? rasterData = null;
            try
            {
                using (Dataset ds = Gdal.Open(filePath, Access.GA_ReadOnly))
                {
                    if (ds == null)
                        throw new Exception($"Error GDAL {filePath}");

                    int width = ds.RasterXSize;
                    int height = ds.RasterYSize;

                    string projection = ds.GetProjection();
                    double[] geoTransform = new double[6];
                    ds.GetGeoTransform(geoTransform);

                    rasterData = new RasterData(fileName, filePath, width, height, projection, geoTransform, ignoreZero);

                    for (int i = 1; i <= ds.RasterCount; i++)
                    {
                        using (Band gdalBand = ds.GetRasterBand(i))
                        {
                            string bandName = gdalBand.GetDescription();

                            if (string.IsNullOrWhiteSpace(bandName))
                            {
                                ColorInterp colorInterp = gdalBand.GetRasterColorInterpretation();

                                bandName = colorInterp switch
                                {
                                    ColorInterp.GCI_RedBand => "Red",
                                    ColorInterp.GCI_GreenBand => "Green",
                                    ColorInterp.GCI_BlueBand => "Blue",
                                    ColorInterp.GCI_AlphaBand => "Alpha",
                                    ColorInterp.GCI_GrayIndex => "Grayscale",
                                    _ => $"Band {i}"
                                };
                            }

                            BandData bandData = new BandData(bandName, filePath, i, width, height, ignoreZero);
                            rasterData.AddBand(bandData);
                        }
                    }

                    if (rasterData.BandsCount > 0 && rasterData.BandsCount < 3)
                        rasterData.SetViewBands(0, 0, 0);
                    else if (rasterData.BandsCount >= 3)
                        rasterData.SetViewBands(2, 1, 0);

                    return rasterData;
                }
            }
            catch (Exception ex)
            {
                rasterData?.Dispose();
                throw new ApplicationException($"Error band load {fileName}: {ex.Message}", ex);
            }
        }

        public static RasterData LoadFolder(List<BandFileInfo> bandFiles, string fileName, string folderPath, bool ignoreZero)
        {
            if (bandFiles.Count == 0)
                throw new ArgumentException($"Not finding files in {folderPath}");

            foreach (var bf in bandFiles)
                if (!File.Exists(bf.FilePath))
                    throw new FileNotFoundException($"Error GDAL {bf.FilePath}");

            Gdal.AllRegister();

            RasterData? rasterData = null;

            try
            {
                foreach (var bf in bandFiles)
                {
                    using (Dataset ds = Gdal.Open(bf.FilePath, Access.GA_ReadOnly))
                    {
                        if (ds == null)
                            throw new Exception($"Error GDAL {bf.FilePath}");

                        int width = ds.RasterXSize;
                        int height = ds.RasterYSize;

                        string projection = ds.GetProjection();
                        double[] geoTransform = new double[6];
                        ds.GetGeoTransform(geoTransform);

                        if (rasterData == null)
                            rasterData = new RasterData(fileName, folderPath, width, height, projection, geoTransform, ignoreZero);
                        else if (rasterData.Width != width || rasterData.Height != height)
                            throw new Exception($"File sizes do not match: {rasterData.Width} -> {width}");
                        else if (rasterData.Height != height)
                            throw new Exception($"File sizes do not match: {rasterData.Height} -> {height}");
                        else if (rasterData.Projection != projection)
                            throw new Exception($"Files projection do not match: {rasterData.Projection} -> {projection}");
                        else if (!rasterData.GeoTransform.SequenceEqual(geoTransform))
                            throw new Exception($"Geotransform do not match");

                        for (int i = 1; i <= ds.RasterCount; i++)
                        {
                            using (Band gdalBand = ds.GetRasterBand(i))
                            {
                                // Используем имя канала из BandFileInfo, если оно задано
                                string bandName = string.IsNullOrWhiteSpace(bf.BandName) ? gdalBand.GetDescription() : bf.BandName;

                                if (string.IsNullOrWhiteSpace(bandName))
                                {
                                    ColorInterp colorInterp = gdalBand.GetRasterColorInterpretation();

                                    bandName = colorInterp switch
                                    {
                                        ColorInterp.GCI_RedBand => "Red",
                                        ColorInterp.GCI_GreenBand => "Green",
                                        ColorInterp.GCI_BlueBand => "Blue",
                                        ColorInterp.GCI_AlphaBand => "Alpha",
                                        ColorInterp.GCI_GrayIndex => $"Band {rasterData.BandsCount + 1}",
                                        _ => $"Band {rasterData.BandsCount + 1}"
                                    };
                                }

                                BandData bandData = new BandData(bandName, bf.FilePath, i, width, height, ignoreZero);
                                rasterData.AddBand(bandData);
                            }
                        }

                        if (rasterData.BandsCount > 0 && rasterData.BandsCount < 3)
                            rasterData.SetViewBands(0, 0, 0);
                        else if (rasterData.BandsCount >= 3)
                            rasterData.SetViewBands(0, 1, 2);
                    }
                }

                if (rasterData == null)
                    throw new Exception($"Data doesn't load {folderPath}");

                return rasterData;
            }
            catch (Exception ex)
            {
                rasterData?.Dispose();
                throw new ApplicationException($"Error band load {fileName}: {ex.Message}", ex);
            }
        }

        public void AddBand(BandData band)
        {
            if (band != null)
                _bands.Add(band);
        }

        public void SetViewBands(int redID, int greenID, int blueID)
        {
            _redID = Math.Clamp(redID, 0, BandsCount - 1);
            _greenID = Math.Clamp(greenID, 0, BandsCount - 1);
            _blueID = Math.Clamp(blueID, 0, BandsCount - 1);

            _isBitmapValid = false;
        }

        public BandData? GetBand(int bandIndex)
        {
            if (0 > bandIndex || bandIndex > BandsCount)
                return null;

            return _bands[bandIndex];
        }

        public Bitmap? GetBitmap()
        {
            if (_isBitmapValid)
                return _bitmap;

            BandData? redBand = GetBand(_redID);
            BandData? greenBand = GetBand(_greenID);
            BandData? blueBand = GetBand(_blueID);

            if (redBand is null || greenBand is null || blueBand is null)
                return null;

            float[]? rData = redBand.Values;
            float[]? gData = greenBand.Values;
            float[]? bData = blueBand.Values;

            if (rData is null || gData is null || bData is null)
                return null;

            BitmapData bmpData = _bitmap.LockBits(
                new Rectangle(0, 0, Width, Height),
                ImageLockMode.WriteOnly,
                PixelFormat.Format32bppArgb);

            try
            {
                unsafe
                {
                    byte* scan0 = (byte*)bmpData.Scan0;
                    int stride = bmpData.Stride;
                    int width = Width;
                    bool ignoreZero = _ignoreZero;

                    float rMin = redBand.Minimum, rMax = redBand.Maximum;
                    float gMin = greenBand.Minimum, gMax = greenBand.Maximum;
                    float bMin = blueBand.Minimum, bMax = blueBand.Maximum;

                    float rRange = Math.Abs(rMax - rMin) < 0.0001f ? 1f : (rMax - rMin);
                    float gRange = Math.Abs(gMax - gMin) < 0.0001f ? 1f : (gMax - gMin);
                    float bRange = Math.Abs(bMax - bMin) < 0.0001f ? 1f : (bMax - bMin);

                    Parallel.For(0, Height, y =>
                    {
                        byte* row = scan0 + (y * stride);

                        for (int x = 0; x < width; x++)
                        {
                            int idx = y * width + x;
                            float r = rData[idx];
                            float g = gData[idx];
                            float b = bData[idx];

                            int offset = x * 4;

                            if (ignoreZero && r == 0 && g == 0 && b == 0)
                            {
                                row[offset] = 0;     // B
                                row[offset + 1] = 0; // G
                                row[offset + 2] = 0; // R
                                row[offset + 3] = 0; // A
                                continue;
                            }

                            byte redByte = (byte)(Math.Clamp((r - rMin) / rRange * 255, 0, 255));
                            byte greenByte = (byte)(Math.Clamp((g - gMin) / gRange * 255, 0, 255));
                            byte blueByte = (byte)(Math.Clamp((b - bMin) / bRange * 255, 0, 255));

                            row[offset] = blueByte;
                            row[offset + 1] = greenByte;
                            row[offset + 2] = redByte;
                            row[offset + 3] = 255;
                        }
                    });
                }
            }
            finally
            {
                _bitmap.UnlockBits(bmpData);

                redBand.Unload();
                greenBand.Unload();
                blueBand.Unload();
            }

            _isBitmapValid = true;
            return _bitmap;
        }

        public void Dispose()
        {
            if (_isDisposed) return;

            foreach (BandData b in _bands)
                b.Dispose();

            _bands.Clear();
            _bitmap?.Dispose();

            _isDisposed = true;
            GC.SuppressFinalize(this);
        }

        public override string ToString() => Name;
    }
}
