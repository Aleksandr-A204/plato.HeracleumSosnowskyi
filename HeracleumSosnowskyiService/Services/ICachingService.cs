namespace HeracleumSosnowskyiService.Services
{
    public interface ICachingService
    {
        T GetData<T>(string key);
        bool SetData<T>(string key, T value, DateTimeOffset expirationTime);
        object RemoveData(string key);
    }
}
