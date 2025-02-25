using System;

namespace Domain.Entities
{
    public class BiometryRecord
    {
        public int BiometryRecordId { get; set; }
        
        // Внешний ключ
        public int PlantId { get; set; }
        public Plant Plant { get; set; } // Навигационное свойство
        
        public DateTime MeasureDate { get; set; } // Дата измерения
        
        // Биометрические показатели
        public double? HeightCm { get; set; } // Высота в сантиметрах
        public double? FlowerDiameter { get; set; } // Диаметр цветка
        public int? BudsCount { get; set; } // Количество бутонов
        
        public string Comment { get; set; } // Комментарий
    }
}
