using HeracleumSosnowskyiService.Helpers;
using HeracleumSosnowskyiService.Models;
using HeracleumSosnowskyiService.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using MongoDB.Driver;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace HeracleumSosnowskyiService.Controllers
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

        [Description("Находит файл по id и возвращает информацию о файле")]
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetById(string id, CancellationToken ct)
        {
            if (!ValidationHelper.IsUlidValid(id))
                return BadRequest("Ошибка запроса при получении файла по индентификатору id. Индентификатор некорректный.");

            if (!_memoryCache.TryGetValue<FileInfoApi>($"file{id}", out var fileInfo))
                fileInfo = await _repository.GetFileInfoByIdAsync(Ulid.Parse(id), ct);


            return fileInfo != null ? Ok(fileInfo) : NotFound("Ошибка репозитории при получении файла по индентификатору id. Не найдено информации о файле.");
        }

        [Description("Создавает новый файл и возвращает идентификатор для загрузки на сервер")]
        [HttpPost]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateFile([FromBody] FileInfoApi fileInfo, CancellationToken ct)
        {
            if (await _repository.TryAddAsync(fileInfo, ct))
                _memoryCache.Set($"file{fileInfo.Id}", fileInfo, new MemoryCacheEntryOptions
                {
                    AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(1),
                    Size = 16
                });
            else
                return NotFound("Ошибка при добавлении данных о файле в БД.");

            return CreatedAtAction(nameof(GetById), new { id = fileInfo.Id }, new { fileId = fileInfo.Id });
        }

        [Description("Загружает файл и возвращает идентификатор спутниковых данных для обнаружения зоны произрастения Борщевик сосновского.")]
        [HttpPut]
        [Consumes("application/octet-stream")]
        [Route("upload/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Upload(
            [Description("Идентификатор загрузки полученный при вызове CreateFile")] 
            [Required] string id,
            CancellationToken ct
            )
        {
            if (!ValidationHelper.IsUlidValid(id))
                return BadRequest("Ошибка запроса при загрузке файла. Полученный при вызове CreateFile идентификатор загрузки файла некорректный.");

            if (Request.ContentType != "application/octet-stream")
                return BadRequest($"Content-Type: {Request.ContentType} is not supported");

            if (_memoryCache.TryGetValue<FileInfoApi>($"file{id}", out var fileInfo))
                _memoryCache.Remove($"file{id}");
            else
                fileInfo = await _repository.GetFileInfoByIdAsync(Ulid.Parse(id), ct);

            if (fileInfo == null)
                return NotFound("Ошибка запроса при загрузке файла. Не найдено полученной при вызове CreateFile информации о файле.");

            fileInfo.FileStreamId = await _repository.UploadOrUpdateFileStreamAsync(id, fileInfo.FileName, Request.Body, ct);

            return await _repository.TryUpdateAsync(fileInfo, ct) ? Ok(new { id = fileInfo.FileStreamId }) : NotFound("Не удалось загрузить файл.");
        }
    }
}
