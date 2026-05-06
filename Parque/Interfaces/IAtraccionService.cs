using parque.Models;

namespace parque.Interfaces
{
    public interface IAtraccionService
    {
        void AgregarAtraccion(Atraccion atraccion);
        List<Atraccion> ObtenerAtracciones();
    }
}