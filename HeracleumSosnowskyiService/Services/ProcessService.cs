using HeracleumSosnowskyiService.Interfaces;
using System.Diagnostics;

namespace HeracleumSosnowskyiService.Services
{
    public class ProcessService : IProcessService
    {
        public async Task RunCmdLineAsync(string arguments)
        {
            // Populate process information
            var processInfo = new ProcessStartInfo("cmd.exe", $"/c test.bat \"{arguments}\"");

            processInfo.CreateNoWindow = true;
            processInfo.UseShellExecute = false;
            processInfo.RedirectStandardOutput = true;
            processInfo.RedirectStandardError = true;

            var process = Process.Start(processInfo);

            Console.WriteLine("Started process PID: " + process.Id);

            process.OutputDataReceived += (object sender, DataReceivedEventArgs e) =>
                Console.WriteLine("output >> " + e.Data);
            process.BeginOutputReadLine();

            process.ErrorDataReceived += (object sender, DataReceivedEventArgs e) =>
                Console.WriteLine("error >> " + e.Data);
            process.BeginErrorReadLine();

            await process.WaitForExitAsync();
        }
    }
}
