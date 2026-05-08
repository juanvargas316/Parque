namespace Parque.Models
{
    public class Atraccion
    {
        private string _nombre;
        public string Nombre
        {
            get => _nombre;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("El nombre no puede estar vacío.");
                _nombre = value;
            }
        }

        public Atraccion(string nombre) => Nombre = nombre;
    }
}
