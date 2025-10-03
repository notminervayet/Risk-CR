using System;
using System.Drawing;
using System.Windows.Forms;

namespace Risk_CR.Formularios
{
    public partial class IntercambiarCartasForm : Form
    {
        private Juego juego;
        private CheckBox[] checkBoxes;

        public IntercambiarCartasForm()
        {

            juego = Juego.Instance;
            InicializarInterfaz();
        }

        private void InicializarInterfaz()
        {
            this.Text = "Intercambiar Cartas";
            this.Size = new Size(500, 400);
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.StartPosition = FormStartPosition.CenterParent;
            this.BackColor = Color.LightGray;

      
            Label lblTitulo = new Label();
            lblTitulo.Text = "Selecciona 3 cartas para intercambiar";
            lblTitulo.Font = new Font("Arial", 12, FontStyle.Bold);
            lblTitulo.Size = new Size(400, 30);
            lblTitulo.Location = new Point(50, 20);
            lblTitulo.TextAlign = ContentAlignment.MiddleCenter;

           
            Label lblInfoJugador = new Label();
            lblInfoJugador.Text = $"Jugador: {juego.JugadorActual.Nombre} - Cartas: {juego.JugadorActual.ManoCartas.Count}";
            lblInfoJugador.Size = new Size(400, 20);
            lblInfoJugador.Location = new Point(50, 50);

          
            Panel panelCartas = new Panel();
            panelCartas.Size = new Size(400, 200);
            panelCartas.Location = new Point(50, 80);
            panelCartas.BackColor = Color.White;
            panelCartas.AutoScroll = true;

            checkBoxes = new CheckBox[juego.JugadorActual.ManoCartas.Count];

            for (int i = 0; i < juego.JugadorActual.ManoCartas.Count; i++)
            {
                Carta carta = juego.JugadorActual.ManoCartas.Obtener(i);

                checkBoxes[i] = new CheckBox();
                checkBoxes[i].Text = $"{carta.Territorio} - {carta.Tipo}";
                checkBoxes[i].Size = new Size(350, 25);
                checkBoxes[i].Location = new Point(10, 10 + (i * 30));
                checkBoxes[i].Tag = carta;

                panelCartas.Controls.Add(checkBoxes[i]);
            }

           
            Button btnIntercambiar = new Button();
            btnIntercambiar.Text = "Intercambiar";
            btnIntercambiar.Size = new Size(100, 30);
            btnIntercambiar.Location = new Point(100, 300);
            btnIntercambiar.Click += BtnIntercambiar_Click;

            Button btnCancelar = new Button();
            btnCancelar.Text = "Cancelar";
            btnCancelar.Size = new Size(100, 30);
            btnCancelar.Location = new Point(250, 300);
            btnCancelar.Click += (s, e) => this.Close();

            
            Label lblMensaje = new Label();
            lblMensaje.Text = "Puedes intercambiar:\n- 3 cartas del mismo tipo\n- 1 de cada tipo diferente";
            lblMensaje.Size = new Size(400, 40);
            lblMensaje.Location = new Point(50, 250);
            lblMensaje.ForeColor = Color.Blue;

            this.Controls.AddRange(new Control[] {
                lblTitulo,
                lblInfoJugador,
                panelCartas,
                btnIntercambiar,
                btnCancelar,
                lblMensaje
            });
        }

        private void BtnIntercambiar_Click(object sender, EventArgs e)
        {
         
            int cartasSeleccionadas = 0;
            for (int i = 0; i < checkBoxes.Length; i++)
            {
                if (checkBoxes[i].Checked)
                    cartasSeleccionadas++;
            }

           
            if (cartasSeleccionadas != 3)
            {
                MessageBox.Show("Debes seleccionar exactamente 3 cartas para intercambiar.",
                              "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

         
            ListaGod<Carta> cartasParaIntercambiar = new ListaGod<Carta>();
            for (int i = 0; i < checkBoxes.Length; i++)
            {
                if (checkBoxes[i].Checked)
                {
                    Carta carta = (Carta)checkBoxes[i].Tag;
                    cartasParaIntercambiar.Agregar(carta);
                }
            }

           
            if (EsTrioValido(cartasParaIntercambiar))
            {
               
                int refuerzos = Juego.Instance.ContadorFibonacci;

                juego.JugadorActual.TropasDisponibles += refuerzos;

            
                for (int i = 0; i < cartasParaIntercambiar.Count; i++)
                {
                    juego.JugadorActual.ManoCartas.Remover(cartasParaIntercambiar.Obtener(i));
                }

               
                AvanzarContadorFibonacci();

                MessageBox.Show($"¡Intercambio exitoso! Recibiste {refuerzos} tropas de refuerzo.\n" +
                              $"Nuevo contador: {Juego.Instance.ContadorFibonacci}",
                              "Intercambio Completado", MessageBoxButtons.OK, MessageBoxIcon.Information);

                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            else
            {
                MessageBox.Show("Las cartas seleccionadas no forman un trío válido.\n" +
                              "Deben ser: 3 del mismo tipo o 1 de cada tipo diferente.",
                              "Trío Inválido", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private bool EsTrioValido(ListaGod<Carta> cartas)
        {
           
            bool mismoTipo = true;
            TipoCarta primerTipo = cartas.Obtener(0).Tipo;

            for (int i = 1; i < cartas.Count; i++)
            {
                if (cartas.Obtener(i).Tipo != primerTipo)
                {
                    mismoTipo = false;
                    break;
                }
            }

            if (mismoTipo)
                return true;

            bool tieneInfanteria = false;
            bool tieneCaballeria = false;
            bool tieneArtilleria = false;

            for (int i = 0; i < cartas.Count; i++)
            {
                switch (cartas.Obtener(i).Tipo)
                {
                    case TipoCarta.Infanteria:
                        tieneInfanteria = true;
                        break;
                    case TipoCarta.Caballeria:
                        tieneCaballeria = true;
                        break;
                    case TipoCarta.Artilleria:
                        tieneArtilleria = true;
                        break;
                }
            }

            return tieneInfanteria && tieneCaballeria && tieneArtilleria;
        }

        private void AvanzarContadorFibonacci()
        {
            int actual = Juego.Instance.ContadorFibonacci;
            int siguiente;

           
            if (actual == 2)
                siguiente = 3;
            else if (actual == 3)
                siguiente = 5;
            else if (actual == 5)
                siguiente = 8;
            else if (actual == 8)
                siguiente = 13;
            else if (actual == 13)
                siguiente = 21;
            else if (actual == 21)
                siguiente = 34;
            else if (actual == 34)
                siguiente = 55;
            else
                siguiente = actual + 21; 

            Juego.Instance.ContadorFibonacci = siguiente;
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // IntercambiarCartasForm
            // 
            this.ClientSize = new System.Drawing.Size(284, 261);
            this.Name = "IntercambiarCartasForm";
            this.Load += new System.EventHandler(this.IntercambiarCartasForm_Load);
            this.ResumeLayout(false);

        }

        private void IntercambiarCartasForm_Load(object sender, EventArgs e)
        {

        }
    }
}