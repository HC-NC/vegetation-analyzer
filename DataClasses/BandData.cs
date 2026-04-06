using OSGeo.GDAL;
using System.Diagnostics.Metrics;
using System.Resources;

namespace vegetation_analyzer.DataClasses
{
    internal class BandData : IDisposable
    {
        private readonly object _lockObj = new object();

        private string _name;
        private string _path;
        private int _bandIndex;

        private int _width;
        private int _height;

        private bool _ignoreZero;

        private float _minimum;
        private float _maximum;

        private float[]? _values;

        private bool _isDisposed = false;

        public string Name => _name;
        public string Path => _path;
        public int BandIndex => _bandIndex;
        public int Width => _width;
        public int Height => _height;
        public bool IgnoreZero => _ignoreZero;
        public float Minimum => _minimum;
        public float Maximum => _maximum;

        public float[]? Values
        {
            get
            {
                if (_values is null)
                {
                    lock (_lockObj)
                    {
                        if (_values is null)
                        {
                            LoadValuesFromGDAL();
                        }
                    }
                }
                return _values;
            }
        }

        public BandData(string name,  string path, int bandIndex, int width, int height, bool ignoreZero)
        {
            _name = name;
            _path = path;
            _bandIndex = bandIndex;
            _width = width;
            _height = height;
            _ignoreZero = ignoreZero;
        }

        private void LoadValuesFromGDAL()
        {
            try
            {
                using (Dataset ds = Gdal.Open(Path, Access.GA_ReadOnly))
                {
                    if (ds == null)
                        throw new Exception($"Error GDAl {Path}");

                    using (Band gdalBand = ds.GetRasterBand(_bandIndex))
                    {
                        int arraySize = _width * _height;
                        _values = new float[arraySize];

                        CPLErr err = gdalBand.ReadRaster(0, 0, _width, _height, _values, _width, _height, 0, 0);

                        if (err != CPLErr.CE_None)
                            throw new Exception($"Error GDAl {_bandIndex}. Code: {err}");
                    }
                }

                CalculateMinMax();
            }
            catch (Exception ex)
            {
                _values = null;
                throw new ApplicationException($"Error GDAl {_name}: {ex.Message}", ex);
            }
        }

        public void Unload()
        {
            lock (_lockObj)
            {
                _values = null;
            }
        }

        public void CalculateMinMax()
        {
            float[]? data = Values;

            if (data is null || data.Length == 0)
                return;

            if (_ignoreZero)
            {
                var nonZeroValues = data.AsParallel().Where(v => v != 0);

                if (nonZeroValues.Any())
                {
                    _minimum = nonZeroValues.Min();
                    _maximum = nonZeroValues.Max();
                }
                else
                {
                    _minimum = 0;
                    _maximum = 0;
                }
            }
            else
            {
                _minimum = data.AsParallel().Min();
                _maximum = data.AsParallel().Max();
            }
        }

        public void Dispose()
        {
            if (_isDisposed) return;

            _values = null;

            _isDisposed = true;
            GC.SuppressFinalize(this);
        }

        public override string ToString() => Name;
    }
}
