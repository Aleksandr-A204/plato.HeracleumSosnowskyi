using HeracleumSosnowskyiService.Helpers;
using HeracleumSosnowskyiService.Repositories;
using HeracleumSosnowskyiService.Services;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using System.ComponentModel;

namespace HeracleumSosnowskyiService.Controllers.v1
{
    [ApiController]
    [Route("[controller]")]
    public class AreasDetectionController : Controller
    {
        private readonly IProcessService _process;
        private readonly ISatelliteDataRepository _repository;

        public AreasDetectionController(IProcessService process, ISatelliteDataRepository repository)
        {
            _process = process ?? throw new ArgumentNullException(nameof(process), $"The {nameof(ProcessService)} cannot be NULL.");
            _repository = repository ?? throw new ArgumentNullException(nameof(repository), $"The {nameof(FilesRepository)} cannot be NULL.");
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Get()
        {
            var satellitesData = await _repository.GetAllAsync();

            return Ok(satellitesData);
        }

        [Description("Рассчитает индекс борщевика Сосновского по формуле Рыжикова, затем получит шейп-файл(shapefile)")]
        [HttpPost]
        [Route("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> StartCalculate([Description("Идентификатор")] string id)
        {
            if (!ValidationHelper.IsIdValid(id))
                return BadRequest("Ошибка запроса при обнаружении зоны произрастания БС. Полученный при вызове Upload идентификатор спутниковых данных некорректный.");

            var satelliteData = await _repository.GetByIdAsync(Ulid.Parse(id));

            var dirPath = Directory.GetParent(Directory.GetCurrentDirectory()).Parent;
            var subdirPath = Path.Combine(dirPath.FullName, "SatelliteImages");

            if (!Directory.Exists(subdirPath))
                dirPath.CreateSubdirectory("SatelliteImages");

            subdirPath = Path.Combine(subdirPath, satelliteData.LandsatProductId);

            if (!Directory.Exists(subdirPath))
                dirPath.CreateSubdirectory($"SatelliteImages\\{satelliteData.LandsatProductId}");

            //foreach (var dataset in satelliteData.Datasets)
            //{
            //    var inputFileStream = await _repository.DouwloadFileStreamAsync(ObjectId.Parse(dataset.FileStreamId));

            //    using (var outputFileStream = new FileStream(Path.Combine(subdirPath, inputFileStream.FileInfo.Filename), FileMode.Create))
            //    {
            //        inputFileStream.CopyTo(outputFileStream);
            //    }
            //}

            await _process.RunCmdLineAsync(subdirPath);

            return Ok();
        }
    }
}
