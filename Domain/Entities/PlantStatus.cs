using System;

namespace Domain.Entities
{
    public class PlantStatus
    {
        public int PlantStatusId { get; set; }
        
        // Внешний ключ
        public int PlantId { get; set; }
        public Plant Plant { get; set; } // Навигационное свойство
        
        public string Status { get; set; } // Например: "Живое", "Мертвое", "Пересаженное", "Болеет" и т.д.
        public DateTime StatusDate { get; set; } // Дата установки статуса
        public string Comment { get; set; } // Комментарий к статусу
        
        // Кто установил статус
        public int UpdatedById { get; set; }
        public User UpdatedBy { get; set; } // Навигационное свойство
    }
} 