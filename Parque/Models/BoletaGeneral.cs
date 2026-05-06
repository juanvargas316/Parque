namespace parque.Models
{
    public class BoletaGeneral : Boleta
    {
        public BoletaGeneral(DateTime fechaVencimiento, float precio)
            : base(fechaVencimiento, precio) { }

        public override TipoAcceso ObtenerTipoAcceso() => TipoAcceso.Normal;
    }
}