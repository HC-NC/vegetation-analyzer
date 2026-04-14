# План реализации модуля классификации растительности

## Обзор

Цель: добавить возможность вычисления вегетационных индексов, их классификации по типам растительности и экспорта результатов с палитрой.

---

## Фаза 1: Вегетационные индексы

### Поддерживаемые индексы

| Индекс | Формула | Каналы | Диапазон |
|---|---|---|---|
| **NDVI** | (NIR - Red) / (NIR + Red) | NIR, Red | -1.0 ... +1.0 |
| **EVI** | 2.5 × (NIR - Red) / (NIR + 6×Red - 7.5×Blue + 1) | NIR, Red, Blue | -1.0 ... +1.0 |
| **EVI2** | 2.5 × (NIR - Red) / (NIR + 2.4×Red + 1) | NIR, Red | -1.0 ... +1.0 |
| **SAVI** | ((NIR - Red) / (NIR + Red + L)) × (1 + L), L=0.5 | NIR, Red | -1.0 ... +1.0 |
| **NDWI** (вода) | (Green - NIR) / (Green + NIR) | Green, NIR | -1.0 ... +1.0 |
| **NDWI** (влажность) | (NIR - SWIR) / (NIR + SWIR) | NIR, SWIR | -1.0 ... +1.0 |
| **NDMI** | (NIR - SWIR1) / (NIR + SWIR1) | NIR, SWIR1 | -1.0 ... +1.0 |
| **GNDVI** | (NIR - Green) / (NIR + Green) | NIR, Green | -1.0 ... +1.0 |
| **MSAVI** | (2×NIR + 1 - √((2×NIR+1)² - 8×(NIR-Red))) / 2 | NIR, Red | -1.0 ... +1.0 |
| **ARVI** | (NIR - (2×Red - Blue)) / (NIR + (2×Red - Blue)) | NIR, Red, Blue | -1.0 ... +1.0 |

### DataClasses

- `VegetationIndex` — enum с типами индексов
- `IndexDefinition` — метаданные индекса:
  - Название, описание
  - Формула (строка)
  - Требуемые каналы (NIR, Red, Green, Blue, SWIR)
  - Диапазон значений (min, max)
  - Ссылка на функцию вычисления
- `IndexRaster` — результат вычисления:
  - `float[]` значений индекса
  - Привязка к исходному `RasterData`
  - GeoTransform, Projection (наследуется)
  - Минимальные/максимальные значения

### UI

- ПКМ по `RasterData` → **"Compute Vegetation Index"**
- Форма `ComputeIndexForm`:
  - Выбор индекса из списка (с описанием)
  - Маппинг каналов: для каждого требуемого канала (NIR, Red, ...) выбрать из доступных в растре
  - Авто-маппинг для Landsat/Sentinel (если определяются по именам)
- Результат появляется в TreeView как дочерний узел растра:
  ```
  📁 Landsat_Scene
  ├── Band 2
  ├── Band 3
  ├── Band 4
  ├── Band 5
  └── 📊 NDVI
  ```

### Визуализация

- Float-растр индекса отображается с цветовой шкалой (color ramp)
- Default: красно-жёлто-зелёный (как в QGIS)
- Настраиваемый min/max для нормализации

---

## Фаза 2: Классификация

### Система пресетов

Пресеты формируются по схеме `{Index} - {Region/Biome}`:

#### NDVI
| Пресет | Классы |
|---|---|
| **NDVI — Standard (NASA/USGS)** | 7 классов: Water, Bare Soil, Sparse, Low/Mod, Dense, Very Dense |
| **NDVI — Temperate** | Умеренный пояс, стандартные пороги |
| **NDVI — Tropical** | Тропики, порог dense > 0.7 |
| **NDVI — Boreal/Taiga** | Тайга, сниженные пороги для хвойных |
| **NDVI — Arid/Semi-Arid** | Пустыня/полупустыня, смещённые пороги |
| **NDVI — Agricultural** | Сельхозугодья, сезонные фазы |

#### SAVI
| Пресет | Классы |
|---|---|
| **SAVI — Standard** | 5 классов: Bare < 0.15, Sparse 0.15-0.33, Mod 0.33-0.50, Dense 0.50-0.70, Very Dense > 0.70 |

#### EVI
| Пресет | Классы |
|---|---|
| **EVI — Standard** | 4 класса: Bare < 0.1, Sparse 0.1-0.2, Mod 0.2-0.4, Dense > 0.4 |

#### NDWI
| Пресет | Классы |
|---|---|
| **NDWI — Water Bodies** | Water > 0.3, Wet 0.1-0.3, Dry < 0.1 |
| **NDWI — Vegetation Moisture** | Stress < 0.0, Low 0.0-0.2, Mod 0.2-0.4, High > 0.4 |

### DataClasses

- `ClassificationClass` — один класс:
  - Название (string)
  - Min, Max (float)
  - Color (Color)
  - Описание (опционально)
- `ClassificationScheme` — набор классов:
  - Название (например "NDVI - Standard")
  - `VegetationIndex` тип индекса
  - `Region/Biome` регион
  - `List<ClassificationClass>`
  - IsCustom (флаг пользовательского пресета)
- `ClassifiedRaster` — результат классификации:
  - `byte[]` — каждый пиксель = номер класса (0 = no data)
  - Привязка к `IndexRaster`
  - `ClassificationScheme` использованная для классификации
  - ColorTable для отображения

### UI

- ПКМ по `IndexRaster` → **"Classify..."**
- Форма `ClassifyForm`:
  - Выбор пресета (dropdown: Index × Region)
  - Таблица классов:
    - Цвет (color picker)
    - Название
    - Min, Max (редактируемые)
    - Кнопки: добавить, удалить, переместить
  - Кнопки: "Применить", "Сохранить как пресет", "Отмена"
  - Preview: миниатюра с применённой палитрой
- Результат в TreeView:
  ```
  📁 Landsat_Scene
  ├── 📊 NDVI
  │   └── 🏷️ NDVI_Classified (Temperate)
  └── 📊 SAVI
      └── 🏷️ SAVI_Classified (Standard)
  ```

---

## Фаза 3: Экспорт

### Поддерживаемые форматы

- **GeoTIFF** — основной формат
  - Float растры (индексы) — Float32
  - Классифицированные растры — Byte + ColorTable
- **PNG** — только для визуализации (без геоданных)
- **GTiff с сжатием** — LZW, DEFLATE

### Экспорт классифицированного растра

1. Создаётся Byte-растр со значениями классов (1, 2, 3, ...)
2. Применяется `ColorTable` (GDAL `PaletteInterp.GPI_RGB`)
3. Сохраняются GeoTransform и Projection
4. Записывается metadata:
   - INDEX_TYPE=NDVI
   - SCHEME_NAME=NDVI - Temperate
   - CLASS_COUNT=6
   - CLASS_1_NAME=Water, CLASS_1_RANGE=-0.1-0.0, ...

### UI

- ПКМ по любому растру/индексу/классификации → **"Export to File..."**
- Форма `ExportForm`:
  - SaveFileDialog (GeoTIFF, PNG)
  - Опции:
    - Включить палитру (для классифицированных)
    - Сжатие (None, LZW, DEFLATE)
    - Для float-растров: масштабирование (Byte или Float32)

---

## Итоговая структура TreeView

```
📁 Landsat_Scene (растр из папки)
├── Band 2 (Blue)
├── Band 3 (Green)
├── Band 4 (Red)
├── Band 5 (NIR)
├── Band 6 (SWIR1)
├── Band 7 (SWIR2)
├── 📊 NDVI                  ← IndexRaster (float)
│   ├── 🏷️ NDVI_Temperate   ← ClassifiedRaster (byte + palette)
│   └── 🏷️ NDVI_Tropical    ← ClassifiedRaster
├── 📊 SAVI
│   └── 🏷️ SAVI_Standard
└── 📊 NDWI (Moisture)
    └── 🏷️ NDWI_Vegetation
```

---

## Технические заметки

### GDAL ColorTable для GeoTIFF

```csharp
ColorTable colorTable = new ColorTable(PaletteInterp.GPI_RGB);
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
band.SetRasterColorInterpretation(ColorInterp.GCI_PaletteIndex);
```

### Ограничения

- GeoTIFF с палитрой — только Byte (8-bit)
- Float-растры нельзя напрямую с палитрой
- Workflow: float index → classify → byte raster → color table → GeoTIFF

### Performance

- Вычисление индексов: `Parallel.For` по строкам
- Классификация: lookup table или binary search
- Unsafe code для быстрой записи в Bitmap

---

## Порядок реализации

1. ~~Удалить `ClassificationData.cs`~~ 
2. Создать `VegetationIndex` enum + `IndexDefinition`
3. Создать `IndexRaster` класс
4. Создать `ComputeIndexForm`
5. Интегрировать в Main (контекстное меню)
6. Создать систему пресетов (`ClassificationScheme`)
7. Создать `ClassifiedRaster`
8. Создать `ClassifyForm`
9. Интегрировать в Main
10. Реализовать экспорт (`ExportForm` + сервис)
11. Сохранение/загрузка пользовательских пресетов (JSON)
