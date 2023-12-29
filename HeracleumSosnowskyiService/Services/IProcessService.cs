namespace HeracleumSosnowskyiService.Services
{
    public interface IProcessService
    {
        Task RunCmdLineAsync(string argumets = "test.bat");
    }
}
