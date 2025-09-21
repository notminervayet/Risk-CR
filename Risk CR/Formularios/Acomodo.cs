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
        public Acomodo()
        {
            InitializeComponent();

        }

        private void Acomodo_Load(object sender, EventArgs e)
        {
            pictureBoxPlay.Enabled = false;
            pictureBoxPlay.Visible = false;

            comboBox1.Items.Add("Rojo");
            comboBox1.Items.Add("Azul");
            comboBox1.Items.Add("Verde");
            comboBox1.Items.Add("Morado");
            comboBox1.Items.Add("Amarillo");

            comboBox2.Items.Add("Rojo");
            comboBox2.Items.Add("Azul");
            comboBox2.Items.Add("Verde");
            comboBox2.Items.Add("Morado");
            comboBox2.Items.Add("Amarillo");

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void pictureBoxPlay_Click(object sender, EventArgs e)
        {
            string nombre1 = textBox1.Text.Trim();
            string nombre2 = textBox2.Text.Trim();
       
            string color1 = comboBox1.SelectedItem?.ToString();
            string color2 = comboBox2.SelectedItem?.ToString();
        
            Jugador jugador1 = new Jugador ( nombre1, color1 );
            Jugador jugador2 = new Jugador ( nombre2,color2 );
            Jugador ejercito_neutral= new Jugador("Ejercito Neutral", "Gris");

            ListaGod<Jugador> jugadores = new ListaGod<Jugador> { jugador1, jugador2 };

            this.Hide();
            MapaFormulario mapa = new MapaFormulario();
            mapa.ShowDialog();
            this.Show();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void ValidarAmbos()
        {
            string nombre1 = textBox1.Text.Trim();
            string nombre2 = textBox2.Text.Trim();

            string color1 = comboBox1.SelectedItem?.ToString();
            string color2 = comboBox2.SelectedItem?.ToString();

            bool nombre1Valido = checkBox1.Checked && !string.IsNullOrEmpty(nombre1) && nombre1.Length <= 10;
            bool nombre2Valido = checkBox2.Checked && !string.IsNullOrEmpty(nombre2) && nombre2.Length <= 10;

            bool color1Valido = !string.IsNullOrEmpty(color1);
            bool color2Valido = !string.IsNullOrEmpty(color2);
            bool coloresDistintos = color1Valido && color2Valido && color1 != color2;

            bool todoCorrecto = nombre1Valido && nombre2Valido && color1Valido && color2Valido && coloresDistintos;

            pictureBoxPlay.Enabled = todoCorrecto;
            pictureBoxPlay.Visible = todoCorrecto;

        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
       
        {
            string nombre = textBox1.Text.Trim();
            string color1 = comboBox1.SelectedItem?.ToString();
            if (checkBox1.Checked)
            {
                if (string.IsNullOrEmpty(nombre))
                {
                    labelad.Text = "Nombre vacío.";
                    checkBox1.Checked = false;
                }
                else if (nombre.Length > 10)
                {
                    labelad.Text = "Máximo 10 caracteres.";
                    checkBox1.Checked = false;
                }
                else if (nombre == textBox2.Text.Trim() && checkBox2.Checked)
                {
                    labelad.Text = "Nombre ya en uso.";
                    checkBox1.Checked = false;
                }
                else if (string.IsNullOrEmpty(color1))
                {
                    labelad.Text = "Seleccione un color.";
                    checkBox1.Checked = false;
                }
                else if (color1 == comboBox2.SelectedItem?.ToString() && checkBox2.Checked)
                {
                    labelad.Text = "Color ya en uso.";
                    checkBox1.Checked = false;
                }
                else
                {
                    labelad.Text = "";
    }
}

            ValidarAmbos();
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            string nombre = textBox2.Text.Trim();
            string color = comboBox2.SelectedItem?.ToString();
            if (checkBox2.Checked)
            {
                if (string.IsNullOrEmpty(nombre))
                {
                    labelad2.Text = "Nombre vacío.";
                    checkBox2.Checked = false;
                }
                else if (nombre.Length > 10)
                {
                    labelad2.Text = "Máximo 10 caracteres.";
                    checkBox2.Checked = false;
                }
                else if (nombre == textBox1.Text.Trim() && checkBox1.Checked)
                {
                    labelad2.Text = "Nombre ya en uso.";
                    checkBox2.Checked = false;
                }
                else if (string.IsNullOrEmpty(color))
                {
                    labelad2.Text = "Seleccione un color.";
                    checkBox2.Checked = false;
                }
                else if (color == comboBox1.SelectedItem?.ToString() && checkBox1.Checked)
                {
                    labelad2.Text = "Color ya en uso.";
                    checkBox2.Checked = false;
                }
                else
                {
                    labelad2.Text = "";
                }
            }

            ValidarAmbos();
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click_1(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
           

        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
          
        }
    }
}

