using HeracleumSosnowskyiService.Helpers;
using HeracleumSosnowskyiService.Models;
using HeracleumSosnowskyiService.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using MongoDB.Driver;
using System.Collections.Generic;
using System.ComponentModel;

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
        [Route("fileinfo")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetFileInfoAll()
        {
            if (!_memoryCache.TryGetValue<IEnumerable<FileInfoApi>>("filesInfo", out var filesInfo))
            {
                filesInfo = await _repository.GetAllFileInfoAsync();
                _memoryCache.Set($"filesInfo", filesInfo, new MemoryCacheEntryOptions
                {
                    AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(30)
                });
            }
            return Ok(filesInfo);
        }

        [HttpGet]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetAll()
        {
            IEnumerable<FileMetadata> metadata = await _repository.GetAllAsync();

            return Ok(metadata);
        }

        [Description("Найдет файл по id и возвращает информацию о файле")]
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetById(string id)
        {
            if (!ValidationHelper.IsIdValid(id))
                return BadRequest("Ошибка запроса при получении файла по индентификатору id. Индентификатор некорректный.");

            if (!_memoryCache.TryGetValue<FileInfoApi>($"file{id}", out var fileInfo))
                fileInfo = await _repository.GetFileInfoByIdAsync(Ulid.Parse(id));


            return fileInfo != null ? Ok(fileInfo) : NotFound("Ошибка репозитории при получении файла по индентификатору id. Не найдено информации о файле.");
        }

        [Description("Создает новый файл и возвращает идентификатор для загрузки на сервер")]
        [HttpPost]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateFile([FromBody] FileInfoApi fileInfo)
        {
            var landsatPoductId = fileInfo.FileName?.Remove(fileInfo.FileName.Length - 10);

            var metadata = new FileMetadata
            {
                FileInfo = fileInfo,
                SatelliteData = await _repository.FindOrInsertAsync(landsatPoductId)
            };

            if (await _repository.TryAddAsync(metadata))
                _memoryCache.Set($"file{fileInfo.Id}", fileInfo, new MemoryCacheEntryOptions
                {
                    AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(1),
                    Size = 16
                });
            else
                return NotFound("Ошибка при добавлении данных о файле в БД.");

            return CreatedAtAction(nameof(GetById), new { id = fileInfo.Id }, new { fileId = fileInfo.Id });
        }

        [Description("Загружает файл и возвращает идентификатор для обнаружения зоны произрастения Борщевик сосновского.")]
        [HttpPut]
        [Consumes("application/octet-stream")]
        [Route("upload/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Upload(
            [Description("Идентификатор загрузки полученный при вызове CreateFile")] string id
            )
        {
            if (!ValidationHelper.IsIdValid(id))
                return BadRequest("Ошибка запроса при загрузке файла. Полученный при вызове CreateFile идентификатор загрузки файла некорректный.");

            if (Request.ContentType != "application/octet-stream")
                return BadRequest($"Content-Type: {Request.ContentType} is not supported");

            if (_memoryCache.TryGetValue<FileInfoApi>($"file{id}", out var fileInfo))
                _memoryCache.Remove($"file{id}");
            else
                fileInfo = await _repository.GetFileInfoByIdAsync(Ulid.Parse(id));

            if (fileInfo?.FileName == null && fileInfo?.Metadata == null)
                return NotFound("Ошибка запроса при загрузке файла. Не найдено полученной при вызове CreateFile информации о файле.");

            fileInfo.Metadata.FileStreamId = await _repository.UploadFileStreamAsync(fileInfo.FileName, Request.Body);

            return await _repository.TryUpdateAsync(fileInfo.Metadata) ? Ok(new { id = fileInfo.Metadata.SatelliteDataId }) : NotFound("Не удалось загрузить файл.");
        }
    }
}
