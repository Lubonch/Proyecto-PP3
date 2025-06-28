using Backend.Domain;
using Backend.Model;

namespace Backend.Service.Contracts
{
    public interface IAlumnoService
    {
        public UserModel GetAlumnoById(int Id);
        public Boolean IncribirseTaller(TallerRequest request);
    }
}
