using Parque.Models;

namespace Parque.Interfaces
{

    public interface IIngresoService
    {
        Ingreso RegistrarIngreso(Boleta boleta, Atraccion atraccion);
    }
}
