namespace Parque.Models
{
    public class Ingreso
    {
        public int Id { get; set; }
        public DateTime Hora { get; private set; }
        public TipoAcceso TipoAcceso { get; private set; }
        public int BoletaId { get; set; }
        public Boleta Boleta { get; private set; }
         public int AtraccionId { get; set; }
        public Atraccion Atraccion { get; set; } = null!;

        public Ingreso()
        {
        }
        public Ingreso(Boleta boleta, Atraccion atraccion)
        {
            if (!boleta.PuedeIngresar())
                throw new InvalidOperationException("La boleta no es válida para ingresar.");

            boleta.MarcarComoUsada();
            Hora = DateTime.Now;
            TipoAcceso = boleta.ObtenerTipoAcceso();
            Boleta = boleta;
            BoletaId = boleta.Id;
            Atraccion = atraccion;
            AtraccionId = atraccion.Id;
        }
    }
}
