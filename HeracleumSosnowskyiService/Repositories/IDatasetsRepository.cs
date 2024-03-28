using HeracleumSosnowskyiService.Models;
using MongoDB.Bson;
using MongoDB.Driver.GridFS;

namespace HeracleumSosnowskyiService.Repositories
{
    public interface IDatasetsRepository
    {
        Task<GridFSDownloadStream<ObjectId>> DownloadFileStreamAsync(ObjectId fileId);
        Task<byte[]> DownloadAsBytesAsync(ObjectId id);
        Task<bool> TryAddMetadataAsync(LandsatMetadata metadata);
        Task<bool> TryAddAsync(Dataset dataset);
        Task<FileInfoApi> GetInfoFileAsync(string id);
        Task<LandsatMetadata> GetMetadataAsync(string id);
        Task<bool> TryUpdateAsync(string id, Ulid ulid);
    }
}
