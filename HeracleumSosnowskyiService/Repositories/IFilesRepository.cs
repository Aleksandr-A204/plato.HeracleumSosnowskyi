using HeracleumSosnowskyiService.Models;
using Microsoft.EntityFrameworkCore;
using MongoDB.Bson;
using MongoDB.Driver.GridFS;

namespace HeracleumSosnowskyiService.Repositories
{
    public interface IFilesRepository
    {
        Task<IEnumerable<FileMetadata>> GetAllAsync();
        Task<IEnumerable<FileInfoApi>> GetAllFileInfoAsync();
        Task<bool> TryAddAsync(FileMetadata metadata);
        Task<FileInfoApi> GetFileInfoByIdAsync(Ulid id);
        Task<bool> SaveAsync();
        Task<bool> TryUpdateAsync(FileMetadata metadata);
        Task<SatelliteDataOfSpacesystem> FindOrInsertAsync(string landsatProductId);
        Task<string> UploadFileStreamAsync(string filename, Stream source);
        Task<GridFSDownloadStream<ObjectId>> DouwloadFileStreamAsync(string filename);

    }
}
