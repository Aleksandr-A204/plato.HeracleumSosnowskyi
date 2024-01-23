using HeracleumSosnowskyiService.Models;
using MongoDB.Bson;
using MongoDB.Driver.GridFS;

namespace HeracleumSosnowskyiService.Repositories
{
    public interface IDatasetsRepository
    {
        Task<IEnumerable<SatelliteDataOfSpacesystem>> GetSatellitesDataAsync();
        Task<IEnumerable<Dataset>> GetDatasetsBySatelliteDataIdAsync(Ulid id);
        Task<SatelliteDataOfSpacesystem> GetByIdAsync(Ulid id);
        Task<GridFSDownloadStream<ObjectId>> DouwloadFileStreamAsync(ObjectId fileId);
    }
}
