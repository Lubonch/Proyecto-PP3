namespace Backend.Domain
{
    public class Taller
    {
        public virtual int Id { get; set; }
        public virtual required string Nombre { get; set; }
        public virtual required string Descripcion { get; set; }
        public virtual required DateTime FechaAlta { get; set; }
        public virtual required string UsuarioAlta { get; set; }
        public virtual DateTime FechaModif { get; set; }
        public virtual string? UsuarioModif { get; set; }
        public virtual Boolean Activo {  get; set; }

    }
}
