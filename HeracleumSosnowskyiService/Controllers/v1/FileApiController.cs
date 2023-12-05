using Amazon.Runtime.Internal.Util;
using HeracleumSosnowskyiService.Data;
using HeracleumSosnowskyiService.Helpers;
using HeracleumSosnowskyiService.Interfaces;
using HeracleumSosnowskyiService.Models;
using HeracleumSosnowskyiService.Services;
using HeracleumSosnowskyiService.Storage;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using MongoDB.Bson;
using MongoDB.Driver;
using System.ComponentModel;
using System.IO;
using static System.Net.WebRequestMethods;

namespace HeracleumSosnowskyiService.Controllers.v1
{
    [ApiController]
    [Route("[controller]")]
    public class FileApiController : Controller
    {
        private readonly IFilesRepository _repository;
        private readonly IMemoryCache _memoryCache;

        public FileApiController(IFilesRepository repository, IMemoryCache memoryCache)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _memoryCache = memoryCache ?? throw new ArgumentNullException(nameof(memoryCache));
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
            await _repository.CreateFileInfoAsync(fileInfo);

            _memoryCache.Set("file", fileInfo, new MemoryCacheEntryOptions { 
                AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(30),
                Size = 16
            });

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

            if (!_memoryCache.TryGetValue("file", out FileInfoApi? fileInfo))
                fileInfo = await _repository.GetFileInfoByIdAsync(id);


            if (fileInfo == null)
                return NotFound("Что-то пошло не так.");

            var newFsId = await _repository.CreateFileStreamAsync(fileInfo.FileName, Request.Body);
            await _repository.UpdateFileInfoAsync(id, newFsId);

            return Ok();
        }
    }
}
