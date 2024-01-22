using HeracleumSosnowskyiService.Models;
using Microsoft.EntityFrameworkCore;
using MongoDB.Bson;
using MongoDB.Driver.GridFS;

namespace HeracleumSosnowskyiService.Repositories
{
    public interface IFilesRepository
    {
        Task<IEnumerable<Dataset>> GetAllAsync();
        Task<IEnumerable<FileInfoApi>> GetAllFileInfoAsync();
        Task<bool> TryAddAsync(Dataset datasets);
        Task<FileInfoApi> GetFileInfoByIdAsync(Ulid id);
        Task<bool> SaveAsync();
        Task<bool> TryUpdateAsync(Dataset datasets);
        Task<SatelliteDataOfSpacesystem> FindOrInsertAsync(string landsatProductId);
        Task<string> UploadFileStreamAsync(string filename, Stream source);
    }
}
