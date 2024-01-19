using HeracleumSosnowskyiService.Models;
using MongoDB.Bson;
using MongoDB.Driver.GridFS;

namespace HeracleumSosnowskyiService.Repositories
{
    public interface ISatelliteDataRepository
    {
        Task<IEnumerable<SatelliteDataOfSpacesystem>> GetAllAsync();
        Task<SatelliteDataOfSpacesystem> GetByIdAsync(Ulid id);
        Task<GridFSDownloadStream<ObjectId>> DouwloadFileStreamAsync(ObjectId fileId);
    }
}
