using Microsoft.AspNetCore.Mvc;
using System.IO;
using System.Web;

namespace HeracleumSosnowskyiService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EarthRemoteSensingController : Controller
    {
        [HttpGet]
        public IActionResult Get()
        {
            return Ok("Test");
        }

        //[HttpPost]
        //public IActionResult OnPostUploadAsync(IEnumerable<HttpPostedFileBase> fileUpload)
        //{
        //    foreach (var file in fileUpload)
        //    {
        //        if (file == null) continue;
        //        string path = AppDomain.CurrentDomain.BaseDirectory + "UploadedFiles/";
        //        string filename = Path.GetFileName(file.FileName);
        //        if (filename != null) file.SaveAs(Path.Combine(path, filename));
        //    }

        //    return RedirectToAction("Index");

        //    //// путь к папке, где будут храниться файлы
        //    //var uploadPath = $"{Directory.GetCurrentDirectory()}/uploads";
        //    //// создаем папку для хранения файлов
        //    //Directory.CreateDirectory(uploadPath);

        //    //foreach (var file in files)
        //    //{
        //    //    // путь к папке uploads
        //    //    string fullPath = $"{uploadPath}/{file.FileName}";

        //    //    // сохраняем файл в папку uploads
        //    //    using (var fileStream = new FileStream(fullPath, FileMode.Create))
        //    //    {
        //    //        await file.CopyToAsync(fileStream);
        //    //    }
        //    //}

        //    //return Ok("Файлы успешно загружены");
        //}

        [HttpPost]
        public IActionResult Index(IFormFile file)
        {
            var size = file.Length;
            return View(size);
        }
    }
}
