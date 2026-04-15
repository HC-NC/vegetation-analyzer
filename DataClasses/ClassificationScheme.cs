using System.Drawing;

namespace vegetation_analyzer.DataClasses
{
    /// <summary>
    /// Один класс классификации (название, диапазон, цвет).
    /// </summary>
    public class ClassificationClass
    {
        public string Name { get; set; } = "";
        public float Min { get; set; }
        public float Max { get; set; }
        public Color Color { get; set; } = Color.White;
        public string Description { get; set; } = "";

        public ClassificationClass() { }

        public ClassificationClass(string name, float min, float max, Color color, string description = "")
        {
            Name = name;
            Min = min;
            Max = max;
            Color = color;
            Description = description;
        }

        public ClassificationClass Clone() => new ClassificationClass(Name, Min, Max, Color, Description);
    }

    /// <summary>
    /// Набор классов классификации для определённого индекса и региона.
    /// </summary>
    public class ClassificationScheme
    {
        public string Name { get; set; } = "";
        public VegetationIndex IndexType { get; set; }
        public string Region { get; set; } = "";
        public List<ClassificationClass> Classes { get; set; } = new();
        public bool IsCustom { get; set; }

        public ClassificationScheme() { }

        public ClassificationScheme Clone()
        {
            return new ClassificationScheme
            {
                Name = Name,
                IndexType = IndexType,
                Region = Region,
                Classes = Classes.Select(c => c.Clone()).ToList(),
                IsCustom = IsCustom
            };
        }

        /// <summary>
        /// Находит номер класса для заданного значения (1-based), 0 = no data.
        /// </summary>
        public byte GetClassIndex(float value)
        {
            for (int i = 0; i < Classes.Count; i++)
            {
                if (value >= Classes[i].Min && value <= Classes[i].Max)
                    return (byte)(i + 1);
            }
            return 0; // no data / out of range
        }
    }

    /// <summary>
    /// Статический реестр пресетов классификации.
    /// </summary>
    public static class ClassificationPresets
    {
        public static List<ClassificationScheme> GetDefaultPresets()
        {
            var presets = new List<ClassificationScheme>
            {
                // ===== NDVI =====
                new ClassificationScheme
                {
                    Name = "NDVI — Standard (NASA/USGS)",
                    IndexType = VegetationIndex.NDVI,
                    Region = "Standard",
                    Classes = new List<ClassificationClass>
                    {
                        new("Water", -1.0f, -0.1f, Color.FromArgb(0, 0, 128), "Open water"),
                        new("Bare Soil", -0.1f, 0.0f, Color.FromArgb(194, 170, 134), "Water/Snow/Ice"),
                        new("Sparse Vegetation", 0.0f, 0.1f, Color.FromArgb(214, 194, 134), "Bare soil/Rock"),
                        new("Low/Moderate", 0.1f, 0.2f, Color.FromArgb(214, 214, 104), "Sparse/Shrubland"),
                        new("Moderate", 0.2f, 0.4f, Color.FromArgb(194, 214, 104), "Grassland/Crops"),
                        new("Dense", 0.4f, 0.6f, Color.FromArgb(114, 194, 44), "Healthy crops/Forest"),
                        new("Very Dense", 0.6f, 1.0f, Color.FromArgb(34, 134, 34), "Tropical rainforest")
                    }
                },
                new ClassificationScheme
                {
                    Name = "NDVI — Temperate",
                    IndexType = VegetationIndex.NDVI,
                    Region = "Temperate",
                    Classes = new List<ClassificationClass>
                    {
                        new("Water", -1.0f, 0.0f, Color.FromArgb(0, 0, 160), "Water bodies"),
                        new("No Vegetation", 0.0f, 0.1f, Color.FromArgb(180, 180, 180), "Urban/Rock"),
                        new("Sparse", 0.1f, 0.25f, Color.FromArgb(220, 220, 100), "Sparse vegetation"),
                        new("Moderate", 0.25f, 0.4f, Color.FromArgb(180, 220, 80), "Grassland/Crops"),
                        new("Dense", 0.4f, 0.6f, Color.FromArgb(100, 180, 40), "Forest"),
                        new("Very Dense", 0.6f, 1.0f, Color.FromArgb(20, 120, 20), "Dense forest")
                    }
                },
                new ClassificationScheme
                {
                    Name = "NDVI — Tropical",
                    IndexType = VegetationIndex.NDVI,
                    Region = "Tropical",
                    Classes = new List<ClassificationClass>
                    {
                        new("Water", -1.0f, 0.0f, Color.FromArgb(0, 0, 128), "Water"),
                        new("Bare/Urban", 0.0f, 0.15f, Color.FromArgb(194, 170, 134), "Urban/Bare soil"),
                        new("Sparse", 0.15f, 0.3f, Color.FromArgb(214, 214, 104), "Degraded/Sparse"),
                        new("Moderate", 0.3f, 0.5f, Color.FromArgb(194, 214, 104), "Agricultural"),
                        new("Dense", 0.5f, 0.7f, Color.FromArgb(114, 194, 44), "Secondary forest"),
                        new("Very Dense", 0.7f, 1.0f, Color.FromArgb(34, 134, 34), "Primary rainforest")
                    }
                },
                new ClassificationScheme
                {
                    Name = "NDVI — Boreal/Taiga",
                    IndexType = VegetationIndex.NDVI,
                    Region = "Boreal",
                    Classes = new List<ClassificationClass>
                    {
                        new("Water", -1.0f, 0.0f, Color.FromArgb(0, 0, 128), "Water"),
                        new("No Vegetation", 0.0f, 0.1f, Color.FromArgb(180, 180, 180), "Rock/Tundra"),
                        new("Sparse", 0.1f, 0.2f, Color.FromArgb(200, 200, 120), "Open forest"),
                        new("Moderate", 0.2f, 0.35f, Color.FromArgb(160, 200, 80), "Sparse coniferous"),
                        new("Dense", 0.35f, 0.5f, Color.FromArgb(100, 170, 40), "Coniferous forest"),
                        new("Very Dense", 0.5f, 1.0f, Color.FromArgb(30, 110, 30), "Dense taiga")
                    }
                },
                new ClassificationScheme
                {
                    Name = "NDVI — Arid/Semi-Arid",
                    IndexType = VegetationIndex.NDVI,
                    Region = "Arid",
                    Classes = new List<ClassificationClass>
                    {
                        new("Water", -1.0f, 0.0f, Color.FromArgb(0, 0, 128), "Water"),
                        new("Bare Desert", 0.0f, 0.05f, Color.FromArgb(210, 190, 140), "Sand/Desert"),
                        new("Very Sparse", 0.05f, 0.1f, Color.FromArgb(220, 210, 140), "Extremely sparse"),
                        new("Sparse", 0.1f, 0.2f, Color.FromArgb(210, 210, 110), "Shrubland"),
                        new("Moderate", 0.2f, 0.35f, Color.FromArgb(180, 210, 90), "Irrigated crops"),
                        new("Dense", 0.35f, 1.0f, Color.FromArgb(80, 170, 40), "Oasis/Riparian")
                    }
                },

                // ===== SAVI =====
                new ClassificationScheme
                {
                    Name = "SAVI — Standard",
                    IndexType = VegetationIndex.SAVI,
                    Region = "Standard",
                    Classes = new List<ClassificationClass>
                    {
                        new("Bare Soil", -1.0f, 0.15f, Color.FromArgb(194, 170, 134), "Bare soil"),
                        new("Sparse Vegetation", 0.15f, 0.33f, Color.FromArgb(214, 214, 104), "Sparse/Shrubland"),
                        new("Moderate", 0.33f, 0.5f, Color.FromArgb(194, 214, 104), "Grassland"),
                        new("Dense", 0.5f, 0.7f, Color.FromArgb(114, 194, 44), "Forest"),
                        new("Very Dense", 0.7f, 1.5f, Color.FromArgb(34, 134, 34), "Very dense vegetation")
                    }
                },

                // ===== EVI =====
                new ClassificationScheme
                {
                    Name = "EVI — Standard",
                    IndexType = VegetationIndex.EVI,
                    Region = "Standard",
                    Classes = new List<ClassificationClass>
                    {
                        new("Bare/Urban", -1.0f, 0.1f, Color.FromArgb(180, 180, 180), "Bare soil/Urban"),
                        new("Sparse", 0.1f, 0.2f, Color.FromArgb(214, 214, 104), "Sparse vegetation"),
                        new("Moderate", 0.2f, 0.4f, Color.FromArgb(160, 200, 80), "Moderate vegetation"),
                        new("Dense", 0.4f, 1.0f, Color.FromArgb(34, 134, 34), "Dense vegetation")
                    }
                },

                // ===== NDWI Water =====
                new ClassificationScheme
                {
                    Name = "NDWI — Water Bodies",
                    IndexType = VegetationIndex.NDWI_Water,
                    Region = "Standard",
                    Classes = new List<ClassificationClass>
                    {
                        new("Dry Soil", -1.0f, 0.1f, Color.FromArgb(180, 180, 180), "Dry soil/Urban"),
                        new("Wet Soil", 0.1f, 0.3f, Color.FromArgb(120, 160, 200), "Wet soil"),
                        new("Water", 0.3f, 1.0f, Color.FromArgb(0, 80, 180), "Water bodies")
                    }
                },

                // ===== NDWI Moisture =====
                new ClassificationScheme
                {
                    Name = "NDWI — Vegetation Moisture",
                    IndexType = VegetationIndex.NDWI_Moisture,
                    Region = "Standard",
                    Classes = new List<ClassificationClass>
                    {
                        new("Water Stress", -1.0f, 0.0f, Color.FromArgb(180, 80, 80), "Water stress/Dry"),
                        new("Low Moisture", 0.0f, 0.2f, Color.FromArgb(220, 180, 100), "Low moisture"),
                        new("Moderate", 0.2f, 0.4f, Color.FromArgb(160, 200, 80), "Moderate moisture"),
                        new("High Moisture", 0.4f, 1.0f, Color.FromArgb(34, 134, 34), "High moisture")
                    }
                },

                // ===== NDMI =====
                new ClassificationScheme
                {
                    Name = "NDMI — Standard",
                    IndexType = VegetationIndex.NDMI,
                    Region = "Standard",
                    Classes = new List<ClassificationClass>
                    {
                        new("Very Dry", -1.0f, 0.0f, Color.FromArgb(180, 80, 80), "Very dry/Dead"),
                        new("Dry", 0.0f, 0.15f, Color.FromArgb(220, 160, 80), "Dry vegetation"),
                        new("Moderate", 0.15f, 0.3f, Color.FromArgb(180, 200, 80), "Moderate moisture"),
                        new("High", 0.3f, 0.5f, Color.FromArgb(100, 180, 40), "Healthy vegetation"),
                        new("Very High", 0.5f, 1.0f, Color.FromArgb(34, 134, 34), "Very high moisture")
                    }
                },

                // ===== GNDVI =====
                new ClassificationScheme
                {
                    Name = "GNDVI — Standard",
                    IndexType = VegetationIndex.GNDVI,
                    Region = "Standard",
                    Classes = new List<ClassificationClass>
                    {
                        new("No Vegetation", -1.0f, 0.1f, Color.FromArgb(180, 180, 180), "Bare/Urban"),
                        new("Low Chlorophyll", 0.1f, 0.3f, Color.FromArgb(220, 210, 110), "Low chlorophyll"),
                        new("Moderate", 0.3f, 0.5f, Color.FromArgb(160, 200, 80), "Moderate chlorophyll"),
                        new("High", 0.5f, 0.7f, Color.FromArgb(100, 180, 40), "High chlorophyll"),
                        new("Very High", 0.7f, 1.0f, Color.FromArgb(34, 134, 34), "Very high chlorophyll")
                    }
                }
            };

            return presets;
        }

        /// <summary>
        /// Получить пресеты для конкретного индекса.
        /// </summary>
        public static List<ClassificationScheme> GetPresetsForIndex(VegetationIndex index)
        {
            return GetDefaultPresets().Where(p => p.IndexType == index).ToList();
        }

        /// <summary>
        /// Получить первый подходящий пресет для индекса.
        /// </summary>
        public static ClassificationScheme? GetDefaultForIndex(VegetationIndex index)
        {
            return GetDefaultPresets().FirstOrDefault(p => p.IndexType == index);
        }
    }
}
