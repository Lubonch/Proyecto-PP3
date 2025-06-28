using Backend.Domain;

namespace Backend.Repository.Contracts
{
    public interface IAlumnoRepository
    {
        public List<User> GetAlumnoById(int Id);
        public List<User> IncribirseTaller(TallerRequest request);
    }
}
