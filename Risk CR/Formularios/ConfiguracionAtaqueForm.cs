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

         
            Label lblDefensor = new Label();
            lblDefensor.Text = $"Defensor ({destino.Nombre} - {destino.Tropas} tropas):";
            lblDefensor.Size = new Size(200, 20);
            lblDefensor.Location = new Point(20, 70);

            
            bool esEjercitoNeutral = false;
            Label lblDadosDefensor = new Label();

            if (destino.Ocupante is Jugador defensor && defensor.Nombre == "Ejercito Neutral")
            {
                esEjercitoNeutral = true;
               
                int dadosNeutral = Math.Min(2, destino.Tropas);
                lblDadosDefensor.Text = $"Dados: {dadosNeutral}";
                TropasDefensor = dadosNeutral; 
            }
            else
            {
                
                NumericUpDown numDefensor = new NumericUpDown();
                numDefensor.Minimum = 1;
                numDefensor.Maximum = Math.Min(2, destino.Tropas);
                numDefensor.Value = Math.Min(2, destino.Tropas);
                numDefensor.Size = new Size(50, 20);
                numDefensor.Location = new Point(220, 70);
                panel.Controls.Add(numDefensor);

                
                lblDadosDefensor.Text = "Selecciona dados:";
                lblDadosDefensor.Tag = numDefensor;
            }

            lblDadosDefensor.Size = new Size(150, 20);
            lblDadosDefensor.Location = new Point(20, 100);
            panel.Controls.Add(lblDadosDefensor);

            Button btnAtacar = new Button();
            btnAtacar.Text = "Lanzar Ataque";
            btnAtacar.Size = new Size(120, 30);
            btnAtacar.Location = new Point(20, 140);
            btnAtacar.Click += (s, e) =>
            {
                TropasAtacante = (int)numAtacante.Value;

                if (!esEjercitoNeutral)
                {
                    NumericUpDown numDefensor = (NumericUpDown)lblDadosDefensor.Tag;
                    TropasDefensor = (int)numDefensor.Value;
                }

                this.DialogResult = DialogResult.OK;
                this.Close();
            };

            Button btnCancelar = new Button();
            btnCancelar.Text = "Cancelar";
            btnCancelar.Size = new Size(120, 30);
            btnCancelar.Location = new Point(150, 140);
            btnCancelar.Click += (s, e) =>
            {
                this.DialogResult = DialogResult.Cancel;
                this.Close();
            };

            panel.Controls.AddRange(new Control[] {
                lblAtacante, numAtacante,
                lblDefensor,
                btnAtacar, btnCancelar
            });
            this.Controls.Add(panel);
        }

        private void ConfiguracionAtaqueForm_Load(object sender, EventArgs e)
        {

        }
    }
}