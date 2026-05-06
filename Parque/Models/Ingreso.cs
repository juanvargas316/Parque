namespace parque.Models
{
    public class Ingreso
    {
        public DateTime Hora { get; private set; }
        public TipoAcceso TipoAcceso { get; private set; }
        public Boleta Boleta { get; private set; }
        public Atraccion Atraccion { get; private set; }

        public Ingreso(Boleta boleta, Atraccion atraccion)
        {
            if (!boleta.PuedeIngresar())
                throw new InvalidOperationException("La boleta no es válida para ingresar.");

            boleta.MarcarComoUsada();
            Hora = DateTime.Now;
            TipoAcceso = boleta.ObtenerTipoAcceso();
            Boleta = boleta;
            Atraccion = atraccion;
        }
    }
}