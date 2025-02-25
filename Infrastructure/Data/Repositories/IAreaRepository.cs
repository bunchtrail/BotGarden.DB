using Domain.Entities;
using NetTopologySuite.Geometries;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Infrastructure.Data.Repositories
{
    public interface IAreaRepository : IRepository<Area>
    {
        // Получение областей по экспозиции
        Task<IEnumerable<Area>> GetAreasByExpositionAsync(int expositionId);
        
        // Получение областей, которые пересекаются с указанным полигоном
        Task<IEnumerable<Area>> GetAreasIntersectingWithPolygonAsync(Polygon polygon);
        
        // Получение областей, которые содержат указанную точку
        Task<IEnumerable<Area>> GetAreasContainingPointAsync(Point point);
        
        // Получение областей с растениями
        Task<IEnumerable<Area>> GetAreasWithPlantsAsync();
        
        // Получение областей с количеством растений
        Task<Dictionary<int, int>> GetPlantCountByAreaAsync();
    }
} 