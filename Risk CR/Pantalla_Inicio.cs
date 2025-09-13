using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Media;

namespace Risk_CR
{
    public partial class CR_Risk : Form
    {
        public CR_Risk()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

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
            SoundPlayer player = new SoundPlayer(@"C:\Users\litzy\Documents\PROYECTOLINEYTHAN\musica\Selva.wav");
            player.Play();
        }
    }
}
