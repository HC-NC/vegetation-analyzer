namespace vegetation_analyzer.DataClasses
{
    /// <summary>
    /// Типы спектральных каналов, требуемых для вычисления индекса.
    /// </summary>
    public enum SpectralBandRole
    {
        Red,
        Green,
        Blue,
        NIR,
        SWIR,
        SWIR1,
        SWIR2
    }

    /// <summary>
    /// Вегетационные индексы.
    /// </summary>
    public enum VegetationIndex
    {
        NDVI,
        EVI,
        EVI2,
        SAVI,
        NDWI_Water,       // NDWI для водных объектов (Green, NIR)
        NDWI_Moisture,    // NDWI для влажности растительности (NIR, SWIR)
        NDMI,
        GNDVI,
        MSAVI,
        ARVI
    }

    /// <summary>
    /// Метаданные и формула вегетационного индекса.
    /// </summary>
    public static class IndexDefinition
    {
        public static string GetName(VegetationIndex index) => index switch
        {
            VegetationIndex.NDVI => "NDVI",
            VegetationIndex.EVI => "EVI",
            VegetationIndex.EVI2 => "EVI2",
            VegetationIndex.SAVI => "SAVI",
            VegetationIndex.NDWI_Water => "NDWI (Water)",
            VegetationIndex.NDWI_Moisture => "NDWI (Moisture)",
            VegetationIndex.NDMI => "NDMI",
            VegetationIndex.GNDVI => "GNDVI",
            VegetationIndex.MSAVI => "MSAVI",
            VegetationIndex.ARVI => "ARVI",
            _ => throw new ArgumentOutOfRangeException(nameof(index))
        };

        public static string GetDescription(VegetationIndex index) => index switch
        {
            VegetationIndex.NDVI => "Normalized Difference Vegetation Index. Самый распространённый индекс для оценки плотности растительности.",
            VegetationIndex.EVI => "Enhanced Vegetation Index. Улучшенный индекс с коррекцией атмосферного влияния и фона почвы.",
            VegetationIndex.EVI2 => "EVI без синего канала. Требует только NIR и Red.",
            VegetationIndex.SAVI => "Soil Adjusted Vegetation Index. Коррекция влияния яркости почвы (L=0.5).",
            VegetationIndex.NDWI_Water => "Normalized Difference Water Index (McFeeters). Для выделения водных объектов.",
            VegetationIndex.NDWI_Moisture => "NDWI влажности растительности (Gao). Для оценки содержания воды в растительности.",
            VegetationIndex.NDMI => "Normalized Difference Moisture Index. Аналог NDWI для оценки влажности.",
            VegetationIndex.GNDVI => "Green NDVI. Использует зелёный канал вместо красного, более чувствителен к хлорофиллу.",
            VegetationIndex.MSAVI => "Modified SAVI. Автоматическая коррекция влияния почвы без параметра L.",
            VegetationIndex.ARVI => "Atmospherically Resistant Vegetation Index. Устойчив к атмосферным влияниям.",
            _ => ""
        };

        public static string GetFormula(VegetationIndex index) => index switch
        {
            VegetationIndex.NDVI => "(NIR - Red) / (NIR + Red)",
            VegetationIndex.EVI => "2.5 × (NIR - Red) / (NIR + 6×Red - 7.5×Blue + 1)",
            VegetationIndex.EVI2 => "2.5 × (NIR - Red) / (NIR + 2.4×Red + 1)",
            VegetationIndex.SAVI => "((NIR - Red) / (NIR + Red + 0.5)) × 1.5",
            VegetationIndex.NDWI_Water => "(Green - NIR) / (Green + NIR)",
            VegetationIndex.NDWI_Moisture => "(NIR - SWIR) / (NIR + SWIR)",
            VegetationIndex.NDMI => "(NIR - SWIR1) / (NIR + SWIR1)",
            VegetationIndex.GNDVI => "(NIR - Green) / (NIR + Green)",
            VegetationIndex.MSAVI => "(2×NIR + 1 - √((2×NIR+1)² - 8×(NIR-Red))) / 2",
            VegetationIndex.ARVI => "(NIR - (2×Red - Blue)) / (NIR + (2×Red - Blue))",
            _ => ""
        };

        public static SpectralBandRole[] GetRequiredBands(VegetationIndex index) => index switch
        {
            VegetationIndex.NDVI => new[] { SpectralBandRole.NIR, SpectralBandRole.Red },
            VegetationIndex.EVI => new[] { SpectralBandRole.NIR, SpectralBandRole.Red, SpectralBandRole.Blue },
            VegetationIndex.EVI2 => new[] { SpectralBandRole.NIR, SpectralBandRole.Red },
            VegetationIndex.SAVI => new[] { SpectralBandRole.NIR, SpectralBandRole.Red },
            VegetationIndex.NDWI_Water => new[] { SpectralBandRole.Green, SpectralBandRole.NIR },
            VegetationIndex.NDWI_Moisture => new[] { SpectralBandRole.NIR, SpectralBandRole.SWIR },
            VegetationIndex.NDMI => new[] { SpectralBandRole.NIR, SpectralBandRole.SWIR1 },
            VegetationIndex.GNDVI => new[] { SpectralBandRole.NIR, SpectralBandRole.Green },
            VegetationIndex.MSAVI => new[] { SpectralBandRole.NIR, SpectralBandRole.Red },
            VegetationIndex.ARVI => new[] { SpectralBandRole.NIR, SpectralBandRole.Red, SpectralBandRole.Blue },
            _ => Array.Empty<SpectralBandRole>()
        };

        /// <summary>
        /// Вычисляет значение индекса для одного пикселя.
        /// bandValues — словарь: роль канала → значение отражения.
        /// </summary>
        public static float Compute(VegetationIndex index, Dictionary<SpectralBandRole, float> bandValues)
        {
            return index switch
            {
                VegetationIndex.NDVI => ComputeNDVI(bandValues),
                VegetationIndex.EVI => ComputeEVI(bandValues),
                VegetationIndex.EVI2 => ComputeEVI2(bandValues),
                VegetationIndex.SAVI => ComputeSAVI(bandValues),
                VegetationIndex.NDWI_Water => ComputeNDWI_Water(bandValues),
                VegetationIndex.NDWI_Moisture => ComputeNDWI_Moisture(bandValues),
                VegetationIndex.NDMI => ComputeNDMI(bandValues),
                VegetationIndex.GNDVI => ComputeGNDVI(bandValues),
                VegetationIndex.MSAVI => ComputeMSAVI(bandValues),
                VegetationIndex.ARVI => ComputeARVI(bandValues),
                _ => float.NaN
            };
        }

        private static float ComputeNDVI(Dictionary<SpectralBandRole, float> v)
        {
            float nir = v[SpectralBandRole.NIR];
            float red = v[SpectralBandRole.Red];
            float denom = nir + red;
            if (denom == 0) return 0;
            return (nir - red) / denom;
        }

        private static float ComputeEVI(Dictionary<SpectralBandRole, float> v)
        {
            float nir = v[SpectralBandRole.NIR];
            float red = v[SpectralBandRole.Red];
            float blue = v[SpectralBandRole.Blue];
            float denom = nir + 6 * red - 7.5f * blue + 1;
            if (denom <= 0) return 0;
            float result = 2.5f * (nir - red) / denom;
            // Ограничиваем EVI разумным диапазоном — знаменатель может быть мал
            return Math.Clamp(result, -1f, 1f);
        }

        private static float ComputeEVI2(Dictionary<SpectralBandRole, float> v)
        {
            float nir = v[SpectralBandRole.NIR];
            float red = v[SpectralBandRole.Red];
            float denom = nir + 2.4f * red + 1;
            if (denom <= 0) return 0;
            float result = 2.5f * (nir - red) / denom;
            return Math.Clamp(result, -1f, 1f);
        }

        private static float ComputeSAVI(Dictionary<SpectralBandRole, float> v)
        {
            const float L = 0.5f;
            float nir = v[SpectralBandRole.NIR];
            float red = v[SpectralBandRole.Red];
            float denom = nir + red + L;
            if (denom == 0) return 0;
            return ((nir - red) / denom) * (1 + L);
        }

        private static float ComputeNDWI_Water(Dictionary<SpectralBandRole, float> v)
        {
            float green = v[SpectralBandRole.Green];
            float nir = v[SpectralBandRole.NIR];
            float denom = green + nir;
            if (denom == 0) return 0;
            return (green - nir) / denom;
        }

        private static float ComputeNDWI_Moisture(Dictionary<SpectralBandRole, float> v)
        {
            float nir = v[SpectralBandRole.NIR];
            float swir = v[SpectralBandRole.SWIR];
            float denom = nir + swir;
            if (denom == 0) return 0;
            return (nir - swir) / denom;
        }

        private static float ComputeNDMI(Dictionary<SpectralBandRole, float> v)
        {
            float nir = v[SpectralBandRole.NIR];
            float swir1 = v[SpectralBandRole.SWIR1];
            float denom = nir + swir1;
            if (denom == 0) return 0;
            return (nir - swir1) / denom;
        }

        private static float ComputeGNDVI(Dictionary<SpectralBandRole, float> v)
        {
            float nir = v[SpectralBandRole.NIR];
            float green = v[SpectralBandRole.Green];
            float denom = nir + green;
            if (denom == 0) return 0;
            return (nir - green) / denom;
        }

        private static float ComputeMSAVI(Dictionary<SpectralBandRole, float> v)
        {
            float nir = v[SpectralBandRole.NIR];
            float red = v[SpectralBandRole.Red];
            float term = 2 * nir + 1;
            float disc = term * term - 8 * (nir - red);
            if (disc < 0) disc = 0;
            return (term - (float)Math.Sqrt(disc)) / 2;
        }

        private static float ComputeARVI(Dictionary<SpectralBandRole, float> v)
        {
            float nir = v[SpectralBandRole.NIR];
            float red = v[SpectralBandRole.Red];
            float blue = v[SpectralBandRole.Blue];
            float rb = 2 * red - blue;
            float denom = nir + rb;
            if (denom == 0) return 0;
            float result = (nir - rb) / denom;
            // ARVI обычно в пределах -1..+1
            return Math.Clamp(result, -1f, 1f);
        }

        /// <summary>
        /// Попытка авто-маппинга каналов для Landsat 8/9.
        /// Возвращает словарь: SpectralBandRole → индекс канала (0-based).
        /// </summary>
        public static Dictionary<SpectralBandRole, int>? AutoMapLandsat89(IReadOnlyList<BandData> bands)
        {
            // Landsat 8/9: B2=Blue, B3=Green, B4=Red, B5=NIR, B6=SWIR1, B7=SWIR2
            var nameMap = new Dictionary<string, int>(StringComparer.OrdinalIgnoreCase);
            for (int i = 0; i < bands.Count; i++)
                nameMap[bands[i].Name] = i;

            int? getBand(string pattern) => nameMap.Keys
                .FirstOrDefault(k => System.Text.RegularExpressions.Regex.IsMatch(k, pattern, System.Text.RegularExpressions.RegexOptions.IgnoreCase))
                is var key && key != null ? nameMap[key] : null;

            int? b2 = getBand(@"^Band\s*2$|B2|Blue");
            int? b3 = getBand(@"^Band\s*3$|B3|Green");
            int? b4 = getBand(@"^Band\s*4$|B4|Red");
            int? b5 = getBand(@"^Band\s*5$|B5|NIR");
            int? b6 = getBand(@"^Band\s*6$|B6|SWIR.?1");
            int? b7 = getBand(@"^Band\s*7$|B7|SWIR.?2");

            var result = new Dictionary<SpectralBandRole, int>();
            if (b4.HasValue) result[SpectralBandRole.Red] = b4.Value;
            if (b3.HasValue) result[SpectralBandRole.Green] = b3.Value;
            if (b2.HasValue) result[SpectralBandRole.Blue] = b2.Value;
            if (b5.HasValue) result[SpectralBandRole.NIR] = b5.Value;
            if (b6.HasValue) result[SpectralBandRole.SWIR1] = b6.Value;
            if (b7.HasValue) result[SpectralBandRole.SWIR2] = b7.Value;
            if (b6.HasValue) result[SpectralBandRole.SWIR] = b6.Value; // fallback

            return result.Count > 0 ? result : null;
        }

        /// <summary>
        /// Попытка авто-маппинга каналов для Sentinel-2.
        /// </summary>
        public static Dictionary<SpectralBandRole, int>? AutoMapSentinel2(IReadOnlyList<BandData> bands)
        {
            var nameMap = new Dictionary<string, int>(StringComparer.OrdinalIgnoreCase);
            for (int i = 0; i < bands.Count; i++)
                nameMap[bands[i].Name] = i;

            int? getBand(string pattern) => nameMap.Keys
                .FirstOrDefault(k => System.Text.RegularExpressions.Regex.IsMatch(k, pattern, System.Text.RegularExpressions.RegexOptions.IgnoreCase))
                is var key && key != null ? nameMap[key] : null;

            // Sentinel-2: B2=Blue, B3=Green, B4=Red, B8=NIR, B11=SWIR1, B12=SWIR2
            int? b2 = getBand(@"^Band\s*2$|B0?2$");
            int? b3 = getBand(@"^Band\s*3$|B0?3$");
            int? b4 = getBand(@"^Band\s*4$|B0?4$");
            int? b8 = getBand(@"^Band\s*8$|B0?8$");
            int? b11 = getBand(@"^Band\s*11$|B11$");
            int? b12 = getBand(@"^Band\s*12$|B12$");

            var result = new Dictionary<SpectralBandRole, int>();
            if (b2.HasValue) result[SpectralBandRole.Blue] = b2.Value;
            if (b3.HasValue) result[SpectralBandRole.Green] = b3.Value;
            if (b4.HasValue) result[SpectralBandRole.Red] = b4.Value;
            if (b8.HasValue) result[SpectralBandRole.NIR] = b8.Value;
            if (b11.HasValue) result[SpectralBandRole.SWIR1] = b11.Value;
            if (b12.HasValue) result[SpectralBandRole.SWIR2] = b12.Value;
            if (b11.HasValue) result[SpectralBandRole.SWIR] = b11.Value;

            return result.Count > 0 ? result : null;
        }
    }
}
