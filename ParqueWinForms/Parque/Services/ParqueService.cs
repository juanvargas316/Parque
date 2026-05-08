using Parque.Interfaces;
using Parque.Models;

namespace Parque.Services
{
    public class ParqueService : IBoletaService, IAtraccionService, IIngresoService
    {
        private readonly List<Boleta> _boletas = new();
        private readonly List<Atraccion> _atracciones = new();
        private readonly List<Ingreso> _ingresos = new();

        public Boleta VenderBoleta(Boleta boleta)
        {
            if (boleta == null) throw new ArgumentNullException(nameof(boleta));
            _boletas.Add(boleta);
            return boleta;
        }

        public void AgregarAtraccion(Atraccion atraccion)
        {
            if (atraccion == null) throw new ArgumentNullException(nameof(atraccion));
            _atracciones.Add(atraccion);
        }

        public List<Atraccion> ObtenerAtracciones() => _atracciones;

        public Ingreso RegistrarIngreso(Boleta boleta, Atraccion atraccion)
        {
            if (boleta == null) throw new ArgumentNullException(nameof(boleta));
            if (atraccion == null) throw new ArgumentNullException(nameof(atraccion));

            var ingreso = new Ingreso(boleta, atraccion);
            _ingresos.Add(ingreso);
            return ingreso;
        }
    }
}
