using Domain.Entities;
using NetTopologySuite.Geometries;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Infrastructure.Data.Repositories
{
    public interface IPlantRepository : IRepository<Plant>
    {
        // Получение растений в указанной области
        Task<IEnumerable<Plant>> GetPlantsInAreaAsync(int areaId);
        
        // Получение растений в радиусе от указанной точки
        Task<IEnumerable<Plant>> GetPlantsInRadiusAsync(Point point, double radiusInMeters);
        
        // Получение растений внутри указанного полигона
        Task<IEnumerable<Plant>> GetPlantsInPolygonAsync(Polygon polygon);
        
        // Получение растений по семейству
        Task<IEnumerable<Plant>> GetPlantsByFamilyAsync(int familyId);
        
        // Получение растений по экспозиции
        Task<IEnumerable<Plant>> GetPlantsByExpositionAsync(int expositionId);
        
        // Получение растений с фильтрацией по различным параметрам
        Task<IEnumerable<Plant>> GetFilteredPlantsAsync(
            int? familyId = null, 
            int? expositionId = null, 
            int? areaId = null, 
            string genus = null, 
            string species = null, 
            string cultivar = null, 
            int? yearPlanted = null);
    }
} 