using HeracleumSosnowskyiService.Helpers;
using HeracleumSosnowskyiService.Models.Xml;
using HeracleumSosnowskyiService.Repositories;
using HeracleumSosnowskyiService.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using MongoDB.Bson;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace HeracleumSosnowskyiService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AreasDetectionController : Controller
    {
        private readonly IProcessService _process;
        private readonly IFilesRepository _repository;
        private readonly IMemoryCache _memoryCache;
        private static string? _subdirPath { get; set; }

        public AreasDetectionController(IProcessService process, IFilesRepository repository, IMemoryCache memory)
        {
            _process = process ?? throw new ArgumentNullException(nameof(process), $"The {nameof(ProcessService)} cannot be NULL.");
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

        [Description("Рассчитает индекс борщевика Сосновского по формуле Рыжикова, затем получит шейп-файл(shapefile)")]
        [HttpPost]
        [Route("open/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Open(
            [Description("Идентификатор спутниковых данных полученный при при вызове Upload или GetSatellitesData")] string id
            )
        {
            if (!ValidationHelper.IsBsonIdValid(id))
                return BadRequest("Ошибка запроса при загрузке файла. Полученный при вызове CreateFile идентификатор загрузки файла некорректный.");

            var fileInfo = await _repository.GetFileInfoByIdAsync(id);

            if (fileInfo?.MimeType == "text/xml")
            {
                var buffer = await _repository.DownloadAsBytesAsync(ObjectId.Parse(id));

                if (buffer == null)
                    return NotFound("Что-то пошло не так.");

                XmlSerializer serializer = new XmlSerializer(typeof(LandsatMetadata));

                LandsatMetadata landsatMetadata;
                using (Stream reader = new MemoryStream(buffer))
                {
                    landsatMetadata = serializer.Deserialize(reader) as LandsatMetadata;
                }

                var dirPath = Directory.GetParent(Directory.GetCurrentDirectory()).Parent;
                var subdirPath = Path.Combine(dirPath.FullName, "SatelliteImages");

                if (!Directory.Exists(subdirPath))
                    dirPath.CreateSubdirectory("SatelliteImages");

                var suvdirLcPath = landsatMetadata?.ProductContents?.LandsatProductId ?? throw new Exception();

                _subdirPath = Path.Combine(subdirPath, suvdirLcPath);

                if (!Directory.Exists(_subdirPath))
                    dirPath.CreateSubdirectory($"SatelliteImages\\{suvdirLcPath}");
            }
            else
            {
                var inputFileStream = await _repository.DownloadFileStreamAsync(ObjectId.Parse(id));

                var path = Path.Combine(_subdirPath, inputFileStream.FileInfo.Filename);
                if (System.IO.File.Exists(path))
                    return Ok();

                using (var outputFileStream = new FileStream(path, FileMode.Create))
                {
                    inputFileStream.CopyTo(outputFileStream);
                }

            }

            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> StartCalculate()
        {
            if(_subdirPath == null)
                return NotFound("Subdir path isn't not found.");

            await _process.RunCmdLineAsync(_subdirPath, Path.Combine(Directory.GetCurrentDirectory(), "bin", "Debug", "net8.0"));

            return Ok();
        }
    }
}
