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

        [HttpPut]
        public void StartProcess()
        {
            Process.Start("script.bat");
        }
    }
}
