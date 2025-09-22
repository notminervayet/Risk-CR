using System;
using System.Drawing;
using System.Windows.Forms;
using Risk_CR.Jugadores;

namespace Risk_CR.Formularios
{
    public partial class IntercambioTarjetasForm : Form
    {
        private Jugador jugadorActual;
        private ListaGod<Carta> cartasSeleccionadas;

        public IntercambioTarjetasForm(Jugador jugador)
        {
            InitializeComponent();
            jugadorActual = jugador;
            cartasSeleccionadas = new ListaGod<Carta>();
            InicializarInterfaz();
        }

        private void InicializarInterfaz()
        {
            this.Text = "Intercambio de Tarjetas";
            this.Size = new Size(500, 400);
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.StartPosition = FormStartPosition.CenterScreen;

            Panel panelPrincipal = new Panel();
            panelPrincipal.Dock = DockStyle.Fill;
            panelPrincipal.BackColor = Color.LightGray;

            ListBox listBoxCartas = new ListBox();
            listBoxCartas.Size = new Size(300, 200);
            listBoxCartas.Location = new Point(50, 50);
            listBoxCartas.SelectionMode = SelectionMode.MultiSimple;

           
            for (int i = 0; i < jugadorActual.ManoCartas.Count; i++)
            {
                Carta carta = jugadorActual.ManoCartas.Obtener(i);
                listBoxCartas.Items.Add($"{carta.Territorio} - {carta.Tipo}");
            }

            
            Button btnSeleccionar = new Button();
            btnSeleccionar.Text = "Seleccionar para Intercambiar";
            btnSeleccionar.Size = new Size(150, 30);
            btnSeleccionar.Location = new Point(50, 270);
            btnSeleccionar.Click += (s, e) =>
            {
                cartasSeleccionadas = new ListaGod<Carta>();
                foreach (int index in listBoxCartas.SelectedIndices)
                {
                    cartasSeleccionadas.Agregar(jugadorActual.ManoCartas.Obtener(index));
                }
            };

            
            Button btnIntercambiar = new Button();
            btnIntercambiar.Text = "Intercambiar Trío";
            btnIntercambiar.Size = new Size(150, 30);
            btnIntercambiar.Location = new Point(220, 270);
            btnIntercambiar.Click += (s, e) => IntercambiarCartas();

        
            Label lblInfo = new Label();
            lblInfo.Text = $"Tropas por intercambio: {Juego.Instance.ContadorFibonacci}";
            lblInfo.Size = new Size(200, 20);
            lblInfo.Location = new Point(50, 320);

            panelPrincipal.Controls.AddRange(new Control[] { listBoxCartas, btnSeleccionar, btnIntercambiar, lblInfo });
            this.Controls.Add(panelPrincipal);
        }

        private void IntercambiarCartas()
        {
            if (cartasSeleccionadas.Count != 3)
            {
                MessageBox.Show("Debes seleccionar exactamente 3 cartas");
                return;
            }

            if (EsTrioValido(cartasSeleccionadas))
            {
               
                jugadorActual.TropasDisponibles += Juego.Instance.ContadorFibonacci;

                
                for (int i = 0; i < cartasSeleccionadas.Count; i++)
                {
                    jugadorActual.ManoCartas.Remover(cartasSeleccionadas.Obtener(i));
                }

          
                Juego.Instance.ContadorFibonacci = SiguienteFibonacci(Juego.Instance.ContadorFibonacci);

                MessageBox.Show($"¡Intercambio exitoso! +{Juego.Instance.ContadorFibonacci} tropas");
                this.Close();
            }
            else
            {
                MessageBox.Show("Las cartas no forman un trío válido (3 iguales o 1 de cada tipo)");
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
            if (mismoTipo) return true;

            bool tieneInfanteria = false, tieneCaballeria = false, tieneArtilleria = false;

            for (int i = 0; i < cartas.Count; i++)
            {
                switch (cartas.Obtener(i).Tipo)
                {
                    case TipoCarta.Infanteria: tieneInfanteria = true; break;
                    case TipoCarta.Caballeria: tieneCaballeria = true; break;
                    case TipoCarta.Artilleria: tieneArtilleria = true; break;
                }
            }

            return tieneInfanteria && tieneCaballeria && tieneArtilleria;
        }

        private int SiguienteFibonacci(int actual)
        {
           
            int[] fibonacci = { 2, 3, 5, 8, 13, 21, 34, 55 };

            for (int i = 0; i < fibonacci.Length - 1; i++)
            {
                if (fibonacci[i] == actual)
                    return fibonacci[i + 1];
            }
            return fibonacci[fibonacci.Length - 1];
        }

        private void IntercambioTarjetasForm_Load(object sender, EventArgs e)
        {

        }
    }
}