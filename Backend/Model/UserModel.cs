

namespace Backend.Model
{
    public class UserModel
    {
        public int Id { get; set; }
        public required string Usuario { get; set; }
        public required string Email { get; set; }
        public int TallerId { get; set; }
        public Boolean Activo { get; set; }
        public Boolean Administrador { get; set; }

      
    }
}
