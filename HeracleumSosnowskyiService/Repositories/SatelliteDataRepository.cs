using HeracleumSosnowskyiService.Data.PostgreSQL;
using HeracleumSosnowskyiService.Models;
using Microsoft.EntityFrameworkCore;

namespace HeracleumSosnowskyiService.Repositories
{
    public class SatelliteDataRepository : ISatelliteDataRepository
    {
        private readonly PostgreSQLDbContext _context;

        public SatelliteDataRepository(PostgreSQLDbContext context)
        {
            _context = context;
        }

        public async Task<SatelliteDataOfSpacesystem> GetByIdAsync(Ulid id) => await _context.SatelliteData.FirstOrDefaultAsync(s => s.Id == id) ?? new();
    }
}
