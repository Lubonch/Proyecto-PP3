using Backend.Controllers;
using Backend.Domain;
using Backend.Repository.Contracts;
using Backend.Service.Contracts;
using Microsoft.AspNetCore.Identity;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;

namespace Backend.Service
{
    public class LoginService : ILoginService
    {
        private readonly ILogger<LoginService> _logger;
        private ILoginRepository _loginRepository;
        private readonly UserManager<User> _userManager;
        //private Mapper mapper;

        public LoginService(ILogger<LoginService> logger, ILoginRepository loginRepository)
        {
            _logger = logger;
            _loginRepository = loginRepository;
            //mapper = MapperConfig.InitializeAutomapper();
        }
        public Boolean Login(LoginRequest request)
        {
            return _loginRepository.Login(request).ToList<User>().Any();
        }

    }
}
