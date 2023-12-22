namespace HeracleumSosnowskyiService.Interfaces
{
    public interface IProcessService
    {
        Task RunCmdLineAsync(string argumets = "test.bat");
    }
}
