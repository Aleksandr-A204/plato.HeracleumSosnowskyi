using HeracleumSosnowskyiService.Models;
using Microsoft.EntityFrameworkCore;
using MongoDB.Bson;
using MongoDB.Driver.GridFS;

namespace HeracleumSosnowskyiService.Repositories
{
    public interface IFilesRepository
    {
        Task<bool> TryAddAsync(FileInfoApi fileInfo, CancellationToken ct);
        Task<FileInfoApi> GetFileInfoByIdAsync(Ulid id, CancellationToken ct);
        Task<bool> TryUpdateAsync(FileInfoApi fileInfo, CancellationToken ct);
        Task<string> UploadOrUpdateFileStreamAsync(string id, string filename, Stream source, CancellationToken ct);
        Task<byte[]> DownloadAsBytesAsync(ObjectId id);
        Task<GridFSDownloadStream<ObjectId>> DownloadFileStreamAsync(ObjectId id);
    }
}
