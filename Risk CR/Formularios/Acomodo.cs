using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Risk_CR.Jugadores;

namespace Risk_CR.Formularios
{
    public partial class Acomodo : Form
    {
        private CheckBox checkBox3;
        private TextBox textBox3;
        private ComboBox comboBox3;
        private Label labelad3;
        private Label label3;

        public Acomodo()
        {
            InitializeComponent();
            InicializarTercerJugador();
            ConfigurarEventos();
        }

        private void ConfigurarEventos()
        {
            //se validan los cambios en los controles   
            textBox1.TextChanged += (s, e) => ValidarAmbos();
            textBox2.TextChanged += (s, e) => ValidarAmbos();
            textBox3.TextChanged += (s, e) => ValidarAmbos();
            comboBox1.SelectedIndexChanged += (s, e) => ValidarAmbos();
            comboBox2.SelectedIndexChanged += (s, e) => ValidarAmbos();
            comboBox3.SelectedIndexChanged += (s, e) => ValidarAmbos();
        }

        private void InicializarTercerJugador()
        {
            
            label3 = new Label();
            label3.Text = "Jugador 3 (Opcional)";
            label3.Location = new Point(700, 217);
            label3.Size = new Size(120, 13);
            label3.BackColor = Color.Transparent;
            this.Controls.Add(label3);

            // TextBox para nombre del tercer jugador
            textBox3 = new TextBox();
            textBox3.Location = new Point(700, 242);
            textBox3.Size = new Size(100, 20);
            textBox3.TextChanged += (s, e) => ValidarAmbos();
            this.Controls.Add(textBox3);

            // ComboBox para color del tercer jugador
            comboBox3 = new ComboBox();
            comboBox3.Location = new Point(700, 268);
            comboBox3.Size = new Size(121, 21);
            comboBox3.SelectedIndexChanged += (s, e) => ValidarAmbos();
            this.Controls.Add(comboBox3);

            // CheckBox para activar tercer jugador
            checkBox3 = new CheckBox();
            checkBox3.Location = new Point(740, 305);
            checkBox3.Size = new Size(15, 14);
            checkBox3.BackColor = Color.Transparent;
            checkBox3.CheckedChanged += checkBox3_CheckedChanged;
            this.Controls.Add(checkBox3);

            // Label para advertencias del tercer jugador
            labelad3 = new Label();
            labelad3.Location = new Point(700, 324);
            labelad3.Size = new Size(200, 13);
            labelad3.BackColor = Color.Transparent;
            labelad3.ForeColor = Color.Red;
            this.Controls.Add(labelad3);
        }

        private void Acomodo_Load(object sender, EventArgs e)
        {
            //desactivar el boton de play al iniciar
            pictureBoxPlay.Enabled = false;
            pictureBoxPlay.Visible = false;

            // Agregar colores a los ComboBox
            string[] colores = { "Rojo", "Azul", "Verde", "Morado", "Amarillo" };

            comboBox1.Items.AddRange(colores);
            comboBox2.Items.AddRange(colores);
            comboBox3.Items.AddRange(colores);

           
            if (comboBox1.Items.Count > 0) comboBox1.SelectedIndex = 0;
            if (comboBox2.Items.Count > 1) comboBox2.SelectedIndex = 1;
            if (comboBox3.Items.Count > 2) comboBox3.SelectedIndex = 2;
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            //boton de regresar al menu principal
            this.Close();
        }

        private void pictureBoxPlay_Click(object sender, EventArgs e)
        {
            //recoger datos de los jugadores
            string nombre1 = textBox1.Text.Trim();
            string nombre2 = textBox2.Text.Trim();
            string nombre3 = textBox3.Text.Trim();

            string color1 = comboBox1.SelectedItem?.ToString();
            string color2 = comboBox2.SelectedItem?.ToString();
            string color3 = comboBox3.SelectedItem?.ToString();

            ListaGod<Jugador> jugadores = new ListaGod<Jugador>();

         
            Jugador jugador1 = new Jugador(nombre1, color1);
            jugadores.Agregar(jugador1);

            
            Jugador jugador2 = new Jugador(nombre2, color2);
            jugadores.Agregar(jugador2);

            //verificar si el tercer jugador esta activo
            if (checkBox3.Checked && !string.IsNullOrEmpty(nombre3) && !string.IsNullOrEmpty(color3))
            {
                
                Jugador jugador3 = new Jugador(nombre3, color3);
                jugadores.Agregar(jugador3);

           
                jugador1.TropasDisponibles = 21;
                jugador2.TropasDisponibles = 21;
                jugador3.TropasDisponibles = 21;
            }
            else
            {
              
                Jugador ejercitoNeutral = new Jugador("Ejercito Neutral", "Gris");
                jugadores.Agregar(ejercitoNeutral);

                
                jugador1.TropasDisponibles = 26;
                jugador2.TropasDisponibles = 26;
                ejercitoNeutral.TropasDisponibles = 26;
            }

            this.Hide();
            MapaFormulario mapa = new MapaFormulario(jugadores);
            mapa.ShowDialog();
            this.Show();
        }

        private void ValidarAmbos()
        {
            //verificar si los jugadores principales estan listos para iniciar el juego
            string nombre1 = textBox1.Text.Trim();
            string nombre2 = textBox2.Text.Trim();
            string nombre3 = textBox3.Text.Trim();

            string color1 = comboBox1.SelectedItem?.ToString();
            string color2 = comboBox2.SelectedItem?.ToString();
            string color3 = comboBox3.SelectedItem?.ToString();

        
            bool nombre1Valido = checkBox1.Checked && !string.IsNullOrEmpty(nombre1) && nombre1.Length <= 10;
            bool color1Valido = !string.IsNullOrEmpty(color1);

            
            bool nombre2Valido = checkBox2.Checked && !string.IsNullOrEmpty(nombre2) && nombre2.Length <= 10;
            bool color2Valido = !string.IsNullOrEmpty(color2);

            // Validar tercer jugador solo si está activo y que no se llame "Ejercito Neutral"
            bool coloresDistintos = color1Valido && color2Valido && color1 != color2;
            bool nombresDistintos = nombre1Valido && nombre2Valido && nombre1 != nombre2;
            bool nombresNoSonNeutral = nombre1.ToLower() != "ejercito neutral" &&
                                      nombre2.ToLower() != "ejercito neutral" &&
                                      nombre3.ToLower() != "ejercito neutral";

            bool jugadoresPrincipalesListos = nombre1Valido && nombre2Valido &&
                                            color1Valido && color2Valido &&
                                            coloresDistintos && nombresDistintos &&
                                            nombresNoSonNeutral;

            pictureBoxPlay.Enabled = jugadoresPrincipalesListos;
            pictureBoxPlay.Visible = jugadoresPrincipalesListos;
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            //validar el primer jugador
            if (checkBox1.Checked)
            {
                ValidarJugador(textBox1, comboBox1, labelad, 1);
            }
            else
            {
                labelad.Text = "";
            }
            ValidarAmbos();
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            //validar el segundo jugador
            if (checkBox2.Checked)
            {
                ValidarJugador(textBox2, comboBox2, labelad2, 2);
            }
            else
            {
                labelad2.Text = "";
            }
            ValidarAmbos();
        }

        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {
            //validar el tercer jugador
            if (checkBox3.Checked)
            {
                ValidarJugador(textBox3, comboBox3, labelad3, 3);
            }
            else
            {
                labelad3.Text = "";
            }
            ValidarAmbos();
        }

        private void ValidarJugador(TextBox textBox, ComboBox comboBox, Label labelError, int numeroJugador)
        {
            //metodo que valida los datos de cada jugador
            string nombre = textBox.Text.Trim();
            string color = comboBox.SelectedItem?.ToString();

            if (string.IsNullOrEmpty(nombre))
            {
                labelError.Text = "Nombre vacío.";
                ObtenerCheckBox(numeroJugador).Checked = false;
                return;
            }

            if (nombre.Length > 10)
            {
                labelError.Text = "Máximo 10 caracteres.";
                ObtenerCheckBox(numeroJugador).Checked = false;
                return;
            }

            if (nombre.ToLower() == "ejercito neutral")
            {
                labelError.Text = "No puedes usar 'Ejercito Neutral'.";
                ObtenerCheckBox(numeroJugador).Checked = false;
                return;
            }

      
            if (NombreYaEnUso(nombre, numeroJugador))
            {
                labelError.Text = "Nombre ya en uso.";
                ObtenerCheckBox(numeroJugador).Checked = false;
                return;
            }

            if (string.IsNullOrEmpty(color))
            {
                labelError.Text = "Seleccione un color.";
                ObtenerCheckBox(numeroJugador).Checked = false;
                return;
            }

            
            if (ColorYaEnUso(color, numeroJugador))
            {
                labelError.Text = "Color ya en uso.";
                ObtenerCheckBox(numeroJugador).Checked = false;
                return;
            }

            labelError.Text = "";
        }

        private CheckBox ObtenerCheckBox(int numeroJugador)
        {
            switch (numeroJugador)
            {
                case 1: return checkBox1;
                case 2: return checkBox2;
                case 3: return checkBox3;
                default: return null;
            }
        }

        //metods auxiliares para validar nombres y colores
        private bool NombreYaEnUso(string nombre, int jugadorActual)
        {
            for (int i = 1; i <= 3; i++)
            {
                if (i != jugadorActual)
                {
                    TextBox textBox = ObtenerTextBox(i);
                    CheckBox checkBox = ObtenerCheckBox(i);
                    if (checkBox.Checked && textBox.Text.Trim() == nombre)
                        return true;
                }
            }
            return false;
        }

        private bool ColorYaEnUso(string color, int jugadorActual)
        {
            for (int i = 1; i <= 3; i++)
            {
                if (i != jugadorActual)
                {
                    ComboBox comboBox = ObtenerComboBox(i);
                    CheckBox checkBox = ObtenerCheckBox(i);
                    if (checkBox.Checked && comboBox.SelectedItem?.ToString() == color)
                        return true;
                }
            }
            return false;
        }

        private TextBox ObtenerTextBox(int numero)
        {
            switch (numero)
            {
                case 1: return textBox1;
                case 2: return textBox2;
                case 3: return textBox3;
                default: return null;
            }
        }

        private ComboBox ObtenerComboBox(int numero)
        {
            switch (numero)
            {
                case 1: return comboBox1;
                case 2: return comboBox2;
                case 3: return comboBox3;
                default: return null;
            }
        }
        
        private void label1_Click(object sender, EventArgs e) { }
        private void textBox1_TextChanged(object sender, EventArgs e) { }
        private void textBox2_TextChanged(object sender, EventArgs e) { }
        private void label3_Click(object sender, EventArgs e) { }
        private void label3_Click_1(object sender, EventArgs e) { }
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e) { }
        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e) { }
    }
}