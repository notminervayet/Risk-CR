using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

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

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            this.Hide();
            MapaFormulario mapa = new MapaFormulario();
            mapa.ShowDialog();
            this.Show();
        }
    }
}
