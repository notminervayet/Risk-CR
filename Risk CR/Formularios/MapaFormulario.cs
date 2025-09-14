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
            InicializarTerritorios();
            EstablecerAdyacencias();
            InicializarMapa();
            CrearBotonesTerritorios();
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

        private void InicializarTerritorios()
        {
            territorios = new List<Territorio>();

            // San José (7 territorios)
            territorios.Add(new Territorio(1, "UCR", "San José"));
            territorios.Add(new Territorio(2, "ParqueDiv", "San José"));
            territorios.Add(new Territorio(3, "Caribeños", "San José"));
            territorios.Add(new Territorio(4, "Empresarios Unidos", "San José"));
            territorios.Add(new Territorio(5, "Plaza del sol", "San José"));
            territorios.Add(new Territorio(6, "La cali", "San José"));
            territorios.Add(new Territorio(7, "AvenidaC", "San José"));

            // Alajuela (7 territorios)
            territorios.Add(new Territorio(8, "Piedades Norte", "Alajuela"));
            territorios.Add(new Territorio(9, "San Ramón", "Alajuela"));
            territorios.Add(new Territorio(10, "Casa de Eythan", "Alajuela"));
            territorios.Add(new Territorio(11, "San Carlos", "Alajuela"));
            territorios.Add(new Territorio(12, "SuperMario", "Alajuela"));
            territorios.Add(new Territorio(13, "Bolivar", "Alajuela"));
            territorios.Add(new Territorio(14, "Palmares", "Alajuela"));

            // Cartago (7 territorios)
            territorios.Add(new Territorio(15, "TEC", "Cartago"));
            territorios.Add(new Territorio(16, "Las Ruinas", "Cartago"));
            territorios.Add(new Territorio(17, "La Basilica", "Cartago"));
            territorios.Add(new Territorio(18, "Irazu", "Cartago"));
            territorios.Add(new Territorio(19, "Aparta Eythan", "Cartago"));
            territorios.Add(new Territorio(20, "Aparta Litzy", "Cartago"));
            territorios.Add(new Territorio(21, "Casa campus", "Cartago"));

            // Limon (7 territorios)
            territorios.Add(new Territorio(22, "Puerto Viejo", "Heredia"));
            territorios.Add(new Territorio(23, "Expo", "Heredia"));
            territorios.Add(new Territorio(24, "Bataan", "Heredia"));
            territorios.Add(new Territorio(25, "Casa de Litzy", "Heredia"));
            territorios.Add(new Territorio(26, "La Argentina", "Heredia"));
            territorios.Add(new Territorio(27, "Cariari", "Heredia"));
            territorios.Add(new Territorio(28, "Yugo", "Heredia"));

            // Guanacaste (7 territorios)
            territorios.Add(new Territorio(29, "Samara", "Guanacaste"));
            territorios.Add(new Territorio(30, "Nicoya", "Guanacaste"));
            territorios.Add(new Territorio(31, "Hermosa", "Guanacaste"));
            territorios.Add(new Territorio(32, "Conchal", "Guanacaste"));
            territorios.Add(new Territorio(33, "Avellana", "Guanacaste"));
            territorios.Add(new Territorio(34, "Bejuco", "Guanacaste"));
            territorios.Add(new Territorio(35, "Coyote", "Guanacaste"));

            // Puntarenas (7 territorios)
            territorios.Add(new Territorio(36, "Dominical", "Puntarenas"));
            territorios.Add(new Territorio(37, "Parrita", "Puntarenas"));
            territorios.Add(new Territorio(38, "El Puerto", "Puntarenas"));
            territorios.Add(new Territorio(39, "Jaco", "Puntarenas"));
            territorios.Add(new Territorio(40, "Esparza", "Puntarenas"));
            territorios.Add(new Territorio(41, "Quepos", "Puntarenas"));
            territorios.Add(new Territorio(42, "Golfito", "Puntarenas"));
        }

        private void EstablecerAdyacencias()
        {
            // EJEMPLO: Cómo establecer adyacencias - TÚ COMPLETARÁS EL RESTO

            // San José
            ConectarAdyacentes(1, new int[] { 2, 3, 4 });  // Central es adyacente a Escazú, Desamparados, Puriscal
            ConectarAdyacentes(2, new int[] { 1, 4, 5 });  // Escazú es adyacente a Central, Puriscal, Tarrazú
            ConectarAdyacentes(3, new int[] { 1, 6, 7 });  // Desamparados es adyacente a Central, Aserrí, Mora

            // Alajuela
            ConectarAdyacentes(8, new int[] { 9, 10, 11 }); // Alajuela es adyacente a San Ramón, Grecia, San Carlos

            // Cartago
            ConectarAdyacentes(15, new int[] { 16, 17, 18 }); // Cartago es adyacente a Paraíso, La Unión, Jiménez

            // CONECTAR PROVINCIAS ENTRE SÍ (ejemplo):
            ConectarAdyacentes(1, new int[] { 8 });  // San José Central - Alajuela
            ConectarAdyacentes(8, new int[] { 1, 22 }); // Alajuela - San José Central y Heredia

            // TÚ deberás completar TODAS las adyacencias restantes
            // siguiendo el mapa de Costa Rica real
        }

        // Método helper para facilitar las conexiones
        private void ConectarAdyacentes(int idTerritorio, int[] idsAdyacentes)
        {
            Territorio territorio = BuscarTerritorioPorId(idTerritorio);
            foreach (int idAdyacente in idsAdyacentes)
            {
                Territorio adyacente = BuscarTerritorioPorId(idAdyacente);
                territorio.AgregarAdyacente(adyacente);
            }
        }

        private Territorio BuscarTerritorioPorId(int id)
        {
            foreach (Territorio territorio in territorios)
            {
                if (territorio.Id == id)
                    return territorio;
            }
            return null;
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
            // DICCIONARIO DE POSICIONES - TÚ AJUSTARÁS ESTOS VALORES
            Dictionary<int, Point> posiciones = new Dictionary<int, Point>()
            {
                // San José
                {1, new Point(400, 300)},  // Central
                {2, new Point(350, 250)},  // Escazú
                {3, new Point(450, 350)},  // Desamparados
                {4, new Point(380, 400)},  // Puriscal
                {5, new Point(320, 450)},  // Tarrazú
                {6, new Point(420, 380)},  // Aserrí
                {7, new Point(480, 320)},  // Mora
                
                // Alajuela
                {8, new Point(300, 200)},  // Alajuela
                {9, new Point(250, 150)},  // San Ramón
                {10, new Point(350, 180)}, // Grecia
                {11, new Point(280, 250)}, // San Carlos
                {12, new Point(220, 300)}, // Upala
                {13, new Point(180, 350)}, // Los Chiles
                {14, new Point(240, 400)}, // Guatuso
                
                // TÚ agregarás las posiciones para los 42 territorios
                // basado en tu mapa de Costa Rica
            };

            foreach (Territorio territorio in territorios)
            {
                if (posiciones.ContainsKey(territorio.Id))
                {
                    Button btn = new Button();
                    btn.Size = new Size(60, 30);  // ← BOTONES MÁS PEQUEÑOS
                    btn.Location = posiciones[territorio.Id];  // ← POSICIÓN del diccionario
                    btn.BackColor = Color.FromArgb(180, Color.LightGray);
                    btn.FlatStyle = FlatStyle.Flat;
                    btn.FlatAppearance.BorderSize = 0;
                    btn.Font = new Font("Arial", 7, FontStyle.Bold);
                    btn.TextAlign = ContentAlignment.MiddleCenter;

                    // VINCULACIÓN
                    btn.Tag = territorio;
                    territorio.BotonAsociado = btn;

                    territorio.ActualizarVisualmente();

                    // Evento clic para testing
                    btn.Click += (sender, e) => {
                        Territorio terrClickeado = (Territorio)((Button)sender).Tag;
                        MessageBox.Show($"{terrClickeado.Nombre}\nTropas: {terrClickeado.Tropas}\nAdyacentes: {terrClickeado.ObtenerNombresAdyacentes()}");
                    };

                    this.Controls.Add(btn);
                    btn.BringToFront();
                }
            }
        }
    }
}