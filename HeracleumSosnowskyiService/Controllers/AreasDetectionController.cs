using HeracleumSosnowskyiService.Data;
using HeracleumSosnowskyiService.Helpers;
using HeracleumSosnowskyiService.Models;
using HeracleumSosnowskyiService.Repositories;
using HeracleumSosnowskyiService.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using MongoDB.Bson;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;


namespace HeracleumSosnowskyiService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AreasDetectionController : Controller
    {
        private readonly IDatasetsRepository _repository;
        private readonly IMemoryCache _memoryCache;

        private static string? _subdirPath { get; set; }
        private static LandsatMetadata? _metadata { get; set; }

        public AreasDetectionController(IDatasetsRepository repository, IMemoryCache memory)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository), $"The {nameof(FilesRepository)} cannot be NULL.");
            _memoryCache = memory ?? throw new ArgumentNullException(nameof(memory), $"The {nameof(MemoryCache)} cannot be NULL.");
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetSatellitesData()
        {
            //var satellitesData = await _repository.GetSatellitesDataAsync();

            return Ok();
        }

        [HttpPost]
        [Route("read/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Read(
            [Description("Идентификатор загрузки полученный при вызове FileApiController/Upload")]
            [Required] string id
            )
        {
            if (!ValidationHelper.IsBsonIdValid(id))
                return BadRequest("Ошибка запроса при загрузке файла. Полученный при вызове Upload идентификатор загрузки файла некорректный.");

            var fileBuffer = await _repository.DownloadAsBytesAsync(ObjectId.Parse(id));

            _metadata = XmlDeserializer.GetAllValues<LandsatMetadata>(fileBuffer);

            if (!await _repository.TryAddMetadataAsync(_metadata))
                return NotFound("Ошибка при добавлении данных о файле в БД.");

            //if (await _repository.TryAddMetadataAsync(metadata))
            //    _memoryCache.Set($"landsatMetadata{metadata.Id}", metadata, new MemoryCacheEntryOptions
            //    {
            //        AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(1),
            //        Size = 16
            //    });
            //else
            //    return NotFound("Ошибка при добавлении данных о файле в БД.");
             
            var fileInfoId = await _repository.GetInfoFileAsync(id);

            var dataset = new Dataset()
            {
                FileStreamId = id,
                LandsatMetadataId = _metadata.Id,
                FileInfoId = fileInfoId.Id
            };

            if (!await _repository.TryAddAsync(dataset))
                return StatusCode(500);

            var dirPath = Directory.GetParent(Directory.GetCurrentDirectory()).Parent;
            var subdirPath = Path.Combine(dirPath.FullName, "SatelliteImages");

            if (!Directory.Exists(subdirPath))
                dirPath.CreateSubdirectory("SatelliteImages");

            var suvdirLcPath = _metadata?.ProductContents?.LandsatProductId ?? throw new Exception();

            _subdirPath = Path.Combine(subdirPath, suvdirLcPath);

            if (!Directory.Exists(_subdirPath))
                dirPath.CreateSubdirectory($"SatelliteImages\\{suvdirLcPath}");

            return Ok();
        }

        [Description("")]
        [HttpPost]
        [Route("open/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Open(
            [Description("Идентификатор данных полученный при при вызове Read")]
            [Required] string id
            )
        {
            if (!ValidationHelper.IsBsonIdValid(id))
                return BadRequest("Ошибка запроса при загрузке файла. Полученный при вызове Upload идентификатор загрузки файла некорректный.");

            if (_subdirPath is null)
                return StatusCode(500);

            var innerFileStream = await _repository.DownloadFileStreamAsync(ObjectId.Parse(id));
            var filename = innerFileStream.FileInfo.Filename;

            if (!CheckForDataset(filename))
                return BadRequest("Ошибка запроса при загрузке файла. Название файла .tiff некорректное.");

            if (!await _repository.TryUpdateAsync(id, _metadata.Id))
                return BadRequest("Ошибка при обновлении данных в БД.");

            var filePath = Path.Combine(_subdirPath, filename);
            using (var fileStream = new FileStream(filePath, FileMode.OpenOrCreate))
            {
                innerFileStream.CopyTo(fileStream);
            }

            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> StartCalculate(CancellationToken ct)
        {
            if (_subdirPath == null)
                return NotFound("Subdir path isn't not found.");

            await ProcessService.RunCmdLineAsync(_subdirPath, Path.Combine(Directory.GetCurrentDirectory(), "bin", "Debug", "net8.0"));

            return Ok();
        }

        private bool CheckForDataset(string filename)
        {
            if (_metadata is null)
                return false;

            var productContent = _metadata.ProductContents;

            return typeof(ProductContent).GetProperties()
                .Where(x => typeof(string).Equals(x.PropertyType))
                .Any(x => filename.Equals(x.GetValue(productContent)));
        }
    }
}
