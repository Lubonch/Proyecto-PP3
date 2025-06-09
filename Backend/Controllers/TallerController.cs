using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using Microsoft.AspNetCore.Http;
using Backend.Service.Contracts;
using System;

namespace Backend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TallerController : ControllerBase
    {
        private readonly ILogger<TallerController> _logger;
        private ITallerService _tallerService;
        //private Mapper mapper;

        public TallerController(ILogger<TallerController> logger, ITallerService tallerService)
        {
            _logger = logger;
            _tallerService = tallerService;
            //mapper = MapperConfig.InitializeAutomapper();
        }

        [HttpGet(Name = "GetAllTalleres")]
        public IEnumerable<Domain.Taller> GetAllTalleres()
        {
            var test = _tallerService.GetAllTalleres().Cast<Domain.Taller>().ToArray();
            return test;
        }
    }
}
