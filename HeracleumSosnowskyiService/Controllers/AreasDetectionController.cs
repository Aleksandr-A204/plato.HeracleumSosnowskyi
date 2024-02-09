using HeracleumSosnowskyiService.Helpers;
using HeracleumSosnowskyiService.Repositories;
using HeracleumSosnowskyiService.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using MongoDB.Bson;
using System.ComponentModel;
using System.Diagnostics;

namespace HeracleumSosnowskyiService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AreasDetectionController : Controller
    {
        private readonly IProcessService _process;
        private readonly IDatasetsRepository _repository;
        private readonly IMemoryCache _memoryCache;

        public AreasDetectionController(IProcessService process, IDatasetsRepository repository, IMemoryCache memory)
        {
            _process = process ?? throw new ArgumentNullException(nameof(process), $"The {nameof(ProcessService)} cannot be NULL.");
            _repository = repository ?? throw new ArgumentNullException(nameof(repository), $"The {nameof(DatasetsRepository)} cannot be NULL.");
            _memoryCache = memory ?? throw new ArgumentNullException(nameof(memory), $"The {nameof(MemoryCache)} cannot be NULL.");
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetSatellitesData()
        {
            var satellitesData = await _repository.GetSatellitesDataAsync();

            return Ok(satellitesData);
        }

        [Description("Рассчитает индекс борщевика Сосновского по формуле Рыжикова, затем получит шейп-файл(shapefile)")]
        [HttpPost]
        [Route("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Post(
            [Description("Идентификатор спутниковых данных полученный при при вызове Upload или GetSatellitesData")] string id
            )
        {
            if (!ValidationHelper.IsIdValid(id))
                return BadRequest("Ошибка запроса при обнаружении зоны произрастания БС. Идентификатор спутниковых данных некорректный.");

            var satelliteData = await _repository.GetByIdAsync(Ulid.Parse(id));

            //var datasets = await _repository.GetDatasetsBySatelliteDataIdAsync(Ulid.Parse(id));

            var dirPath = Directory.GetParent(Directory.GetCurrentDirectory()).Parent;
            var subdirPath = Path.Combine(dirPath.FullName, "SatelliteImages");

            if (!Directory.Exists(subdirPath))
                dirPath.CreateSubdirectory("SatelliteImages");

            subdirPath = Path.Combine(subdirPath, satelliteData.LandsatProductId);

            if (!Directory.Exists(subdirPath))
                dirPath.CreateSubdirectory($"SatelliteImages\\{satelliteData.LandsatProductId}");

            foreach (var dataset in satelliteData.Datasets)
            {
                var inputFileStream = await _repository.DouwloadFileStreamAsync(ObjectId.Parse(dataset.FileStreamId));

                var path = Path.Combine(subdirPath, inputFileStream.FileInfo.Filename);

                if (System.IO.File.Exists(path))
                    continue;

                using (var outputFileStream = new FileStream(Path.Combine(subdirPath, inputFileStream.FileInfo.Filename), FileMode.Create))
                {
                    inputFileStream.CopyTo(outputFileStream);
                }
            }

            //_memoryCache.Set("subdirPath", subdirPath, new MemoryCacheEntryOptions
            //{
            //    AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(5)
            //});

            _memoryCache.Set("subdirPath", subdirPath);

            return Ok(satelliteData);
        }

        [HttpPut]
        public async Task<IActionResult> StartCalculate()
        {
            if (!_memoryCache.TryGetValue<string>("subdirPath", out var subdirPath))
                return NotFound();

            await _process.RunCmdLineAsync(subdirPath);

            return Ok();
        }
    }
}
