using Risk_CR.Jugadores;
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
        private NumericUpDown numAtacante;
        private NumericUpDown numDefensor;
        private Label lblInfo;

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
            this.MaximizeBox = false;
            this.MinimizeBox = false;

            // Panel principal
            Panel panel = new Panel();
            panel.Dock = DockStyle.Fill;
            panel.BackColor = SystemColors.Control;
            panel.Padding = new Padding(15);

            // Título
            Label lblTitulo = new Label();
            lblTitulo.Text = $"Ataque: {origen.Nombre} → {destino.Nombre}";
            lblTitulo.Font = new Font("Arial", 11, FontStyle.Bold);
            lblTitulo.Size = new Size(350, 25);
            lblTitulo.Location = new Point(10, 10);
            lblTitulo.TextAlign = ContentAlignment.MiddleCenter;

            // Información del atacante
            Label lblAtacante = new Label();
            lblAtacante.Text = $"Atacante ({origen.Nombre}):";
            lblAtacante.Font = new Font("Arial", 9, FontStyle.Regular);
            lblAtacante.Size = new Size(150, 20);
            lblAtacante.Location = new Point(10, 50);

            Label lblInfoAtacante = new Label();
            lblInfoAtacante.Text = $"{origen.Tropas} tropas disponibles";
            lblInfoAtacante.Size = new Size(150, 20);
            lblInfoAtacante.Location = new Point(10, 70);

            numAtacante = new NumericUpDown();
            numAtacante.Minimum = 1;
            numAtacante.Maximum = Math.Min(3, origen.Tropas - 1);
            numAtacante.Value = Math.Min(3, origen.Tropas - 1);
            numAtacante.Size = new Size(50, 20);
            numAtacante.Location = new Point(170, 65);
            numAtacante.ValueChanged += ActualizarInfo;

            // Información del defensor
            Label lblDefensor = new Label();
            lblDefensor.Text = $"Defensor ({destino.Nombre}):";
            lblDefensor.Font = new Font("Arial", 9, FontStyle.Regular);
            lblDefensor.Size = new Size(150, 20);
            lblDefensor.Location = new Point(10, 100);

            Label lblInfoDefensor = new Label();
            lblInfoDefensor.Text = $"{destino.Tropas} tropas disponibles";
            lblInfoDefensor.Size = new Size(150, 20);
            lblInfoDefensor.Location = new Point(10, 120);

            bool esEjercitoNeutral = destino.Ocupante is Jugador defensorJugador && defensorJugador.Nombre == "Ejercito Neutral";

            if (esEjercitoNeutral)
            {
                // Ejército neutral - dados automáticos
                Label lblAutoDefensa = new Label();
                lblAutoDefensa.Text = $"Defensa automática: {Math.Min(2, destino.Tropas)} dados";
                lblAutoDefensa.Size = new Size(200, 20);
                lblAutoDefensa.Location = new Point(10, 140);
                panel.Controls.Add(lblAutoDefensa);
            }
            else
            {
                // Jugador normal - selección de dados
                Label lblSeleccionDefensor = new Label();
                lblSeleccionDefensor.Text = "Dados a usar:";
                lblSeleccionDefensor.Size = new Size(100, 20);
                lblSeleccionDefensor.Location = new Point(10, 140);

                numDefensor = new NumericUpDown();
                numDefensor.Minimum = 1;
                numDefensor.Maximum = Math.Min(2, destino.Tropas);
                numDefensor.Value = Math.Min(2, destino.Tropas);
                numDefensor.Size = new Size(50, 20);
                numDefensor.Location = new Point(110, 140);
                numDefensor.ValueChanged += ActualizarInfo;
                panel.Controls.Add(lblSeleccionDefensor);
                panel.Controls.Add(numDefensor);
            }

            // Información de la batalla
            lblInfo = new Label();
            lblInfo.Text = ObtenerTextoInfo();
            lblInfo.Size = new Size(350, 40);
            lblInfo.Location = new Point(10, 170);
            lblInfo.Font = new Font("Arial", 8, FontStyle.Regular);

            // Botones
            Button btnAtacar = new Button();
            btnAtacar.Text = "LANZAR ATAQUE";
            btnAtacar.Size = new Size(120, 30);
            btnAtacar.Location = new Point(10, 220);
            btnAtacar.BackColor = SystemColors.ButtonFace;
            btnAtacar.Font = new Font("Arial", 9, FontStyle.Regular);
            btnAtacar.Click += BtnAtacar_Click;

            Button btnCancelar = new Button();
            btnCancelar.Text = "CANCELAR";
            btnCancelar.Size = new Size(120, 30);
            btnCancelar.Location = new Point(140, 220);
            btnCancelar.BackColor = SystemColors.ButtonFace;
            btnCancelar.Font = new Font("Arial", 9, FontStyle.Regular);
            btnCancelar.Click += BtnCancelar_Click;

            // Agregar controles al panel
            panel.Controls.AddRange(new Control[] {
                lblTitulo,
                lblAtacante, lblInfoAtacante, numAtacante,
                lblDefensor, lblInfoDefensor,
                lblInfo,
                btnAtacar, btnCancelar
            });

            this.Controls.Add(panel);
            ActualizarInfo(null, EventArgs.Empty);
        }

        private void ActualizarInfo(object sender, EventArgs e)
        {
            lblInfo.Text = ObtenerTextoInfo();
        }

        private string ObtenerTextoInfo()
        {
            int dadosAtacante = (int)numAtacante.Value;
            int dadosDefensor = destino.Ocupante is Jugador defensor && defensor.Nombre == "Ejercito Neutral"
                ? Math.Min(2, destino.Tropas)
                : numDefensor != null ? (int)numDefensor.Value : 1;

            return $"Batalla: {dadosAtacante} dado(s) atacante vs {dadosDefensor} dado(s) defensor\n";
        }

        private void BtnAtacar_Click(object sender, EventArgs e)
        {
            TropasAtacante = (int)numAtacante.Value;

            if (destino.Ocupante is Jugador defensor && defensor.Nombre == "Ejercito Neutral")
            {
                TropasDefensor = Math.Min(2, destino.Tropas);
            }
            else
            {
                TropasDefensor = (int)numDefensor.Value;
            }

            if (TropasAtacante >= origen.Tropas)
            {
                MessageBox.Show("No puedes usar todas tus tropas para atacar. Debes dejar al menos 1 en el territorio.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (TropasDefensor > destino.Tropas)
            {
                MessageBox.Show("El defensor no puede usar más dados que tropas disponibles.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void BtnCancelar_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void ConfiguracionAtaqueForm_Load(object sender, EventArgs e)
        {
          
        }
    }
}