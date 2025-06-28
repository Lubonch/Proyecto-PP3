using AutoMapper;
using Backend.Configs;
using Backend.Domain;
using Backend.Model;
using Backend.Repository.Contracts;
using Backend.Service.Contracts;
using log4net;
using Serilog;
using System.Net;

namespace Backend.Service
{
    public class AlumnoService : IAlumnoService
    {
        private readonly ILog Logger;
        private IAlumnoRepository _alumnoRepository;
        private Mapper mapper;

        public AlumnoService(IAlumnoRepository alumnoRepository)
        {
            Logger = LogManager.GetLogger("AlumnoService");
            _alumnoRepository = alumnoRepository;
            mapper = MapperConfig.InitializeAutomapper();
        }

        public UserModel GetAlumnoById(int Id)
        {
            try
            {
                Logger.Info($"Obteniendo el Alumno con Id: {Id}");
                var loginUser = _alumnoRepository.GetAlumnoById(Id).ToList<User>().FirstOrDefault();

                if (loginUser == null)
                {
                    Logger.Warn($"No se encontro ningun alumno con el Id: {Id}");
                    return null;
                }

                Logger.Info($"Se encontro el alumno: {loginUser.Usuario}");
                return mapper.Map<UserModel>(loginUser);
            }
            catch (Exception ex)
            {
                Logger.Error($"Error al obtener el alumno con Id: {Id}", ex);
                throw;
            }
        }

        public Boolean IncribirseTaller(TallerRequest request)
        {
            return _alumnoRepository.IncribirseTaller(request).ToList<User>().Any();
        }
    }
}