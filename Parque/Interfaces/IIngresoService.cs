using parque.Models;

namespace parque.Interfaces
{

    public interface IIngresoService
    {
        Ingreso RegistrarIngreso(Boleta boleta, Atraccion atraccion);
    }
}