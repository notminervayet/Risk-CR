using System;
using System.Drawing;
using System.Windows.Forms;

namespace Risk_CR.Formularios
{
    public partial class ConfiguracionAtaqueForm : Form
    {
        private Territorio origen;
        private Territorio destino;
        private Juego juego;

        public int TropasAtacante { get; private set; }
        public int TropasDefensor { get; private set; }

        public ConfiguracionAtaqueForm(Territorio origen, Territorio destino)
        {
            InitializeComponent();
            this.origen = origen;
            this.destino = destino;
            juego = Juego.Instance;
            InicializarInterfaz();
        }

        private void InicializarInterfaz()
        {
            this.Text = "Configurar Ataque";
            this.Size = new Size(400, 300);
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.StartPosition = FormStartPosition.CenterScreen;

            Panel panel = new Panel();
            panel.Dock = DockStyle.Fill;
            panel.BackColor = Color.LightGray;

            // Configuración atacante
            Label lblAtacante = new Label();
            lblAtacante.Text = $"Atacante ({origen.Nombre} - {origen.Tropas} tropas):";
            lblAtacante.Size = new Size(200, 20);
            lblAtacante.Location = new Point(20, 30);

            NumericUpDown numAtacante = new NumericUpDown();
            numAtacante.Minimum = 1;
            numAtacante.Maximum = Math.Min(3, origen.Tropas - 1);
            numAtacante.Value = Math.Min(3, origen.Tropas - 1);
            numAtacante.Size = new Size(50, 20);
            numAtacante.Location = new Point(220, 30);

            // Configuración defensor
            Label lblDefensor = new Label();
            lblDefensor.Text = $"Defensor ({destino.Nombre} - {destino.Tropas} tropas):";
            lblDefensor.Size = new Size(200, 20);
            lblDefensor.Location = new Point(20, 70);

            NumericUpDown numDefensor = new NumericUpDown();
            numDefensor.Minimum = 1;
            numDefensor.Maximum = Math.Min(2, destino.Tropas);
            numDefensor.Value = Math.Min(2, destino.Tropas);
            numDefensor.Size = new Size(50, 20);
            numDefensor.Location = new Point(220, 70);

            Button btnAtacar = new Button();
            btnAtacar.Text = "Lanzar Ataque";
            btnAtacar.Size = new Size(120, 30);
            btnAtacar.Location = new Point(20, 120);
            btnAtacar.Click += (s, e) =>
            {
                TropasAtacante = (int)numAtacante.Value;
                TropasDefensor = (int)numDefensor.Value;
                this.DialogResult = DialogResult.OK;
                this.Close();
            };

            Button btnCancelar = new Button();
            btnCancelar.Text = "Cancelar";
            btnCancelar.Size = new Size(120, 30);
            btnCancelar.Location = new Point(150, 120);
            btnCancelar.Click += (s, e) =>
            {
                this.DialogResult = DialogResult.Cancel;
                this.Close();
            };

            panel.Controls.AddRange(new Control[] {
                lblAtacante, numAtacante,
                lblDefensor, numDefensor,
                btnAtacar, btnCancelar
            });
            this.Controls.Add(panel);
        }
    }
}