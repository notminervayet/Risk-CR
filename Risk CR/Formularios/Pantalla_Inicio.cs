using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

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
            string ruta = Path.Combine(Application.StartupPath,"Formularios", "sonido", "Selva.wav");
            SoundPlayer selva = new SoundPlayer(ruta);
            selva.Play();
            selva.PlayLooping();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            MapaFormulario mapa = new MapaFormulario();
            mapa.Show();
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            config mapa = new config();
            mapa.Show();
            this.Hide();
        }

        private void pictureBox3_Click_1(object sender, EventArgs e)
        {
            config mapa = new config();
            mapa.Show();
            this.Hide();
        }
    }
}
