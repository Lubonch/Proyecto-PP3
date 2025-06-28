using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using Microsoft.AspNetCore.Http;
using Backend.Service.Contracts;
using System;
using Backend.Domain;
using Microsoft.AspNetCore.Authorization;
using Backend.Service;
using Backend.Model;
using AutoMapper;
using Backend.Configs;

namespace Backend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AlumnoController : ControllerBase
    {
        private readonly ILogger<AlumnoController> _logger;
        private IAlumnoService _alumnoService;
        private Mapper mapper;

        public AlumnoController(ILogger<AlumnoController> logger, IAlumnoService alumnoService)
        {
            _logger = logger;
            _alumnoService = alumnoService;
            mapper = MapperConfig.InitializeAutomapper();
        }

        [HttpGet("GetAlumnoById")]
        public UserModel GetAlumnoById([FromQuery] int Id)
        {
            return _alumnoService.GetAlumnoById(Id);
        }


        [HttpPost("IncribirseTaller")]
        public Boolean IncribirseTaller(TallerRequest request)
        {
            var test = _alumnoService.IncribirseTaller(request);
            return test;
        }
    }
}
