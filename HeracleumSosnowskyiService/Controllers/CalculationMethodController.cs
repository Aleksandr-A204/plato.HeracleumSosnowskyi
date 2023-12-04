using Microsoft.AspNetCore.Mvc;
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
            return View();
        }

        [HttpPost]
        public void Post()
        {
            //Console.WriteLine(Directory.GetCurrentDirectory());
            

            Process.Start("script.bat");
        }
    }
}
