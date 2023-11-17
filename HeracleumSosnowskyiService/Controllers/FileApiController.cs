using HeracleumSosnowskyiService.Interfaces;
using HeracleumSosnowskyiService.Models;
using HeracleumSosnowskyiService.Storage;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;

namespace HeracleumSosnowskyiService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FileApiController : Controller
    {
        private readonly FileStreamRepository _fileStreamRepository;
        private readonly FileInfoRepository _fileInfoRepository;

        public FileApiController(FileStreamRepository fileStreamRepository, FileInfoRepository fileInfoRepository)
        {
            _fileStreamRepository = fileStreamRepository;
            _fileInfoRepository = fileInfoRepository;
        }

        [HttpGet]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Get() =>
            Ok(await _fileInfoRepository.GetAllAsync());

        [HttpPost]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateFile([FromBody] FileApi newFileInfo)
        {
            await _fileInfoRepository.CreateAsync(newFileInfo);

            return Ok(new { fileId = newFileInfo.Id });
        }

        [HttpPut]
        [Consumes("application/octet-stream")]
        [Route("upload/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Upload(string? id)
        {
            if (!ObjectId.TryParse(id, out _))
                return BadRequest("Ошибка запроса при загрузке файла. Invalid id");

            if (Request.ContentType != "application/octet-stream")
                return BadRequest($"Content-Type: {Request.ContentType} is not supported");

            var fileId = await _fileStreamRepository.CreateAsync(Request.Body);

            return Ok(fileId);
        }
    }
}
