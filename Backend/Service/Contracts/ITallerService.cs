using Backend.Domain;

namespace Backend.Service.Contracts
{
    public interface ITallerService
    {
        public List<Taller> GetAllTalleres();
    }
}
