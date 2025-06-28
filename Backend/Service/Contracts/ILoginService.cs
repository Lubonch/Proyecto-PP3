using Backend.Domain;
using Backend.Model;

namespace Backend.Service.Contracts
{
    public interface ILoginService
    {
        public UserModel Login(LoginRequest request);
        public Boolean Registro(Registro registroData);
    }
}
