using OSGeo.GDAL;
using System.Drawing;
using System.Drawing.Imaging;

namespace vegetation_analyzer.DataClasses
{
    public static class ExportService
    {
        /// <summary>
        /// Экспорт RasterData в GeoTIFF (многоканальный Float32).
        /// </summary>
        public static void ExportRasterData(RasterData raster, string filePath, string compression = "LZW")
        {
            Gdal.AllRegister();
            var driver = Gdal.GetDriverByName("GTiff");
            if (driver == null)
                throw new Exception("GTiff driver not available");

            var options = new[] { $"COMPRESS={compression}" };
            using (Dataset ds = driver.Create(filePath, raster.Width, raster.Height, raster.BandsCount, DataType.GDT_Float32, options))
            {
                if (ds == null)
                    throw new Exception($"Cannot create file: {filePath}");

                ds.SetGeoTransform(raster.GeoTransform);
                ds.SetProjection(raster.Projection);

                for (int i = 0; i < raster.BandsCount; i++)
                {
                    var band = raster.GetBand(i);
                    if (band == null) continue;

                    float[]? values = band.Values;
                    if (values == null) continue;

                    using (Band outBand = ds.GetRasterBand(i + 1))
                    {
                        CPLErr err = outBand.WriteRaster(0, 0, raster.Width, raster.Height, values, raster.Width, raster.Height, 0, 0);
                        if (err != CPLErr.CE_None)
                            throw new Exception($"Error writing band {i + 1}");

                        outBand.SetDescription(band.Name);
                    }

                    band.Unload();
                }
            }
        }

        /// <summary>
        /// Экспорт IndexRaster в GeoTIFF (Float32 или Byte).
        /// </summary>
        public static void ExportIndexRaster(IndexRaster indexRaster, string filePath, string compression = "LZW", bool asByte = false)
        {
            Gdal.AllRegister();
            var driver = Gdal.GetDriverByName("GTiff");
            if (driver == null)
                throw new Exception("GTiff driver not available");

            var options = new[] { $"COMPRESS={compression}" };
            DataType dataType = asByte ? DataType.GDT_Byte : DataType.GDT_Float32;

            using (Dataset ds = driver.Create(filePath, indexRaster.Width, indexRaster.Height, 1, dataType, options))
            {
                if (ds == null)
                    throw new Exception($"Cannot create file: {filePath}");

                ds.SetGeoTransform(indexRaster.GeoTransform);
                ds.SetProjection(indexRaster.Projection);

                ds.SetMetadataItem("INDEX_TYPE", IndexDefinition.GetName(indexRaster.IndexType), "VEGETATION_INDEX");
                ds.SetMetadataItem("FORMULA", IndexDefinition.GetFormula(indexRaster.IndexType), "VEGETATION_INDEX");
                ds.SetMetadataItem("DISPLAY_MIN", indexRaster.DisplayMin.ToString("F6"), "VEGETATION_INDEX");
                ds.SetMetadataItem("DISPLAY_MAX", indexRaster.DisplayMax.ToString("F6"), "VEGETATION_INDEX");
                ds.SetMetadataItem("SOURCE", indexRaster.SourceRaster.Name, "VEGETATION_INDEX");

                using (Band outBand = ds.GetRasterBand(1))
                {
                    if (asByte)
                    {
                        // Конвертируем float в byte [0..255]
                        float min = indexRaster.DisplayMin;
                        float max = indexRaster.DisplayMax;
                        float range = Math.Abs(max - min) < 0.0001f ? 1f : (max - min);
                        byte[] byteValues = new byte[indexRaster.Values.Length];

                        for (int i = 0; i < indexRaster.Values.Length; i++)
                        {
                            float v = indexRaster.Values[i];
                            if (float.IsNaN(v))
                                byteValues[i] = 0;
                            else
                                byteValues[i] = (byte)Math.Clamp(((v - min) / range) * 255, 0, 255);
                        }

                        CPLErr err = outBand.WriteRaster(0, 0, indexRaster.Width, indexRaster.Height, byteValues, indexRaster.Width, indexRaster.Height, 0, 0);
                        if (err != CPLErr.CE_None)
                            throw new Exception("Error writing raster data");
                    }
                    else
                    {
                        CPLErr err = outBand.WriteRaster(0, 0, indexRaster.Width, indexRaster.Height, indexRaster.Values, indexRaster.Width, indexRaster.Height, 0, 0);
                        if (err != CPLErr.CE_None)
                            throw new Exception("Error writing raster data");
                    }
                }
            }
        }

        /// <summary>
        /// Экспорт ClassifiedRaster в GeoTIFF с ColorTable.
        /// </summary>
        public static void ExportClassifiedRaster(ClassifiedRaster classified, string filePath, string compression = "LZW")
        {
            Gdal.AllRegister();
            var driver = Gdal.GetDriverByName("GTiff");
            if (driver == null)
                throw new Exception("GTiff driver not available");

            var options = new[] { $"COMPRESS={compression}" };
            using (Dataset ds = driver.Create(filePath, classified.Width, classified.Height, 1, DataType.GDT_Byte, options))
            {
                if (ds == null)
                    throw new Exception($"Cannot create file: {filePath}");

                ds.SetGeoTransform(classified.GeoTransform);
                ds.SetProjection(classified.Projection);

                using (Band band = ds.GetRasterBand(1))
                {
                    // Создаем ColorTable
                    ColorTable colorTable = new ColorTable(PaletteInterp.GPI_RGB);
                    var classes = classified.Scheme.Classes;

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

                    CPLErr err = band.WriteRaster(0, 0, classified.Width, classified.Height, classified.Values, classified.Width, classified.Height, 0, 0);
                    if (err != CPLErr.CE_None)
                        throw new Exception("Error writing raster data");
                }

                // Метаданные
                ds.SetMetadataItem("INDEX_TYPE", IndexDefinition.GetName(classified.SourceIndex.IndexType), "CLASSIFICATION");
                ds.SetMetadataItem("SCHEME_NAME", classified.Scheme.Name, "CLASSIFICATION");
                ds.SetMetadataItem("CLASS_COUNT", classified.Scheme.Classes.Count.ToString(), "CLASSIFICATION");

                for (int i = 0; i < classified.Scheme.Classes.Count; i++)
                {
                    var c = classified.Scheme.Classes[i];
                    ds.SetMetadataItem($"CLASS_{i + 1}_NAME", c.Name, "CLASSIFICATION");
                    ds.SetMetadataItem($"CLASS_{i + 1}_MIN", c.Min.ToString("F4"), "CLASSIFICATION");
                    ds.SetMetadataItem($"CLASS_{i + 1}_MAX", c.Max.ToString("F4"), "CLASSIFICATION");
                }
            }
        }
    }
}
