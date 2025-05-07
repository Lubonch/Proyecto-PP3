namespace Backend.Domain
{
    public class Taller
    {
        public virtual int id { get; set; }
        public virtual required string nombre { get; set; }
        public virtual required string descripcion { get; set; }
    }
}
