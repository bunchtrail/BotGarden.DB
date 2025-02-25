using System;

namespace Domain.Entities
{
    public class PlantImage
    {
        public int PlantImageId { get; set; }
        
        // Внешний ключ
        public int PlantId { get; set; }
        public Plant Plant { get; set; } // Навигационное свойство
        
        public string ImagePath { get; set; } // Путь к файлу изображения
        public string Caption { get; set; } // Подпись к изображению
        public DateTime UploadDate { get; set; } // Дата загрузки
        
        // Кто загрузил изображение
        public int UploadedById { get; set; }
        public User UploadedBy { get; set; } // Навигационное свойство
    }
} 