using HeracleumSosnowskyiService.Interfaces;
using HeracleumSosnowskyiService.Services;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Diagnostics;
using System.IO.Pipelines;

namespace HeracleumSosnowskyiService.Controllers.v1
{
    [ApiController]
    [Route("[controller]")]
    public class CalculationMethodController : Controller
    {
        private readonly IProcessService _process;
        private readonly IFilesRepository _repository;

        public CalculationMethodController(IProcessService process, IFilesRepository repository)
        {
            _process = process ?? throw new ArgumentNullException(nameof(process));
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        [HttpGet]
        public IActionResult Index()
        {
            return Ok();
        }

        [HttpPost]
        public async Task StartsCalculate()
        {
            await _repository.DouwloadFileStreamAsync(ObjectId.Parse("6565d481d344aad249654b71"));
        }
    }
}
