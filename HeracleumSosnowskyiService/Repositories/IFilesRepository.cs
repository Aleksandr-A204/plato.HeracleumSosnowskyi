using HeracleumSosnowskyiService.Models;
using Microsoft.EntityFrameworkCore;
using MongoDB.Bson;
using MongoDB.Driver.GridFS;

namespace HeracleumSosnowskyiService.Repositories
{
    public interface IFilesRepository
    {
        Task<IEnumerable<FileInfoApi>> GetAllFileInfoAsync();
        Task<bool> TryAddAsync(FileInfoApi fileInfo);
        Task<FileInfoApi> GetFileInfoByIdAsync(Ulid id);
        Task<FileInfoApi> GetFileInfoByIdAsync(string id);
        Task<bool> SaveAsync();
        Task<bool> TryUpdateAsync(FileInfoApi fileInfo);
        Task<SatelliteDataOfSpacesystem> FindOrInsertAsync(string landsatProductId);
        Task<string> UploadFileStreamAsync(string filename, Stream source);
        Task<byte[]> DownloadAsBytesAsync(ObjectId id);
        Task<GridFSDownloadStream<ObjectId>> DownloadFileStreamAsync(ObjectId id);
        //Task<GridFSDownloadStream<ObjectId>> DownloadStreamAsync(ObjectId id);

    }
}
