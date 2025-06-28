using Backend.Repository.Contracts;
using Backend.Repository;
using Backend.Service.Contracts;
using Backend.Service;
using Microsoft.Extensions.DependencyInjection;

namespace Backend.Configs
{
    public class CustomExtensions
    {
        public static void AddInjectionServices(IServiceCollection services)
        {
            services.AddScoped<ITallerService, TallerService>();
            services.AddScoped<ILoginService, LoginService>();
            services.AddScoped<IAlumnoService, AlumnoService>();
        }
        public static void AddInjectionRepositories(IServiceCollection services)
        {
            services.AddScoped<ITallerRepository, TallerRepository>();
            services.AddScoped<ILoginRepository, LoginRepository>();
            services.AddScoped<IAlumnoRepository, AlumnoRepository>();
        }
    }
}