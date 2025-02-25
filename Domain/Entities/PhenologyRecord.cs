using System;

namespace Domain.Entities
{
    public class PhenologyRecord
    {
        public int PhenologyRecordId { get; set; }
        
        // Внешний ключ
        public int PlantId { get; set; }
        public Plant Plant { get; set; } // Навигационное свойство
        
        public int ObservationYear { get; set; }
        
        // Даты фенологических фаз
        public DateTime? DateBudBurst { get; set; } // Распускание почек
        public DateTime? DateBloom { get; set; } // Начало цветения
        public DateTime? DateFruiting { get; set; } // Плодоношение
        public DateTime? DateLeafFall { get; set; } // Листопад
        
        public string Comment { get; set; } // Комментарий
    }
}
