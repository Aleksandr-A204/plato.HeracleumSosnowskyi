using HeracleumSosnowskyiService.Interfaces;
using HeracleumSosnowskyiService.MongoDb.Data;
using HeracleumSosnowskyiService.Repositories;
using HeracleumSosnowskyiService.Services;
using HeracleumSosnowskyiService.Storage;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Diagnostics;
using System.IO;
using System.IO.Pipelines;

namespace HeracleumSosnowskyiService.Controllers.v1
{
    [ApiController]
    [Route("[controller]")]
    public class CalculationMethodController : Controller
    {
        private readonly IProcessService _process;
        private readonly IFilesRepository _filesRepository;
        private readonly ISatelliteImagesDatasetRepository _imagesDatasetsrepository;

        public CalculationMethodController(IProcessService process, IFilesRepository filesRepository, ISatelliteImagesDatasetRepository imagesDatasetsrepository)
        {
            _process = process ?? 
                throw new ArgumentNullException(nameof(process), $"The {nameof(ProcessService)} cannot be NULL.");
            _filesRepository = filesRepository ?? 
                throw new ArgumentNullException(nameof(filesRepository), $"The {nameof(FilesRepository)} cannot be NULL.");
            _imagesDatasetsrepository = imagesDatasetsrepository ?? 
                throw new ArgumentNullException(nameof(imagesDatasetsrepository), $"The {nameof(SatelliteImagesDatasetRepository)} cannot be NULL.");
        }

        [HttpGet]
        public IActionResult Index()
        {
            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> StartsCalculate()
        {
            //var filesInfo = await _repository.GetAllAsync();

            var dirPath = Directory.GetParent(Directory.GetCurrentDirectory()).Parent;
            var subdirPath = Path.Combine(dirPath.FullName, "Satellite");

            // if (!Directory.Exists(subdirPath))
            //    dirPath.CreateSubdirectory("Satellite");

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

            await _process.RunCmdLineAsync(subdirPath);

            return Ok();
        }
    }
}
