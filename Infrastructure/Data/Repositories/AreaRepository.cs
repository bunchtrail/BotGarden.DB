using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using NetTopologySuite.Geometries;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Infrastructure.Data.Repositories
{
    public class AreaRepository : Repository<Area>, IAreaRepository
    {
        public AreaRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Area>> GetAreasByExpositionAsync(int expositionId)
        {
            return await _context.Areas
                .Where(a => a.ExpositionId == expositionId)
                .Include(a => a.Exposition)
                .ToListAsync();
        }

        public async Task<IEnumerable<Area>> GetAreasIntersectingWithPolygonAsync(Polygon polygon)
        {
            // Используем функцию ST_Intersects из PostGIS для поиска областей, пересекающихся с полигоном
            return await _context.Areas
                .Where(a => a.Boundary.Intersects(polygon))
                .Include(a => a.Exposition)
                .ToListAsync();
        }

        public async Task<IEnumerable<Area>> GetAreasContainingPointAsync(Point point)
        {
            // Используем функцию ST_Contains из PostGIS для поиска областей, содержащих точку
            return await _context.Areas
                .Where(a => a.Boundary.Contains(point))
                .Include(a => a.Exposition)
                .ToListAsync();
        }

        public async Task<IEnumerable<Area>> GetAreasWithPlantsAsync()
        {
            return await _context.Areas
                .Where(a => a.Plants.Any())
                .Include(a => a.Exposition)
                .Include(a => a.Plants)
                .ToListAsync();
        }

        public async Task<Dictionary<int, int>> GetPlantCountByAreaAsync()
        {
            var result = await _context.Areas
                .Select(a => new
                {
                    AreaId = a.AreaId,
                    PlantCount = a.Plants.Count
                })
                .ToDictionaryAsync(a => a.AreaId, a => a.PlantCount);

            return result;
        }
    }
} 