using System.Diagnostics;

namespace HeracleumSosnowskyiService.Services
{
    public static class ProcessService
    {
        public static async Task RunCmdLineAsync(string arguments, string assembly)
        {
            // Populate process information
            var processInfo = new ProcessStartInfo("cmd.exe", $"/c saga_runs_calc.bat \"{arguments}\" \"{assembly}\"");

            processInfo.CreateNoWindow = true;
            processInfo.UseShellExecute = false;
            processInfo.RedirectStandardOutput = true;
            processInfo.RedirectStandardError = true;

            var process = Process.Start(processInfo);

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
