using HeracleumSosnowskyiService.Models;
using MongoDB.Bson;
using MongoDB.Driver.GridFS;

namespace HeracleumSosnowskyiService.Interfaces
{
    public interface IFilesRepository
    {
        Task CreateFileInfoAsync(FileInfoApi newFileInfo, CancellationToken cancellationToken);
        Task<ObjectId> UploadFileStreamAsync(string filename, Stream newFileStream, CancellationToken cancellationToken);
        Task<GridFSDownloadStream<ObjectId>> DouwloadFileStreamAsync(string filename);
        Task<bool> DeleteFile(string id);
        Task<IEnumerable<FileInfoApi>> GetAllAsync();
        Task<FileInfoApi> GetFileInfoByIdAsync(string id, CancellationToken cancellationToken);
        Task UpdateFileInfoAsync(string id, ObjectId fsId);
    }
}
