using HeracleumSosnowskyiService.Models;
using MongoDB.Bson;
using MongoDB.Driver.GridFS;

namespace HeracleumSosnowskyiService.Repositories
{
    public interface IDatasetsRepository
    {
        Task<IEnumerable<Dataset>> GetAllAsync();
        Task<Dataset> GetByIdAsync(Ulid id);
        Task<GridFSDownloadStream<ObjectId>> DouwloadFileStreamAsync(ObjectId fileId);
    }
}
