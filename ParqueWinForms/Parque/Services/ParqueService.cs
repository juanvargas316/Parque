using Microsoft.EntityFrameworkCore;
using Parque.Data;
using Parque.Interfaces;
using Parque.Models;

namespace Parque.Services
{
    public class ParqueService : IBoletaService, IAtraccionService, IIngresoService
    {
        private readonly ParqueDbContext _context;

        public ParqueService()
        {
            _context = new ParqueDbContext();
        }

        public Boleta VenderBoleta(Boleta boleta)
        {
            if (boleta == null)
                throw new ArgumentNullException(nameof(boleta));

            _context.Boletas.Add(boleta);
            _context.SaveChanges();

            return boleta;
        }

        public void AgregarAtraccion(Atraccion atraccion)
        {
            if (atraccion == null)
                throw new ArgumentNullException(nameof(atraccion));

            _context.Atracciones.Add(atraccion);
            _context.SaveChanges();
        }

        public List<Atraccion> ObtenerAtracciones()
        {
            return _context.Atracciones.ToList();
        }

        public List<Boleta> ObtenerBoletas()
        {
            return _context.Boletas.ToList();
        }

        public List<Ingreso> ObtenerIngresos()
        {
            return _context.Ingresos
                .Include(i => i.Atraccion)
                .Include(i => i.Boleta)
                .ToList();
        }

        public Ingreso RegistrarIngreso(Boleta boleta, Atraccion atraccion)
        {
            if (boleta == null)
                throw new ArgumentNullException(nameof(boleta));

            if (atraccion == null)
                throw new ArgumentNullException(nameof(atraccion));

            var ingreso = new Ingreso(boleta, atraccion);

            _context.Ingresos.Add(ingreso);

            _context.Boletas.Update(boleta);

            _context.SaveChanges();

            return ingreso;
        }
    }
}