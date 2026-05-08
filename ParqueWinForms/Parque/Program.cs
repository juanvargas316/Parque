using Parque.Forms;
using Parque.Services;

namespace Parque
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            ApplicationConfiguration.Initialize();
            var servicio = new ParqueService();
            Application.Run(new FormPrincipal(servicio));
        }
    }
}
