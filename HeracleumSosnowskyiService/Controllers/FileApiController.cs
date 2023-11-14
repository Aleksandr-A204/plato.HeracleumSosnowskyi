using HeracleumSosnowskyiService.Storage;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using MongoDB.Driver;
using System.ComponentModel;
using System.Globalization;
using System.Net.Sockets;
using System.Web;

namespace HeracleumSosnowskyiService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FileApiController : Controller
    {
        private readonly IConfiguration _configuration;

        public FileApiController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpGet]
        [Produces("application/octet-stream")]
        [Route("file")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> ListFile(CancellationToken canellationToken)
        {
            IFilesRepository filesRepository = new FilesRepository(_configuration);

            var fileStream = await filesRepository.GetFile(Response, "test", canellationToken);

            return new FileStreamResult(fileStream, "application/octet-stream");
        }

        [HttpPost]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        public IActionResult CreateFile()
        {
            var headers = Request.Headers.Where(header =>
                CultureInfo.InvariantCulture.TextInfo.ToTitleCase(header.Key).StartsWith("Plato")).ToDictionary(header =>
                CultureInfo.InvariantCulture.TextInfo.ToTitleCase(header.Key), header => HttpUtility.UrlDecode(header.Value.ToString()));

            if (headers.Count == 0)
                return BadRequest("Ошибка при отправке запросов. Возможно, отсутствуют заголовки запросов с префиксом \"Plato\"");

            var newId = ObjectId.GenerateNewId().ToString();

            return Ok(newId);
        }

        [HttpPut]
        [Consumes("application/octet-stream")]
        [Route("upload/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Upload(string? id)
        {
            if (!ObjectId.TryParse(id, out _))
                return BadRequest("Invalid id");

            if (Request.ContentType != "application/octet-stream")
                return BadRequest($"Content-Type: {Request.ContentType} is not supported");

            var dataStream = Request.Body;

            //IFilesRepository repository = new FilesRepository(_configuration);

            //var fileInfo = await repository.UpdateFile(HttpContext);

            //var fileId = await repository.AddFile(dataStream, canellationToken);

            //var uploadPath = $"{Directory.GetCurrentDirectory()}/upload";
            //Directory.CreateDirectory(uploadPath);

            //var fullPath = $"{uploadPath}/{file.FileName}";

            //using (var fileStream = new FileStream(fullPath, FileMode.Create))
            //{
            //    await file.CopyToAsync(fileStream);
            //}

            //foreach (var file in files)
            //        await file.CopyToAsync(new FileStream($"{uploadPath}/{file.FileName}", FileMode.Create), canellationToken);

            return Ok("Successfuly");
        }
    }
}
