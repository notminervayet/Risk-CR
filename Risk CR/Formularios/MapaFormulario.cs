using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace Risk_CR
{
    public partial class MapaFormulario : Form
    {
        private PictureBox picMapa;
        private List<Territorio> territorios;

        public MapaFormulario()
        {
            InitializeComponent();
            ConfigurarVentana();
            InicializarTerritorios();      // ← Primero: crear objetos territorio
            EstablecerAdyacencias();       // ← Segundo: conectar territorios
            InicializarMapa();             // ← Tercero: cargar imagen de fondo
            CrearBotonesTerritorios();     // ← Cuarto: crear botones visuales
        }

        private void ConfigurarVentana()
        {
            this.Text = "Risk Costa Rica";
            this.Size = new Size(1200, 800);
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.BackColor = Color.LightBlue;
        }
//guagua
        private void InicializarTerritorios()
        {
            territorios = new List<Territorio>();

            // San José (7 territorios)
            territorios.Add(new Territorio(1, "San José Centro", "San José"));
            territorios.Add(new Territorio(2, "Escazú", "San José"));
            territorios.Add(new Territorio(3, "Desamparados", "San José"));
            territorios.Add(new Territorio(4, "Puriscal", "San José"));
            territorios.Add(new Territorio(5, "Tarrazú", "San José"));
            territorios.Add(new Territorio(6, "Aserrí", "San José"));
            territorios.Add(new Territorio(7, "Mora", "San José"));

            // Alajuela (7 territorios)
            territorios.Add(new Territorio(8, "Alajuela Centro", "Alajuela"));
            territorios.Add(new Territorio(9, "San Ramón", "Alajuela"));
            territorios.Add(new Territorio(10, "Grecia", "Alajuela"));
            territorios.Add(new Territorio(11, "San Carlos", "Alajuela"));
            territorios.Add(new Territorio(12, "Upala", "Alajuela"));
            territorios.Add(new Territorio(13, "Los Chiles", "Alajuela"));
            territorios.Add(new Territorio(14, "Guatuso", "Alajuela"));

            // Agregar aquí los demás: Cartago, Heredia, Guanacaste, Puntarenas...
            // ... (completar hasta 42 territorios)
        }

        private void EstablecerAdyacencias()
        {
            // Ejemplo: San José Centro es adyacente a Escazú y Desamparados
            Territorio sanJose = BuscarTerritorioPorId(1);
            Territorio escazu = BuscarTerritorioPorId(2);
            Territorio desamparados = BuscarTerritorioPorId(3);

            sanJose.AgregarAdyacente(escazu);
            sanJose.AgregarAdyacente(desamparados);
            escazu.AgregarAdyacente(sanJose);
            desamparados.AgregarAdyacente(sanJose);

            // Agregar aquí TODAS las adyacencias de los 42 territorios
        }

        private Territorio BuscarTerritorioPorId(int id)
        {
            return territorios.Find(t => t.Id == id);
        }

        private void InicializarMapa()
        {
            picMapa = new PictureBox();
            picMapa.Dock = DockStyle.Fill;
            picMapa.SizeMode = PictureBoxSizeMode.Zoom;

            string ruta = Path.Combine(Application.StartupPath, "imagenes", "MapaImagenPNG.png");
            picMapa.Image = Image.FromFile(ruta);

            this.Controls.Add(picMapa);
            picMapa.SendToBack();
        }

        private void CrearBotonesTerritorios()
        {
            // Diccionario de posiciones (x, y) para cada territorio - AJUSTAR ESTOS VALORES
            Dictionary<int, Point> posiciones = new Dictionary<int, Point>()
            {
                {1, new Point(400, 300)},  // San José Centro
                {2, new Point(350, 250)},  // Escazú
                {3, new Point(450, 350)},  // Desamparados
                {4, new Point(380, 400)},  // Puriscal
                {5, new Point(320, 450)},  // Tarrazú
                {6, new Point(420, 380)},  // Aserrí
                {7, new Point(480, 320)},  // Mora
                {8, new Point(300, 200)},  // Alajuela Centro
                {9, new Point(250, 150)},  // San Ramón
                {10, new Point(350, 180)}, // Grecia
                // ... agregar posiciones para los 42 territorios
            };

            foreach (Territorio territorio in territorios)
            {
                if (posiciones.ContainsKey(territorio.Id))
                {
                    Button btn = new Button();
                    btn.Size = new Size(100, 50);
                    btn.Location = posiciones[territorio.Id];
                    btn.BackColor = Color.LightGray;
                    btn.ForeColor = Color.Black;
                    btn.Font = new Font("Arial", 8, FontStyle.Bold);
                    btn.TextAlign = ContentAlignment.MiddleCenter;

                    // VINCULACIÓN BIDIRECCIONAL
                    btn.Tag = territorio;
                    territorio.BotonAsociado = btn;

                    // Actualizar texto inicial
                    territorio.ActualizarVisualmente();

                    // Evento clic - temporal para testing
                    btn.Click += (sender, e) => {
                        Territorio terrClickeado = (Territorio)((Button)sender).Tag;
                        MessageBox.Show($"Territorio: {terrClickeado.Nombre}\n" +
                                      $"Provincia: {terrClickeado.Provincia}\n" +
                                      $"Tropas: {terrClickeado.Tropas}\n" +
                                      $"Adyacentes: {terrClickeado.ObtenerNombresAdyacentes()}");
                    };

                    this.Controls.Add(btn);
                    btn.BringToFront();
                }
            }
        }
    }
}