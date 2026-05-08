using Parque.Models;

namespace Parque.Interfaces
{
    public interface IAtraccionService
    {
        void AgregarAtraccion(Atraccion atraccion);
        List<Atraccion> ObtenerAtracciones();
    }
}
