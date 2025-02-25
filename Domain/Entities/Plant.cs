using System;
using System.Collections.Generic;
using NetTopologySuite.Geometries;

namespace Domain.Entities
{
    public class Plant
    {
        public int PlantId { get; set; }
        public string InventoryNumber { get; set; } // Уникальный
        
        // Внешние ключи
        public int PlantFamilyId { get; set; }
        public PlantFamily PlantFamily { get; set; } // Навигационное свойство
        
        public int ExpositionId { get; set; }
        public Exposition Exposition { get; set; } // Навигационное свойство
        
        // Связь с областью на карте
        public int? AreaId { get; set; }
        public Area Area { get; set; } // Навигационное свойство
        
        // Таксономические данные
        public string Genus { get; set; } // Род
        public string Species { get; set; } // Вид
        public string Cultivar { get; set; } // Сорт
        public string Form { get; set; } // Форма, если есть
        
        // Дополнительная информация
        public int? YearPlanted { get; set; } // Год посадки
        public string Origin { get; set; } // Происхождение
        public string NaturalRange { get; set; } // Ареал
        public string EcologyBiology { get; set; } // Экология и биология
        public string Usage { get; set; } // Хозяйственное применение
        public string ConservationStatus { get; set; } // Охранный статус
        
        // Геолокация (для обратной совместимости)
        public double? Latitude { get; set; } // Координаты для карты
        public double? Longitude { get; set; } // Координаты для карты
        
        // Геометрия для PostGIS
        public Point Location { get; set; } // Точка расположения растения
        
        public string Note { get; set; } // Примечание
        
        // Кто создал запись
        public int CreatedById { get; set; }
        public User CreatedBy { get; set; } // Навигационное свойство
        
        // Навигационные свойства для связанных сущностей
        public ICollection<PhenologyRecord> PhenologyRecords { get; set; }
        public ICollection<BiometryRecord> BiometryRecords { get; set; }
        public ICollection<PlantImage> PlantImages { get; set; }
        public ICollection<PlantStatus> PlantStatuses { get; set; }
        public ICollection<PlantMovement> PlantMovements { get; set; }
    }
}
