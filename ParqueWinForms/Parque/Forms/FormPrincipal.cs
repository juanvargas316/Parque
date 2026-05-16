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
            if (_ultimaBoleta == null)
            {
                lblEstadoBoleta.Text = "Sin boleta activa";
                lblEstadoBoleta.ForeColor = Color.Gray;
                btnAnular.Enabled = false;
                btnRegistrarIngreso.Enabled = false;
            }
            else
            {
                string tipo = _ultimaBoleta is BoletaVIP ? "VIP" : "General";
                lblEstadoBoleta.Text = $"Boleta {tipo} | Estado: {_ultimaBoleta.Estado} | Precio: ${_ultimaBoleta.Precio}";
                lblEstadoBoleta.ForeColor = _ultimaBoleta.PuedeIngresar() ? Color.Green : Color.Red;
                btnAnular.Enabled = !_ultimaBoleta.EstaAnulada() && !_ultimaBoleta.EstaUsada();
                btnRegistrarIngreso.Enabled = _ultimaBoleta.PuedeIngresar();
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
                MessageBox.Show("Atracción agregada.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
            if (_ultimaBoleta == null) return;
            try
            {
                _ultimaBoleta.Anular();
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
            if (_ultimaBoleta == null || lstAtracciones.SelectedItem == null)
            {
                MessageBox.Show("Selecciona una atracción de la lista.", "Aviso",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                var atraccion = (Atraccion)lstAtracciones.SelectedItem;
                var ingreso = _servicio.RegistrarIngreso(_ultimaBoleta, atraccion);

                string tipo = ingreso.TipoAcceso == TipoAcceso.SinFila ? "VIP" : "General";
                lstIngresos.Items.Insert(0, $"{atraccion.Nombre} | Boleta {tipo} | {ingreso.Hora:HH:mm}");

                ActualizarEstadoBoleta();

                MessageBox.Show($"Ingreso registrado en '{atraccion.Nombre}'.\nTipo de boleta: {tipo}\nHora: {ingreso.Hora:HH:mm}",
                    "Ingreso OK", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}