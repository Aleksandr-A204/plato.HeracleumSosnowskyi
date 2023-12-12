using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using System;
using System.Diagnostics;

namespace HeracleumSosnowskyiService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CalculationMethodController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            return Ok();
        }

        [HttpPut]
        public async Task RunProcess()
        {
            // Populate process information
            var processInfo = new ProcessStartInfo("cmd.exe", "/c test.bat");
            //var processInfo = new ProcessStartInfo("cmd.exe") 
            //{
            //    Arguments = "/c test.bat" + " " + "\"D:\\1\""
            //};
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
