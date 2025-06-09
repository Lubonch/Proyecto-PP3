using Backend.Domain;

namespace Backend.Repository.Contracts
{
    public interface ITallerRepository
    {
        public List<Taller> GetAllTalleres();
    }
}
