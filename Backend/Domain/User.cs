

namespace Backend.Domain
{
    public class User
    {
        public virtual int Id { get; set; }
        public virtual required string Usuario { get; set; }
        public virtual required string Password { get; set; }
        public virtual required string Email { get; set; }
        public virtual required int TallerId { get; set; }
        public virtual required DateTime FechaRegistro { get; set; }
        public virtual Boolean Activo { get; set; }
        public virtual Boolean Administrador { get; set; }

      
    }
}
