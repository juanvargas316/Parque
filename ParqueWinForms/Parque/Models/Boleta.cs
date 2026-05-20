using Parque.Models;

namespace Parque.Models
{
    public abstract class Boleta
    {
        public int Id { get; set; }

        private float _precio;
        public float Precio
        {
            get => _precio;
            set
            {
                if (value <= 0)
                    throw new ArgumentException("El precio debe ser mayor a cero.");
                _precio = value;
            }
        }

        private DateTime _fechaVencimiento;
        public DateTime FechaVencimiento
        {
            get => _fechaVencimiento;
            set
            {
                if (value <= DateTime.Now)
                    throw new ArgumentException("La fecha de vencimiento debe ser futura.");
                _fechaVencimiento = value;
            }
        }

        public EstadoBoleta Estado { get; private set; }

        public Boleta(DateTime fechaVencimiento, float precio)
        {
            Precio = precio;
            FechaVencimiento = fechaVencimiento;
            Estado = EstadoBoleta.Activa;
        }

        public bool EstaVigente() => DateTime.Now <= FechaVencimiento;
        public bool EstaAnulada() => Estado == EstadoBoleta.Anulada;
        public bool EstaUsada() => Estado == EstadoBoleta.Usada;
        public bool PuedeIngresar() => !EstaAnulada() && !EstaUsada() && EstaVigente();

        public void Anular()
        {
            if (EstaAnulada())
                throw new InvalidOperationException("La boleta ya está anulada.");
            if (EstaUsada())
                throw new InvalidOperationException("No se puede anular una boleta ya usada.");
            Estado = EstadoBoleta.Anulada;
        }

        public void MarcarComoUsada() => Estado = EstadoBoleta.Usada;

        public abstract TipoAcceso ObtenerTipoAcceso();

        public override string ToString() =>
            $"Boleta ID: {Id}, Vence: {FechaVencimiento}, Estado: {Estado}, Precio: ${Precio}";
    }
}
