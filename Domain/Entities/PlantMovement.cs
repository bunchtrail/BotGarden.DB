using System;
using NetTopologySuite.Geometries;

namespace Domain.Entities
{
    public class PlantMovement
    {
        public int PlantMovementId { get; set; }
        
        // Внешний ключ
        public int PlantId { get; set; }
        public Plant Plant { get; set; } // Навигационное свойство
        
        // Откуда переместили
        public int? SourceAreaId { get; set; }
        public Area SourceArea { get; set; } // Навигационное свойство
        
        // Куда переместили
        public int? DestinationAreaId { get; set; }
        public Area DestinationArea { get; set; } // Навигационное свойство
        
        // Старые и новые координаты
        public Point OldLocation { get; set; }
        public Point NewLocation { get; set; }
        
        public DateTime MovementDate { get; set; } // Дата перемещения
        public string Reason { get; set; } // Причина перемещения
        public string Comment { get; set; } // Комментарий
        
        // Кто переместил растение
        public int MovedById { get; set; }
        public User MovedBy { get; set; } // Навигационное свойство
    }
} 