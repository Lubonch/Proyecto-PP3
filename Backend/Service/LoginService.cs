using AutoMapper;
using Backend.Configs;
using Backend.Controllers;
using Backend.Domain;
using Backend.Model;
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
        private Mapper mapper;

        public LoginService(ILogger<LoginService> logger, ILoginRepository loginRepository)
        {
            _logger = logger;
            _loginRepository = loginRepository;
            mapper = MapperConfig.InitializeAutomapper();
        }
        public UserModel Login(LoginRequest request)
        {
            var loginUser = _loginRepository.Login(request).ToList<User>().FirstOrDefault();

            return mapper.Map<UserModel>(loginUser);
        }

        public Boolean Registro(Registro registroData)
        {
            return _loginRepository.Registro(registroData);     
        }
    }
}
