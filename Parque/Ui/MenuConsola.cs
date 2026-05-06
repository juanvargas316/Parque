using parque.Interfaces;
using parque.Models;

namespace parque.UI
{
    public class MenuConsola
    {
        private readonly IBoletaService _boletaService;
        private readonly IAtraccionService _atraccionService;
        private readonly IIngresoService _ingresoService;

        private Boleta? _ultimaBoleta;

        public MenuConsola(
            IBoletaService boletaService,
            IAtraccionService atraccionService,
            IIngresoService ingresoService)
        {
            _boletaService = boletaService;
            _atraccionService = atraccionService;
            _ingresoService = ingresoService;
        }

        public void Ejecutar()
        {
            bool salir = false;
            while (!salir)
            {
                Console.Clear();
                Console.WriteLine("\n=== MENU PARQUE ===");
                Console.WriteLine("1. Agregar atracción");
                Console.WriteLine("2. Listar atracciones");
                Console.WriteLine("3. Vender Boleta General");
                Console.WriteLine("4. Vender Boleta VIP");
                Console.WriteLine("5. Anular última boleta");
                Console.WriteLine("6. Registrar ingreso");
                Console.WriteLine("7. Ver estado de la última boleta");
                Console.WriteLine("0. Salir");
                Console.Write("Seleccione una opción: ");

                switch (Console.ReadLine())
                {
                    case "1": AgregarAtraccion(); break;
                    case "2": ListarAtracciones(); break;
                    case "3": VenderBoleta(esVIP: false); break;
                    case "4": VenderBoleta(esVIP: true); break;
                    case "5": AnularBoleta(); break;
                    case "6": RegistrarIngreso(); break;
                    case "7": VerEstadoBoleta(); break;
                    case "0": salir = true; break;
                    default: Console.WriteLine("Opción inválida."); break;
                }

                if (!salir)
                {
                    Console.Write("\nPresione Enter para continuar...");
                    Console.ReadLine();
                }
            }
        }

        private void AgregarAtraccion()
        {
            try
            {
                Console.Write("Nombre de la atracción: ");
                var nombre = Console.ReadLine();
                _atraccionService.AgregarAtraccion(new Atraccion(nombre!));
                Console.WriteLine("Atracción agregada correctamente.");
            }
            catch (Exception ex) { Console.WriteLine($"Error: {ex.Message}"); }
        }

        private void ListarAtracciones()
        {
            var lista = _atraccionService.ObtenerAtracciones();
            if (lista.Count == 0)
            {
                Console.WriteLine("No hay atracciones registradas.");
                return;
            }
            Console.WriteLine("\nAtracciones:");
            for (int i = 0; i < lista.Count; i++)
                Console.WriteLine($"{i + 1}. {lista[i].Nombre}");
        }

        private void VenderBoleta(bool esVIP)
        {
            Boleta boleta = esVIP
                ? new BoletaVIP(DateTime.Now.AddMinutes(1), 1200)
                : new BoletaGeneral(DateTime.Now.AddMinutes(1), 500);

            _ultimaBoleta = _boletaService.VenderBoleta(boleta);
            Console.WriteLine($"Boleta {(esVIP ? "VIP" : "General")} vendida.");
        }

        private void AnularBoleta()
        {
            if (_ultimaBoleta == null) { Console.WriteLine("No hay boleta para anular."); return; }
            try
            {
                _ultimaBoleta.Anular();
                Console.WriteLine("Boleta anulada correctamente.");
            }
            catch (Exception ex) { Console.WriteLine($"Error: {ex.Message}"); }
        }

        private void RegistrarIngreso()
        {
            if (_ultimaBoleta == null) { Console.WriteLine("No hay boleta vendida."); return; }

            var lista = _atraccionService.ObtenerAtracciones();
            if (lista.Count == 0) { Console.WriteLine("No hay atracciones disponibles."); return; }

            Console.WriteLine("\nSeleccione una atracción:");
            for (int i = 0; i < lista.Count; i++)
                Console.WriteLine($"{i + 1}. {lista[i].Nombre}");

            Console.Write("Opción: ");
            if (int.TryParse(Console.ReadLine(), out int index) && index > 0 && index <= lista.Count)
            {
                try
                {
                    var ingreso = _ingresoService.RegistrarIngreso(_ultimaBoleta, lista[index - 1]);
                    Console.WriteLine($"Ingreso en '{lista[index - 1].Nombre}' — Acceso: {ingreso.TipoAcceso}");
                }
                catch (Exception ex) { Console.WriteLine($"Error: {ex.Message}"); }
            }
            else Console.WriteLine("Selección inválida.");
        }

        private void VerEstadoBoleta()
        {
            if (_ultimaBoleta == null) { Console.WriteLine("No hay boleta para consultar."); return; }

            Console.WriteLine(_ultimaBoleta);
            if (!_ultimaBoleta.EstaVigente()) Console.WriteLine("La boleta está vencida.");
            if (_ultimaBoleta.EstaAnulada()) Console.WriteLine("La boleta está anulada.");
            if (_ultimaBoleta.EstaUsada()) Console.WriteLine("La boleta ya fue utilizada.");
            if (_ultimaBoleta.PuedeIngresar()) Console.WriteLine("La boleta puede ingresar.");
        }
    }
}