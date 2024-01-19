using HeracleumSosnowskyiService.Models;
using Microsoft.EntityFrameworkCore;
using MongoDB.Bson;
using MongoDB.Driver.GridFS;

namespace HeracleumSosnowskyiService.Repositories
{
    public interface IFilesRepository
    {
        Task<IEnumerable<Datasets>> GetAllAsync();
        Task<IEnumerable<FileInfoApi>> GetAllFileInfoAsync();
        Task<bool> TryAddAsync(Datasets datasets);
        Task<FileInfoApi> GetFileInfoByIdAsync(Ulid id);
        Task<bool> SaveAsync();
        Task<bool> TryUpdateAsync(Datasets datasets);
        Task<SatelliteDataOfSpacesystem> FindOrInsertAsync(string landsatProductId);
        Task<string> UploadFileStreamAsync(string filename, Stream source);
    }
}
