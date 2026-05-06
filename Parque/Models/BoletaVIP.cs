namespace parque.Models
{
    public class BoletaVIP : Boleta
    {
        public BoletaVIP(DateTime fechaVencimiento, float precio)
            : base(fechaVencimiento, precio) { }

        public override TipoAcceso ObtenerTipoAcceso() => TipoAcceso.SinFila;
    }
}