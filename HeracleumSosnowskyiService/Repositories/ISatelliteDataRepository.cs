using HeracleumSosnowskyiService.Models;

namespace HeracleumSosnowskyiService.Repositories
{
    public interface ISatelliteDataRepository
    {
        Task<SatelliteDataOfSpacesystem> GetByIdAsync(Ulid id);
    }
}
