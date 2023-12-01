using HeracleumSosnowskyiService.Models;
using MongoDB.Bson;

namespace HeracleumSosnowskyiService.Interfaces
{
    public interface IFilesRepository
    {
        Task<ObjectId> CreateFileStreamAsync(string filename, Stream newFileStream);
        Task CreateFileInfoAsync(FileInfoApi newFileInfo);
        Task<bool> DeleteFile(string id);
        Task<FileInfoApi> GetFileInfoByIdAsync(string id);
        Task UpdateFileInfoAsync(string id, ObjectId fsId);
    }
}
