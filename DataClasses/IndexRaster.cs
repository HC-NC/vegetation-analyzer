using System.Drawing.Drawing2D;
using System.Drawing.Imaging;

namespace vegetation_analyzer.DataClasses
{
    /// <summary>
    /// Растр вегетационного индекса (float-значения).
    /// </summary>
    public class IndexRaster : IDisposable
    {
        private readonly string _name;
        private readonly int _width;
        private readonly int _height;
        private readonly float[] _values;
        private readonly VegetationIndex _indexType;
        private readonly string _projection;
        private readonly double[] _geoTransform;
        private readonly RasterData _sourceRaster;

        private float _minimum;
        private float _maximum;

        private Bitmap? _bitmap;
        private float _displayMin = float.NaN;
        private float _displayMax = float.NaN;
        private bool _isBitmapValid = false;
        private bool _isDisposed = false;

        public string Name => _name;
        public int Width => _width;
        public int Height => _height;
        public float[] Values => _values;
        public VegetationIndex IndexType => _indexType;
        public string Projection => _projection;
        public double[] GeoTransform => _geoTransform;
        public RasterData SourceRaster => _sourceRaster;
        public float Minimum => _minimum;
        public float Maximum => _maximum;

        /// <summary>
        /// Минимум для визуализации (по умолчанию = Minimum).
        /// </summary>
        public float DisplayMin
        {
            get => float.IsNaN(_displayMin) ? _minimum : _displayMin;
            set { _displayMin = value; _isBitmapValid = false; }
        }

        /// <summary>
        /// Максимум для визуализации (по умолчанию = Maximum).
        /// </summary>
        public float DisplayMax
        {
            get => float.IsNaN(_displayMax) ? _maximum : _displayMax;
            set { _displayMax = value; _isBitmapValid = false; }
        }

        public InterpolationMode InterpolationMode { get; set; } = InterpolationMode.NearestNeighbor;

        private IndexRaster(string name, int width, int height, float[] values,
            VegetationIndex indexType, string projection, double[] geoTransform, RasterData sourceRaster)
        {
            _name = name;
            _width = width;
            _height = height;
            _values = values;
            _indexType = indexType;
            _projection = projection;
            _geoTransform = geoTransform;
            _sourceRaster = sourceRaster;

            // Вычисляем min/max (игнорируя NaN)
            float min = float.MaxValue;
            float max = float.MinValue;
            for (int i = 0; i < values.Length; i++)
            {
                if (float.IsNaN(values[i])) continue;
                if (values[i] < min) min = values[i];
                if (values[i] > max) max = values[i];
            }
            _minimum = min == float.MaxValue ? 0 : min;
            _maximum = max == float.MinValue ? 0 : max;
        }

        /// <summary>
        /// Вычисляет вегетационный индекс для растра.
        /// bandMapping: SpectralBandRole → индекс канала в sourceRaster.
        /// </summary>
        public static IndexRaster Compute(RasterData sourceRaster, VegetationIndex indexType,
            Dictionary<SpectralBandRole, int> bandMapping)
        {
            var definition = IndexDefinition.GetRequiredBands(indexType);
            foreach (var role in definition)
                if (!bandMapping.ContainsKey(role))
                    throw new ArgumentException($"Не указан канал для {role}");

            int width = sourceRaster.Width;
            int height = sourceRaster.Height;
            float[] values = new float[width * height];

            // Загружаем данные всех требуемых каналов ЗАРАНЕЕ (до Parallel.For)
            var bandDatas = new Dictionary<SpectralBandRole, float[]>();
            var bandIgnoreZero = new Dictionary<SpectralBandRole, bool>();
            var loadedBands = new List<BandData>();
            foreach (var kvp in bandMapping)
            {
                var band = sourceRaster.GetBand(kvp.Value);
                if (band == null)
                    throw new ArgumentException($"Канал с индексом {kvp.Value} не найден");

                float[]? bandValues = band.Values;
                if (bandValues == null)
                    throw new ArgumentException($"Не удалось загрузить данные канала {band.Name}");

                bandDatas[kvp.Key] = bandValues;
                bandIgnoreZero[kvp.Key] = band.IgnoreZero;
                loadedBands.Add(band);
            }

            // Вычисляем индекс
            Parallel.For(0, height, y =>
            {
                for (int x = 0; x < width; x++)
                {
                    int idx = y * width + x;
                    var pixelBands = new Dictionary<SpectralBandRole, float>();

                    bool allValid = true;
                    foreach (var kvp in bandDatas)
                    {
                        float val = kvp.Value[idx];
                        // Если IgnoreZero = true, нулевые значения = no data
                        if (bandIgnoreZero[kvp.Key] && val == 0) { allValid = false; break; }
                        pixelBands[kvp.Key] = val;
                    }

                    values[idx] = allValid
                        ? IndexDefinition.Compute(indexType, pixelBands)
                        : float.NaN;
                }
            });

            // Освобождаем загруженные каналы
            foreach (var band in loadedBands)
                band.Unload();

            string indexName = IndexDefinition.GetName(indexType);
            return new IndexRaster(indexName, width, height, values, indexType,
                sourceRaster.Projection, sourceRaster.GeoTransform, sourceRaster);
        }

        /// <summary>
        /// Получает Bitmap для визуализации с цветовой шкалой NDVI-style (красный-жёлтый-зелёный).
        /// </summary>
        public Bitmap GetBitmap()
        {
            if (_isBitmapValid && _bitmap != null)
                return _bitmap;

            _bitmap?.Dispose();
            _bitmap = new Bitmap(_width, _height);

            float min = DisplayMin;
            float max = DisplayMax;
            float range = Math.Abs(max - min) < 0.0001f ? 1f : (max - min);

            BitmapData bmpData = _bitmap.LockBits(
                new Rectangle(0, 0, _width, _height),
                ImageLockMode.WriteOnly,
                PixelFormat.Format32bppArgb);

            try
            {
                unsafe
                {
                    byte* scan0 = (byte*)bmpData.Scan0;
                    int stride = bmpData.Stride;
                    int width = _width;

                    Parallel.For(0, _height, y =>
                    {
                        byte* row = scan0 + y * stride;

                        for (int x = 0; x < width; x++)
                        {
                            int idx = y * width + x;
                            float v = _values[idx];
                            int offset = x * 4;

                            if (float.IsNaN(v))
                            {
                                row[offset] = 0;     // B
                                row[offset + 1] = 0; // G
                                row[offset + 2] = 0; // R
                                row[offset + 3] = 0; // A
                                continue;
                            }

                            // Нормализуем в [0, 1]
                            float t = Math.Clamp((v - min) / range, 0f, 1f);

                            // NDVI color ramp: красный (0) → жёлтый (0.5) → зелёный (1)
                            byte r, g, b;
                            if (t < 0.5f)
                            {
                                // Красный → Жёлтый
                                float s = t / 0.5f;
                                r = 255;
                                g = (byte)(s * 255);
                                b = 0;
                            }
                            else
                            {
                                // Жёлтый → Зелёный
                                float s = (t - 0.5f) / 0.5f;
                                r = (byte)((1 - s) * 255);
                                g = 255;
                                b = 0;
                            }

                            row[offset] = b;
                            row[offset + 1] = g;
                            row[offset + 2] = r;
                            row[offset + 3] = 255;
                        }
                    });
                }
            }
            finally
            {
                _bitmap.UnlockBits(bmpData);
            }

            _isBitmapValid = true;
            return _bitmap;
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
