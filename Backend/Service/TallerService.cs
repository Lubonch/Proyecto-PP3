using Backend.Controllers;
using Backend.Domain;
using Backend.Repository.Contracts;
using Backend.Service.Contracts;
using System.Net;

namespace Backend.Service
{
    public class TallerService : ITallerService
    {
        private readonly ILogger<TallerService> _logger;
        private ITallerRepository _tallerRepository;
        //private Mapper mapper;

        public TallerService(ILogger<TallerService> logger, ITallerRepository tallerRepository)
        {
            _logger = logger;
            _tallerRepository = tallerRepository;
            //mapper = MapperConfig.InitializeAutomapper();
        }
        public List<Taller> GetAllTalleres()
        {
            return _tallerRepository.GetAllTalleres();
        }
    }
}
