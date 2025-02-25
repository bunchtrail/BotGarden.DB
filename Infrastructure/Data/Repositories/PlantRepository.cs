using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using NetTopologySuite.Geometries;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Infrastructure.Data.Repositories
{
    public class PlantRepository : Repository<Plant>, IPlantRepository
    {
        public PlantRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<bool> AreaExistsAsync(int areaId)
        {
            return await _context.Areas.AnyAsync(a => a.AreaId == areaId);
        }

        public async Task<IEnumerable<Plant>> GetPlantsInAreaAsync(int areaId)
        {
            return await _context.Plants
                .Where(p => p.AreaId == areaId)
                .Include(p => p.PlantFamily)
                .Include(p => p.Exposition)
                .ToListAsync();
        }

        public async Task<IEnumerable<Plant>> GetPlantsInRadiusAsync(Point point, double radiusInMeters)
        {
            // Используем функцию ST_DWithin из PostGIS для поиска растений в радиусе
            return await _context.Plants
                .Where(p => p.Location.Distance(point) <= radiusInMeters)
                .Include(p => p.PlantFamily)
                .Include(p => p.Exposition)
                .Include(p => p.Area)
                .ToListAsync();
        }

        public async Task<IEnumerable<Plant>> GetPlantsInPolygonAsync(Polygon polygon)
        {
            // Используем функцию ST_Within из PostGIS для поиска растений внутри полигона
            return await _context.Plants
                .Where(p => polygon.Contains(p.Location))
                .Include(p => p.PlantFamily)
                .Include(p => p.Exposition)
                .Include(p => p.Area)
                .ToListAsync();
        }

        public async Task<IEnumerable<Plant>> GetPlantsByFamilyAsync(int familyId)
        {
            return await _context.Plants
                .Where(p => p.PlantFamilyId == familyId)
                .Include(p => p.Exposition)
                .Include(p => p.Area)
                .ToListAsync();
        }

        public async Task<IEnumerable<Plant>> GetPlantsByExpositionAsync(int expositionId)
        {
            return await _context.Plants
                .Where(p => p.ExpositionId == expositionId)
                .Include(p => p.PlantFamily)
                .Include(p => p.Area)
                .ToListAsync();
        }

        public async Task<IEnumerable<Plant>> GetFilteredPlantsAsync(
            int? familyId = null, 
            int? expositionId = null, 
            int? areaId = null, 
            string genus = null, 
            string species = null, 
            string cultivar = null, 
            int? yearPlanted = null)
        {
            var query = _context.Plants.AsQueryable();

            // Применяем фильтры
            if (familyId.HasValue)
                query = query.Where(p => p.PlantFamilyId == familyId.Value);

            if (expositionId.HasValue)
                query = query.Where(p => p.ExpositionId == expositionId.Value);

            if (areaId.HasValue)
                query = query.Where(p => p.AreaId == areaId.Value);

            if (!string.IsNullOrEmpty(genus))
                query = query.Where(p => p.Genus.Contains(genus));

            if (!string.IsNullOrEmpty(species))
                query = query.Where(p => p.Species.Contains(species));

            if (!string.IsNullOrEmpty(cultivar))
                query = query.Where(p => p.Cultivar.Contains(cultivar));

            if (yearPlanted.HasValue)
                query = query.Where(p => p.YearPlanted == yearPlanted.Value);

            // Включаем связанные данные
            return await query
                .Include(p => p.PlantFamily)
                .Include(p => p.Exposition)
                .Include(p => p.Area)
                .ToListAsync();
        }
    }
} 