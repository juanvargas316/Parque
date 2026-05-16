using System.Drawing;

namespace Parque.Forms
{
    partial class FormPrincipal
    {
        private System.ComponentModel.IContainer components = null;

        private Panel pnlEncabezado;
        private Label lblTitulo;

        private GroupBox grpAtracciones;
        private TextBox txtNombreAtraccion;
        private RoundedButton btnAgregarAtraccion;
        private ListBox lstAtracciones;

        private GroupBox grpBoletas;
        private RoundedButton btnVenderGeneral;
        private RoundedButton btnVenderVIP;
        private RoundedButton btnAnular;
        private Label lblEstadoBoleta;

        private GroupBox grpIngreso;
        private RoundedButton btnRegistrarIngreso;
        private ListBox lstIngresos;

        private PictureBox picLogo;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.Text = "Parque de Atracciones";
            this.Icon = new Icon(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "barnyparque.ico"));
            this.Size = new Size(700, 720);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.MinimumSize = new Size(700, 720);
            this.BackColor = Color.FromArgb(245, 245, 250);
            this.Font = new Font("Comic Sans", 9, FontStyle.Bold);

            // Encabezado
            pnlEncabezado = new Panel
            {
                Location = new Point(0, 0),
                Size = new Size(700, 60),
                BackColor = Color.FromArgb(16, 88, 181)
            };

            lblTitulo = new Label
            {
                Text = "Parque de Atracciones",
                Font = new Font("Comic Sans", 18, FontStyle.Regular),
                ForeColor = Color.Black,
                Location = new Point(0, 12),
                Size = new Size(700, 40),
                AutoSize = false,
                TextAlign = ContentAlignment.MiddleCenter
            };

            pnlEncabezado.Controls.Add(lblTitulo);

            // Grupo Atracciones
            grpAtracciones = new GroupBox
            {
                Text = "Atracciones",
                Location = new Point(12, 75),
                Size = new Size(320, 200),
                BackColor = Color.FromArgb(220, 235, 255)
            };

            txtNombreAtraccion = new TextBox
            {
                PlaceholderText = "Nombre de la atracción",
                Location = new Point(10, 25),
                Size = new Size(200, 23)
            };

            btnAgregarAtraccion = new RoundedButton
            {
                Text = "Agregar",
                Location = new Point(218, 24),
                Size = new Size(90, 25)
            };
            btnAgregarAtraccion.Click += btnAgregarAtraccion_Click;

            lstAtracciones = new ListBox
            {
                Location = new Point(10, 58),
                Size = new Size(298, 130)
            };

            grpAtracciones.Controls.AddRange(new Control[]
                { txtNombreAtraccion, btnAgregarAtraccion, lstAtracciones });

            // Grupo Boletas
            grpBoletas = new GroupBox
            {
                Text = "Boletas",
                Location = new Point(12, 288),
                Size = new Size(320, 160),
                BackColor = Color.FromArgb(255, 245, 220)
            };

            btnVenderGeneral = new RoundedButton
            {
                Text = "Vender General ($500)",
                Location = new Point(10, 28),
                Size = new Size(145, 35),
                BackColor = Color.SteelBlue,
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat
            };
            btnVenderGeneral.Click += btnVenderGeneral_Click;

            btnVenderVIP = new RoundedButton
            {
                Text = "Vender VIP ($1200)",
                Location = new Point(163, 28),
                Size = new Size(145, 35),
                BackColor = Color.Gold,
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat
            };
            btnVenderVIP.Click += btnVenderVIP_Click;

            btnAnular = new RoundedButton
            {
                Text = "Anular boleta",
                Location = new Point(10, 75),
                Size = new Size(145, 35),
                BackColor = Color.IndianRed,
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Enabled = false
            };
            btnAnular.Click += btnAnular_Click;

            lblEstadoBoleta = new Label
            {
                Text = "Sin boleta activa",
                Location = new Point(10, 120),
                Size = new Size(298, 30),
                ForeColor = Color.Gray,
                AutoEllipsis = true
            };

            grpBoletas.Controls.AddRange(new Control[]
                { btnVenderGeneral, btnVenderVIP, btnAnular, lblEstadoBoleta });

            // Grupo Ingresos
            grpIngreso = new GroupBox
            {
                Text = "Registrar Ingreso",
                Location = new Point(348, 75),
                Size = new Size(326, 570),
                BackColor = Color.FromArgb(220, 255, 235)
            };

            btnRegistrarIngreso = new RoundedButton
            {
                Text = "Registrar ingreso (atracción seleccionada)",
                Location = new Point(10, 25),
                Size = new Size(304, 35),
                BackColor = Color.SeaGreen,
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Enabled = false
            };
            btnRegistrarIngreso.Click += btnRegistrarIngreso_Click;

            var lblHistorial = new Label
            {
                Text = "Historial de ingresos:",
                Location = new Point(10, 70),
                Size = new Size(200, 18)
            };

            lstIngresos = new ListBox
            {
                Location = new Point(10, 92),
                Size = new Size(304, 460)
            };

            grpIngreso.Controls.AddRange(new Control[]
                { btnRegistrarIngreso, lblHistorial, lstIngresos });

            // Logo
            picLogo = new PictureBox
            {
                Image = Image.FromFile(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "barnyparque.ico")),
                Location = new Point(12, 460),
                Size = new Size(320, 200),
                SizeMode = PictureBoxSizeMode.Zoom
            };

            this.Controls.AddRange(new Control[]
                { pnlEncabezado, grpAtracciones, grpBoletas, grpIngreso, picLogo });
        }
    }
}