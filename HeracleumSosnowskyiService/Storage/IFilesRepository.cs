namespace HeracleumSosnowskyiService.Storage
{
    public interface IFilesRepository
    {
        Task<string> AddFile(Stream fileStream, CancellationToken canellationToken);
        Task<Stream> GetFile(HttpResponse response, string id, CancellationToken canellationToken);
    }
}
