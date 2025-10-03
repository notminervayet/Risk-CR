using Risk_CR.Jugadores;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace Risk_CR.Formularios
{
    public class SeleccionAtaqueForm : Form
    {
        private Territorio territorioOrigen;
        private ListBox listTerritorios;

        public Territorio TerritorioDestinoSeleccionado { get; private set; }

        public SeleccionAtaqueForm(Territorio origen)
        {
            territorioOrigen = origen;
            InicializarInterfaz();
        }

        private void InicializarInterfaz()
        {
            this.Text = "Seleccionar Territorio para Atacar";
            this.Size = new Size(350, 300);
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.BackColor = Color.LightGray;

            
            listTerritorios = new ListBox();
            listTerritorios.Size = new Size(300, 150);
            listTerritorios.Location = new Point(20, 20);

            
            for (int i = 0; i < territorioOrigen.TerritoriosAdyacentes.Count; i++)
            {
                Territorio adyacente = territorioOrigen.TerritoriosAdyacentes.Obtener(i);
                if (adyacente.Ocupante != Juego.Instance.JugadorActual)
                {
                    string jugadorEnemigo = (adyacente.Ocupante is Jugador jugador) ? jugador.Nombre : "Neutral";
                    listTerritorios.Items.Add(new TerritorioItem(adyacente, $"{adyacente.Nombre} ({adyacente.Tropas} tropas) - {jugadorEnemigo}"));
                }
            }

            
            Button btnSeleccionar = new Button();
            btnSeleccionar.Text = "Atacar";
            btnSeleccionar.Size = new Size(80, 30);
            btnSeleccionar.Location = new Point(20, 190);
            btnSeleccionar.Click += BtnSeleccionar_Click;

            Button btnCancelar = new Button();
            btnCancelar.Text = "Cancelar";
            btnCancelar.Size = new Size(80, 30);
            btnCancelar.Location = new Point(120, 190);
            btnCancelar.Click += (s, e) => { this.DialogResult = DialogResult.Cancel; this.Close(); };

           
            this.Controls.Add(listTerritorios);
            this.Controls.Add(btnSeleccionar);
            this.Controls.Add(btnCancelar);
        }

        private void BtnSeleccionar_Click(object sender, EventArgs e)
        {
            if (listTerritorios.SelectedIndex == -1)
            {
                MessageBox.Show("Selecciona un territorio para atacar");
                return;
            }

            TerritorioDestinoSeleccionado = ((TerritorioItem)listTerritorios.SelectedItem).Territorio;
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

   
        private class TerritorioItem
        {
            public Territorio Territorio { get; set; }
            public string Texto { get; set; }

            public TerritorioItem(Territorio territorio, string texto)
            {
                Territorio = territorio;
                Texto = texto;
            }

            public override string ToString()
            {
                return Texto;
            }
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // SeleccionAtaqueForm
            // 
            this.ClientSize = new System.Drawing.Size(284, 261);
            this.Name = "SeleccionAtaqueForm";
            this.Load += new System.EventHandler(this.SeleccionAtaqueForm_Load);
            this.ResumeLayout(false);

        }

        private void SeleccionAtaqueForm_Load(object sender, EventArgs e)
        {

        }
    }
}