using Backend.Domain;

namespace Backend.Repository.Contracts
{
    public interface ILoginRepository
    {
        public List<User> Login(LoginRequest request);
        public Boolean Registro(Registro dataRegistro);
    }
}
