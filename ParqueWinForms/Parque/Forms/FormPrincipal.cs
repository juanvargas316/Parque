using Parque.Models;
using Parque.Services;

namespace Parque.Forms
{
    public partial class FormPrincipal : Form
    {
        private readonly ParqueService _servicio;
        private Boleta? _ultimaBoleta;

        public FormPrincipal(ParqueService servicio)
        {
            InitializeComponent();
            _servicio = servicio;
            ActualizarUI();
        }

        private Boleta? ObtenerProximaBoleta() =>
            _servicio
                .ObtenerBoletas()
                .Where(b => b.PuedeIngresar())
                .OrderByDescending(b => b is BoletaVIP)
                .FirstOrDefault();

        private void ActualizarUI()
        {
            lstAtracciones.Items.Clear();
            foreach (var a in _servicio.ObtenerAtracciones())
                lstAtracciones.Items.Add(a);

            lstAtracciones.DisplayMember = "Nombre";
            ActualizarEstadoBoleta();
        }

        private void ActualizarEstadoBoleta()
        {
            var boletaDisponible = ObtenerProximaBoleta();

            if (boletaDisponible == null)
            {
                lblEstadoBoleta.Text = "Sin boleta activa";
                lblEstadoBoleta.ForeColor = Color.Gray;
                btnAnular.Enabled = false;
                btnRegistrarIngreso.Enabled = false;
            }
            else
            {
                string tipo = boletaDisponible is BoletaVIP ? "VIP" : "General";
                int disponibles = _servicio.ObtenerBoletas().Count(b => b.PuedeIngresar());
                lblEstadoBoleta.Text = $"Boletas disponibles: {disponibles} | Proxima: {tipo} | ${boletaDisponible.Precio}";
                lblEstadoBoleta.ForeColor = Color.Green;
                btnAnular.Enabled = true;
                btnRegistrarIngreso.Enabled = true;
            }
        }

        private void btnAgregarAtraccion_Click(object sender, EventArgs e)
        {
            string nombre = txtNombreAtraccion.Text.Trim();
            if (string.IsNullOrWhiteSpace(nombre))
            {
                MessageBox.Show("Ingresa un nombre válido.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                _servicio.AgregarAtraccion(new Atraccion(nombre));
                txtNombreAtraccion.Clear();
                ActualizarUI();
                MessageBox.Show("Atraccion agregada.", "Exito", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnVenderGeneral_Click(object sender, EventArgs e) => VenderBoleta(false);
        private void btnVenderVIP_Click(object sender, EventArgs e) => VenderBoleta(true);

        private void VenderBoleta(bool esVIP)
        {
            Boleta boleta = esVIP
                ? new BoletaVIP(DateTime.Now.AddMinutes(10), 1200)
                : new BoletaGeneral(DateTime.Now.AddMinutes(10), 500);

            _ultimaBoleta = _servicio.VenderBoleta(boleta);
            ActualizarEstadoBoleta();

            string tipo = esVIP ? "VIP" : "General";
            MessageBox.Show($"Boleta {tipo} vendida.\nPrecio: ${boleta.Precio}\nVence: {boleta.FechaVencimiento:HH:mm}",
                "Boleta vendida", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnAnular_Click(object sender, EventArgs e)
        {
            var boletaDisponible = ObtenerProximaBoleta();
            if (boletaDisponible == null) return;
            try
            {
                boletaDisponible.Anular();
                ActualizarEstadoBoleta();
                MessageBox.Show("Boleta anulada.", "Anulada", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnRegistrarIngreso_Click(object sender, EventArgs e)
        {
            var boletaDisponible = ObtenerProximaBoleta();

            if (boletaDisponible == null || lstAtracciones.SelectedItem == null)
            {
                MessageBox.Show("No hay boletas disponibles o no seleccionaste una atraccion.", "Aviso",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                var atraccion = (Atraccion)lstAtracciones.SelectedItem;
                var ingreso = _servicio.RegistrarIngreso(boletaDisponible, atraccion);

                string tipo = ingreso.TipoAcceso == TipoAcceso.SinFila ? "VIP" : "General";
                lstIngresos.Items.Insert(0, $"{atraccion.Nombre} | Boleta {tipo} | {ingreso.Hora:HH:mm}");

                _ultimaBoleta = ObtenerProximaBoleta();
                ActualizarEstadoBoleta();

                MessageBox.Show($"Ingreso registrado en '{atraccion.Nombre}'.\nTipo: {tipo}\nHora: {ingreso.Hora:HH:mm}",
                    "Ingreso OK", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}