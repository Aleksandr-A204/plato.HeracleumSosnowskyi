using HeracleumSosnowskyiService.Data;
using HeracleumSosnowskyiService.Helpers;
using HeracleumSosnowskyiService.Interfaces;
using HeracleumSosnowskyiService.Models;
using HeracleumSosnowskyiService.Services;
using HeracleumSosnowskyiService.Storage;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using MongoDB.Bson;
using MongoDB.Driver;
using System.ComponentModel;
using System.IO;

namespace HeracleumSosnowskyiService.Controllers.v1
{
    [ApiController]
    [Route("[controller]")]
    public class FileApiController : Controller
    {
        private readonly IFilesRepository _repository;
        private readonly IMemoryCache _memoryCache;
        private readonly ICachingService _cachingService;

        public FileApiController(IFilesRepository repository, IMemoryCache memoryCache, ICachingService cachingService)
        {
            _repository = repository;
            _memoryCache = memoryCache;
            _cachingService = cachingService;
        }

        [HttpGet]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        public IActionResult GetAll()
        {
            //var fInfoList = await _repository _context.FilesInfo.Find(_ => true).ToListAsync();

            return Ok();
        }

        [HttpPost]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateFile([FromBody] FileInfoApi fileInfo)
        {
            var res = _memoryCache.GetOrCreate("file", entry =>
            {
                entry.SetAbsoluteExpiration(TimeSpan.FromMinutes(5));
                return fileInfo;
            });

            fileInfo.Id = res?.Id ?? ObjectId.GenerateNewId().ToString();

            //await _repository.CreateFileInfoAsync(fileInfo);

            return ValidationHelper.IsIdValid(fileInfo.Id) ? Ok(new { fileId = fileInfo.Id }) : 
                NotFound("Ошибка запроса при выборе файла. Идентификатор выбранного файла некорректный.");
        }

        [HttpPut]
        [Consumes("application/octet-stream")]
        [Route("upload/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Upload(
            [Description("Идентификатор загрузки полученный при вызове CreateFile")] string id
            )
        {
            if (Request.ContentType != "application/octet-stream")
                return BadRequest($"Content-Type: {Request.ContentType} is not supported");

            if (!ValidationHelper.IsIdValid(id))
                return BadRequest("Ошибка запроса при загрузке файла. Полученный при вызове CreateFile идентификатор загрузки файла некорректный.");

            var res = _memoryCache.Get<FileInfoApi>("file");

            res.FileStreamId = await _repository.CreateFileStreamAsync(Request.Body);

            await _repository.CreateFileInfoAsync(res);

            //var fileId = await _context.GridFilesStream.UploadFromStreamAsync("fileTest", Request.Body);

            //return ObjectId.TryParse(fileId.ToString(), out _) ? Ok(fileId) : BadRequest();

            return Ok();
        }
    }
}
