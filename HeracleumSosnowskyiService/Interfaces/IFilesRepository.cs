namespace HeracleumSosnowskyiService.Interfaces
{
    public interface IFilesRepository
    {
        Task<bool> CreateAsync(Stream newFileStream);
        Task<bool> DeleteFile(string id);
        Task<Stream> GetFileById(string id);
        Task<bool> UpdateFile(Stream fileStream);
    }
}
