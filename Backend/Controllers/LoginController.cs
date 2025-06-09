using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using Microsoft.AspNetCore.Http;
using Backend.Service.Contracts;
using System;
using Backend.Domain;
using Microsoft.AspNetCore.Authorization;
using Backend.Service;

namespace Backend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LoginController : ControllerBase
    {
        private readonly ILogger<LoginController> _logger;
        private ILoginService _loginService;
        //private Mapper mapper;

        public LoginController(ILogger<LoginController> logger, ILoginService loginService)
        {
            _logger = logger;
            _loginService = loginService;
            //mapper = MapperConfig.InitializeAutomapper();
        }

        [HttpPost("login")]
        public Boolean Login(LoginRequest request)
        {
            return _loginService.Login(request);
        }

        [HttpPost("registro")]
        public Boolean Registro(Registro registroData)
        {
            return _loginService.Registro(registroData);
        }
    }
}
