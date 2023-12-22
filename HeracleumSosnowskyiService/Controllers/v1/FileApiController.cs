using Amazon.Runtime.Internal.Util;
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
using System.Threading;

namespace HeracleumSosnowskyiService.Controllers.v1
{
    [ApiController]
    [Route("[controller]")]
    public class FileApiController : Controller
    {
        private readonly IFilesRepository _repository;
        private readonly IMemoryCache _memoryCache;
        private readonly ILogger<FileApiController> _logger;

        public FileApiController(IFilesRepository repository, IMemoryCache memoryCache, ILogger<FileApiController> logger)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _memoryCache = memoryCache ?? throw new ArgumentNullException(nameof(memoryCache));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetAll()
        {
            if (!_memoryCache.TryGetValue<IEnumerable<FileInfoApi>>("filesInfo", out var filesInfo))
            {
                filesInfo = await _repository.GetAllAsync();
                _memoryCache.Set($"filesInfo", filesInfo, new MemoryCacheEntryOptions
                {
                    AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(30)
                });
            }

            return Ok(filesInfo);
        }

        [Description("Найдет файл по id и возвращает информацию о файле")]
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetFileInfoById(string id, CancellationToken cancellationToken)
        {
            if (!ValidationHelper.IsIdValid(id))
                return BadRequest();

            if (!_memoryCache.TryGetValue<FileInfoApi>($"file{id}", out var fileInfo))
                fileInfo = await _repository.GetFileInfoByIdAsync(id, cancellationToken);


            return fileInfo == null ? Ok(fileInfo) : NotFound("Не найдено информации о файле");
        }

        [Description("Создает новый файл и возвращает идентификатор для загрузки на сервер")]
        [HttpPost]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateFile([FromBody] FileInfoApi fileInfo, CancellationToken cancellationToken)
        {
            await _repository.CreateFileInfoAsync(fileInfo, cancellationToken);

            if (ValidationHelper.IsIdValid(fileInfo.Id))
                _memoryCache.Set($"file{fileInfo.Id}", fileInfo, new MemoryCacheEntryOptions
                {
                    AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(1),
                    Size = 16
                });
            else
                NotFound("Ошибка запроса при выборе файла. Идентификатор выбранного файла некорректный.");

            return CreatedAtAction(nameof(GetFileInfoById), new { id = fileInfo.Id }, new { fileId = fileInfo.Id });
        }

        [Description("Загружает файл и возвращает статус 200")]
        [HttpPut]
        [Consumes("application/octet-stream")]
        [Route("upload/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Upload(
            [Description("Идентификатор загрузки полученный при вызове CreateFile")] string id, CancellationToken cancellationToken
            )
        {
            if (Request.ContentType != "application/octet-stream")
                return BadRequest($"Content-Type: {Request.ContentType} is not supported");

            if (!ValidationHelper.IsIdValid(id))
                return BadRequest("Ошибка запроса при загрузке файла. Полученный при вызове CreateFile идентификатор загрузки файла некорректный.");

            if (_memoryCache.TryGetValue<FileInfoApi>($"file{id}", out var fileInfo))
                _memoryCache.Remove($"file{id}");
            else
                fileInfo = await _repository.GetFileInfoByIdAsync(id, cancellationToken);


            if (fileInfo?.FileName == null)
                return NotFound("Что-то пошло не так.");

            var newFsId = await _repository.UploadFileStreamAsync(fileInfo.FileName, Request.Body, cancellationToken);
            await _repository.UpdateFileInfoAsync(id, newFsId);


            return Ok();
        }
    }
}
