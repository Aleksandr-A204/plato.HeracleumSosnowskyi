using HeracleumSosnowskyiService.Helpers;
using HeracleumSosnowskyiService.Repositories;
using HeracleumSosnowskyiService.Services;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel;

namespace HeracleumSosnowskyiService.Controllers.v1
{
    [ApiController]
    [Route("[controller]")]
    public class ZoneDetectionController : Controller
    {
        private readonly IProcessService _process;
        private readonly ISatelliteDataRepository _repository;

        public ZoneDetectionController(IProcessService process, ISatelliteDataRepository repository)
        {
            _process = process ?? throw new ArgumentNullException(nameof(process), $"The {nameof(ProcessService)} cannot be NULL.");
            _repository = repository ?? throw new ArgumentNullException(nameof(repository), $"The {nameof(FilesRepository)} cannot be NULL.");
        }

        [HttpGet]
        public IActionResult Index()
        {
            return Ok();
        }

        [Description("Вычислит индекс борщевика Сосновского по формуле Рыжикова и получит шейп-файл")]
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

            //if (!Directory.Exists(subdirPath))
            //    dirPath.CreateSubdirectory("SatelliteImages");

            //foreach (var fileInfo in filesInfo)
            //{
            //    if (fileInfo.FileName == null)
            //        return NotFound();

            //    var inputFileStream = await _repository.DouwloadFileStreamAsync(fileInfo.FileName);

            //    using (var outputFileStream = new FileStream(Path.Combine(subdirPath, fileInfo.FileName), FileMode.Create))
            //    {
            //        inputFileStream.CopyTo(outputFileStream);
            //    }
            //}

            //await _process.RunCmdLineAsync(subdirPath);

            return Ok();
        }
    }
}
