using OSGeo.GDAL;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;

namespace vegetation_analyzer.DataClasses
{
    /// <summary>
    /// Классифицированный растр (byte + ColorTable).
    /// </summary>
    public class ClassifiedRaster : IDisposable
    {
        private readonly string _name;
        private readonly int _width;
        private readonly int _height;
        private readonly byte[] _values; // 0 = no data, 1..N = class index
        private readonly ClassificationScheme _scheme;
        private readonly IndexRaster _sourceIndex;
        private readonly string _projection;
        private readonly double[] _geoTransform;

        private Bitmap? _bitmap;
        private bool _isBitmapValid = false;
        private bool _isDisposed = false;

        public string Name => _name;
        public int Width => _width;
        public int Height => _height;
        public byte[] Values => _values;
        public ClassificationScheme Scheme => _scheme;
        public IndexRaster SourceIndex => _sourceIndex;
        public string Projection => _projection;
        public double[] GeoTransform => _geoTransform;

        public InterpolationMode InterpolationMode { get; set; } = InterpolationMode.NearestNeighbor;

        private ClassifiedRaster(string name, int width, int height, byte[] values,
            ClassificationScheme scheme, IndexRaster sourceIndex)
        {
            _name = name;
            _width = width;
            _height = height;
            _values = values;
            _scheme = scheme;
            _sourceIndex = sourceIndex;
            _projection = sourceIndex.Projection;
            _geoTransform = sourceIndex.GeoTransform;
        }

        /// <summary>
        /// Классифицирует IndexRaster по заданной схеме.
        /// </summary>
        public static ClassifiedRaster Classify(IndexRaster sourceIndex, ClassificationScheme scheme)
        {
            int width = sourceIndex.Width;
            int height = sourceIndex.Height;
            float[] indexValues = sourceIndex.Values;
            byte[] classValues = new byte[indexValues.Length];

            Parallel.For(0, indexValues.Length, i =>
            {
                classValues[i] = float.IsNaN(indexValues[i]) ? (byte)0 : scheme.GetClassIndex(indexValues[i]);
            });

            string className = $"Classified_{scheme.Name.Replace(" — ", "_").Replace(" - ", "_")}";
            return new ClassifiedRaster(className, width, height, classValues, scheme, sourceIndex);
        }

        /// <summary>
        /// Получает Bitmap с применённой палитрой.
        /// </summary>
        public Bitmap GetBitmap()
        {
            if (_isBitmapValid && _bitmap != null)
                return _bitmap;

            _bitmap?.Dispose();
            _bitmap = new Bitmap(_width, _height);

            BitmapData bmpData = _bitmap.LockBits(
                new Rectangle(0, 0, _width, _height),
                ImageLockMode.WriteOnly,
                PixelFormat.Format32bppArgb);

            unsafe
            {
                byte* scan0 = (byte*)bmpData.Scan0;
                int stride = bmpData.Stride;
                int width = _width;
                var classes = _scheme.Classes;

                // Предвычисляем цвета для каждого класса (1-based)
                var colors = new Color[classes.Count + 1]; // index 0 = transparent
                for (int i = 0; i < classes.Count; i++)
                    colors[i + 1] = classes[i].Color;

                Parallel.For(0, _height, y =>
                {
                    byte* row = scan0 + y * stride;

                    for (int x = 0; x < width; x++)
                    {
                        int idx = y * width + x;
                        byte classIdx = _values[idx];
                        int offset = x * 4;

                        if (classIdx == 0 || classIdx > classes.Count)
                        {
                            row[offset] = 0;     // B
                            row[offset + 1] = 0; // G
                            row[offset + 2] = 0; // R
                            row[offset + 3] = 0; // A
                        }
                        else
                        {
                            Color c = colors[classIdx];
                            row[offset] = c.B;
                            row[offset + 1] = c.G;
                            row[offset + 2] = c.R;
                            row[offset + 3] = 255;
                        }
                    }
                });
            }

            _bitmap.UnlockBits(bmpData);
            _isBitmapValid = true;
            return _bitmap;
        }

        /// <summary>
        /// Экспорт в GeoTIFF с ColorTable.
        /// </summary>
        public void ExportToGeoTIFF(string filePath, string compression = "NONE")
        {
            Gdal.AllRegister();

            var driver = Gdal.GetDriverByName("GTiff");
            if (driver == null)
                throw new Exception("GTiff driver not available");

            using (Dataset ds = driver.Create(filePath, _width, _height, 1, DataType.GDT_Byte, new string[] { $"COMPRESS={compression}" }))
            {
                if (ds == null)
                    throw new Exception($"Cannot create file: {filePath}");

                ds.SetGeoTransform(_geoTransform);
                ds.SetProjection(_projection);

                using (Band band = ds.GetRasterBand(1))
                {
                    // Записываем данные
                    CPLErr err = band.WriteRaster(0, 0, _width, _height, _values, _width, _height, 0, 0);
                    if (err != CPLErr.CE_None)
                        throw new Exception("Error writing raster data");

                    // Создаём ColorTable
                    ColorTable colorTable = new ColorTable(PaletteInterp.GPI_RGB);
                    var classes = _scheme.Classes;

                    for (int i = 0; i < classes.Count; i++)
                    {
                        ColorEntry entry = new ColorEntry();
                        entry.c1 = classes[i].Color.R;
                        entry.c2 = classes[i].Color.G;
                        entry.c3 = classes[i].Color.B;
                        entry.c4 = 255;
                        colorTable.SetColorEntry(i + 1, entry);
                    }

                    band.SetColorTable(colorTable);
                }

                // Метаданные
                ds.SetMetadataItem("INDEX_TYPE", IndexDefinition.GetName(_sourceIndex.IndexType), "CLASSIFICATION");
                ds.SetMetadataItem("SCHEME_NAME", _scheme.Name, "CLASSIFICATION");
                ds.SetMetadataItem("CLASS_COUNT", _scheme.Classes.Count.ToString(), "CLASSIFICATION");

                for (int i = 0; i < _scheme.Classes.Count; i++)
                {
                    var c = _scheme.Classes[i];
                    ds.SetMetadataItem($"CLASS_{i + 1}_NAME", c.Name, "CLASSIFICATION");
                    ds.SetMetadataItem($"CLASS_{i + 1}_MIN", c.Min.ToString("F4"), "CLASSIFICATION");
                    ds.SetMetadataItem($"CLASS_{i + 1}_MAX", c.Max.ToString("F4"), "CLASSIFICATION");
                }
            }
        }

        public void Dispose()
        {
            if (_isDisposed) return;
            _bitmap?.Dispose();
            _isDisposed = true;
            GC.SuppressFinalize(this);
        }

        public override string ToString() => Name;
    }
}
