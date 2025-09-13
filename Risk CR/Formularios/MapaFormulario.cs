using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
namespace Risk_CR
{
    public partial class MapaFormulario : Form
    {
        private PictureBox picMapa;

        public MapaFormulario()
        {
            InitializeComponent();
            ConfigurarVentana();
            InicializarMapa();
        }

        private void ConfigurarVentana()
        {
            this.Text = "Risk Costa Rica";
            this.Size = new Size(1200, 800);
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.BackColor = Color.LightBlue; // ← FONDO CELESTE
        }

        private void InicializarMapa()
        {
            picMapa = new PictureBox();
            picMapa.Dock = DockStyle.Fill;
            picMapa.SizeMode = PictureBoxSizeMode.Zoom;

            // Ruta de la imagen
            string ruta = Path.Combine(Application.StartupPath, "imagenes", "MapaImagenPNG.png");
            picMapa.Image = Image.FromFile(ruta);

            this.Controls.Add(picMapa);
        }
    }
}