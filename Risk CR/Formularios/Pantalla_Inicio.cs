using System;
using System.IO;
using System.Media;
using System.Windows.Forms;
using Risk_CR.Formularios;

namespace Risk_CR
{
    
    public partial class CR_Risk : Form
    {

        public CR_Risk()
        {
            InitializeComponent();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
           
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        
        private void Pantalla_Inicio_Load(object sender, EventArgs e)
        {
            //musica de fondo
            string ruta = Path.Combine(Application.StartupPath,"Formularios", "sonido", "Selva.wav");
            SoundPlayer selva = new SoundPlayer(ruta);
            selva.Play();
            selva.PlayLooping();
        }

        private void pictureBox3_Click_1(object sender, EventArgs e)
        {
            this.Hide();
            Acomodo pantalla = new Acomodo();
            pantalla.ShowDialog();
            this.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //this.Hide();
            //Tirada pantalla2 = new Tirada();
            //pantalla2.ShowDialog();
            //this.Show();
        }
    }
}

