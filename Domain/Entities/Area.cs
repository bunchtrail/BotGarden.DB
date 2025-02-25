using System;
using System.Collections.Generic;
using NetTopologySuite.Geometries;

namespace Domain.Entities
{
    public class Area
    {
        public int AreaId { get; set; }
        public string Name { get; set; } // Название области
        public string Description { get; set; } // Описание области
        
        // Координаты для отображения на карте (для обратной совместимости)
        public double Latitude { get; set; } // Широта центра области
        public double Longitude { get; set; } // Долгота центра области
        
        // Геометрия области для PostGIS
        public Polygon Boundary { get; set; } // Полигон границы области
        
        // Связь с экспозицией (если область относится к определенной экспозиции)
        public int? ExpositionId { get; set; }
        public Exposition Exposition { get; set; }
        
        // Навигационное свойство для растений в этой области
        public ICollection<Plant> Plants { get; set; }
    }
} 