using HeracleumSosnowskyiService.Models;
using MongoDB.Bson;

namespace HeracleumSosnowskyiService.Interfaces
{
    public interface IFilesRepository
    {
        Task<string> CreateFileStreamAsync(Stream newFileStream);
        Task CreateFileInfoAsync(FileInfoApi newFileInfo);
        Task<bool> DeleteFile(string id);
        Task<FileInfoApi> GetFileInfoById(string id);
        Task<bool> UpdateFile(Stream fileStream);
        Task<bool> IsIdEqualsAsync(string id);
    }
}
