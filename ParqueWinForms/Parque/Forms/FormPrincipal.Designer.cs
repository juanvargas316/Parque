namespace Parque.Forms
{
    partial class FormPrincipal
    {
        private System.ComponentModel.IContainer components = null;

        private GroupBox grpAtracciones;
        private TextBox txtNombreAtraccion;
        private Button btnAgregarAtraccion;
        private ListBox lstAtracciones;

        private GroupBox grpBoletas;
        private Button btnVenderGeneral;
        private Button btnVenderVIP;
        private Button btnAnular;
        private Label lblEstadoBoleta;

        private GroupBox grpIngreso;
        private Button btnRegistrarIngreso;
        private ListBox lstIngresos;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.Text = "Parque de Atracciones";
            this.Size = new Size(700, 580);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.MinimumSize = new Size(700, 580);

            // Grupo Atracciones
            grpAtracciones = new GroupBox
            {
                Text = "Atracciones",
                Location = new Point(12, 12),
                Size = new Size(320, 200)
            };

            txtNombreAtraccion = new TextBox
            {
                PlaceholderText = "Nombre de la atracción...",
                Location = new Point(10, 25),
                Size = new Size(200, 23)
            };

            btnAgregarAtraccion = new Button
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
                Location = new Point(12, 225),
                Size = new Size(320, 160)
            };

            btnVenderGeneral = new Button
            {
                Text = "Vender General ($500)",
                Location = new Point(10, 28),
                Size = new Size(145, 35),
                BackColor = Color.SteelBlue,
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat
            };
            btnVenderGeneral.Click += btnVenderGeneral_Click;

            btnVenderVIP = new Button
            {
                Text = "Vender VIP ($1200)",
                Location = new Point(163, 28),
                Size = new Size(145, 35),
                BackColor = Color.Gold,
                ForeColor = Color.Black,
                FlatStyle = FlatStyle.Flat
            };
            btnVenderVIP.Click += btnVenderVIP_Click;

            btnAnular = new Button
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
                Location = new Point(348, 12),
                Size = new Size(326, 510)
            };

            btnRegistrarIngreso = new Button
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
                Size = new Size(304, 405)
            };

            grpIngreso.Controls.AddRange(new Control[]
                { btnRegistrarIngreso, lblHistorial, lstIngresos });

            this.Controls.AddRange(new Control[]
                { grpAtracciones, grpBoletas, grpIngreso });
        }
    }
}