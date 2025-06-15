using Backend.Domain;

namespace Backend.Service.Contracts
{
    public interface ILoginService
    {
        public Boolean Login(LoginRequest request);
        public Boolean Registro(Registro registroData);
    }
}
