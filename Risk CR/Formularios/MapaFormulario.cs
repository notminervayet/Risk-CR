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

          
            territorios.Add(new Territorio(1, "UCR", "San José"));
            territorios.Add(new Territorio(2, "ParqueDiv", "San José"));
            territorios.Add(new Territorio(3, "desampa", "San José"));
            territorios.Add(new Territorio(4, "Empresarios Unidos", "San José"));
            territorios.Add(new Territorio(5, "Plazadelsol", "San José"));
            territorios.Add(new Territorio(6, "Lacali", "San José"));
            territorios.Add(new Territorio(7, "AvenidaC", "San José"));

        
            territorios.Add(new Territorio(8, "PiedadesNorte", "Alajuela"));
            territorios.Add(new Territorio(9, "SanRamón", "Alajuela"));
            territorios.Add(new Territorio(10, "CasaEythan", "Alajuela"));
            territorios.Add(new Territorio(11, "SanCarlos", "Alajuela"));
            territorios.Add(new Territorio(12, "SuperMario", "Alajuela"));
            territorios.Add(new Territorio(13, "Bolivar", "Alajuela"));
            territorios.Add(new Territorio(14, "Palmares", "Alajuela"));

           
            territorios.Add(new Territorio(15, "TEC", "Cartago"));
            territorios.Add(new Territorio(16, "LasRuinas", "Cartago"));
            territorios.Add(new Territorio(17, "LaBasilica", "Cartago"));
            territorios.Add(new Territorio(18, "Irazu", "Cartago"));
            territorios.Add(new Territorio(19, "ApartaEythan", "Cartago"));
            territorios.Add(new Territorio(20, "ApartaLitzy", "Cartago"));
            territorios.Add(new Territorio(21, "Casacampus", "Cartago"));

            
            territorios.Add(new Territorio(22, "Puerto Viejo", "Limon"));
            territorios.Add(new Territorio(23, "Expo", "Limon"));
            territorios.Add(new Territorio(24, "Bataan", "Limon"));
            territorios.Add(new Territorio(25, "CasadeLitzy", "Limon"));
            territorios.Add(new Territorio(26, "LaArgentina", "Limon"));
            territorios.Add(new Territorio(27, "Cariari", "Limon"));
            territorios.Add(new Territorio(28, "Yugo", "Limon"));

          
            territorios.Add(new Territorio(29, "Samara", "Guanacaste"));
            territorios.Add(new Territorio(30, "Nicoya", "Guanacaste"));
            territorios.Add(new Territorio(31, "Hermosa", "Guanacaste"));
            territorios.Add(new Territorio(32, "Conchal", "Guanacaste"));
            territorios.Add(new Territorio(33, "Avellana", "Guanacaste"));
            territorios.Add(new Territorio(34, "Bejuco", "Guanacaste"));
            territorios.Add(new Territorio(35, "Coyote", "Guanacaste"));

            
            territorios.Add(new Territorio(36, "Dominical", "Puntarenas"));
            territorios.Add(new Territorio(37, "Parrita", "Puntarenas"));
            territorios.Add(new Territorio(38, "ElPuerto", "Puntarenas"));
            territorios.Add(new Territorio(39, "Jaco", "Puntarenas"));
            territorios.Add(new Territorio(40, "Esparza", "Puntarenas"));
            territorios.Add(new Territorio(41, "Quepos", "Puntarenas"));
            territorios.Add(new Territorio(42, "Golfito", "Puntarenas"));
        }

        private void EstablecerAdyacencias()
        {

            //San jose
            ConectarAdyacentes(1, new int[] { 2, 3, 4, 5, 6, 9 });//
            ConectarAdyacentes(2, new int[] { 1, 3, 8, 9 });//
            ConectarAdyacentes(3, new int[] { 8, 2, 1, 5, 4 });//
            ConectarAdyacentes(4, new int[] { 3, 5, 17, 7 });
            ConectarAdyacentes(5, new int[] { 1, 3, 4, 17, 16, 15, 6 });
            ConectarAdyacentes(6, new int[] { 1, 5, 15, 9, 26 });
            ConectarAdyacentes(7, new int[] { 4, 17, 21, 38 });


            //guanacaste
            ConectarAdyacentes(8, new int[] { 2, 9, 10, 34 });
            ConectarAdyacentes(9, new int[] { 2, 1, 6, 26, 24, 10, 14, 8 });
            ConectarAdyacentes(10, new int[] { 8, 9, 14, 13, 12, 11, 34 });
            ConectarAdyacentes(11, new int[] { 30, 32, 34, 10, 12 });
            ConectarAdyacentes(12, new int[] { 11, 10, 13, 30 });
            ConectarAdyacentes(13, new int[] { 12, 10, 14 });
            ConectarAdyacentes(14, new int[] { 13, 14, 9, 24, 22 });


            ConectarAdyacentes(15, new int[] { 6, 5, 16, 18, 28, 27, 24, 26 });
            ConectarAdyacentes(16, new int[] { 5, 17, 20, 18, 15 });
            ConectarAdyacentes(17, new int[] { 5, 4, 7, 21, 20, 16 });
            ConectarAdyacentes(18, new int[] { 28, 15, 16, 20, 19, });
            ConectarAdyacentes(19, new int[] { 18, 20, 36 });
            ConectarAdyacentes(20, new int[] { 16, 17, 18, 19, 21, 36, 37 });
            ConectarAdyacentes(21, new int[] { 7, 17, 20, 37, 38 });


            ConectarAdyacentes(22, new int[] { 14, 24, 23, 25 });
            ConectarAdyacentes(23, new int[] { 22, 25 });
            ConectarAdyacentes(24, new int[] { 14, 7 });
            ConectarAdyacentes(25, new int[] { 1, 7 });
            ConectarAdyacentes(26, new int[] { 1, 7 });
            ConectarAdyacentes(27, new int[] { 1, 7 });
            ConectarAdyacentes(28, new int[] { 1, 7 });


            ConectarAdyacentes(29, new int[] { 1, 7 });
            ConectarAdyacentes(30, new int[] { 1, 7 });
            ConectarAdyacentes(31, new int[] { 1, 7 });
            ConectarAdyacentes(32, new int[] { 1, 7 });
            ConectarAdyacentes(33, new int[] { 1, 7 });
            ConectarAdyacentes(34, new int[] { 1, 7 });
            ConectarAdyacentes(35, new int[] { 1, 7 });

            ConectarAdyacentes(36, new int[] { 1, 7 });
            ConectarAdyacentes(37, new int[] { 1, 7 });
            ConectarAdyacentes(38, new int[] { 1, 7 });
            ConectarAdyacentes(39, new int[] { 1, 7 });
            ConectarAdyacentes(40, new int[] { 1, 7 });
            ConectarAdyacentes(41, new int[] { 1, 7 });
            ConectarAdyacentes(42, new int[] { 1, 7 });




        }

        
        private void ConectarAdyacentes(int idTerritorio, int[] idsAdyacentes)
        {
            Territorio territorio = BuscarTerritorioPorId(idTerritorio);
            foreach (int idAdyacente in idsAdyacentes)
            {
                Territorio adyacente = BuscarTerritorioPorId(idAdyacente);
                if (adyacente != null && territorio != null)
                {
                    territorio.AgregarAdyacente(adyacente);
                    adyacente.AgregarAdyacente(territorio);
                }
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
            
            Dictionary<int, Point> posiciones = new Dictionary<int, Point>()
            {
                // San José
                {1, new Point(547, 285)},  // UCR
                {2, new Point(500, 300)},  // ParqueDiv
                {3, new Point(540, 340)},  // Desampa
                {4, new Point(600, 357)},  // Empresarios Unidos
                {5, new Point(610, 300)},  // Plazadel sol
                {6, new Point(585, 235)},  // Lacali
                {7, new Point(640, 410)},  // AvenidaC
                
                // Alajuela
                {8, new Point(455, 265)},  // PiedadesNorte
                {9, new Point(537, 245)},  // SanRamón
                {10, new Point(465, 200)}, // CasaEythan
                {11, new Point(410, 150)}, // SanCarlos
                {12, new Point(415, 105)}, // SuperMario
                {13, new Point(475, 100)}, // Bolivar
                {14, new Point(525, 125)}, // Palmares

                // Cartago
                {15, new Point(650, 250)}, // TEC
                {16, new Point(690, 300)}, // LasRuinas
                {17, new Point(655, 350)}, // LaBasilica
                {18, new Point(730, 250)}, // Irazu
                {19, new Point(800, 300)}, // ApartaEythan
                {20, new Point(730, 320)}, // ApartaLitzy
                {21, new Point(695, 415)}, // Casacampus

                // limon
                {22, new Point(590, 127)}, // Puerto Viejo
                {23, new Point(662, 123)}, // Expo
                {24, new Point(590, 167)}, // Bataan
                {25, new Point(670, 166)}, // CasadeLitzy
                {26, new Point(585, 202)}, // LaArgentina
                {27, new Point(650, 202)}, // Cariari
                {28, new Point(717, 206)}, // Yugo

                // Guanacaste
                {29, new Point(272, 57)}, // Samara
                {30, new Point(350, 80)}, // Nicoya
                {31, new Point(292, 127)}, // Hermosa
                {32, new Point(355, 160)}, // Conchal
                {33, new Point(275, 220)}, // Avellana
                {34, new Point(395, 200)}, // Bejuco
                {35, new Point(355, 280)}, // Coyote

                // Puntarenas
                {36, new Point(766, 354)}, // Dominical
                {37, new Point(760, 410)}, // Parrita
                {38, new Point(700, 455)}, // El Puerto
                {39, new Point(815, 455)}, // Jaco
                {40, new Point(760, 462)}, // Esparza
                {41, new Point(775, 530)}, // Quepos
                {42, new Point(675, 530)}  // Golfitoo
            };

            foreach (Territorio territorio in territorios)
            {
                if (posiciones.ContainsKey(territorio.Id))
                {
                    Button btn = new Button();
                    btn.Size = new Size(60, 30);  
                    btn.Location = posiciones[territorio.Id];
                    btn.BackColor = Color.FromArgb(180, Color.LightGray);
                    btn.FlatStyle = FlatStyle.Flat;
                    btn.FlatAppearance.BorderSize = 0;
                    btn.Font = new Font("Arial", 7, FontStyle.Bold);
                    btn.TextAlign = ContentAlignment.MiddleCenter;

                   
                    btn.Tag = territorio;
                    territorio.BotonAsociado = btn;

                    territorio.ActualizarVisualmente();

                   
                    btn.Click += (sender, e) => {
                        Territorio terrClickeado = (Territorio)((Button)sender).Tag;
                        MessageBox.Show($"{terrClickeado.Nombre}\nTropas: {terrClickeado.Tropas}\nAdyacentes: {terrClickeado.ObtenerNombresAdyacentes()}");
                    };

                    this.Controls.Add(btn);
                    btn.BringToFront();
                }
            }
        }

        private void MapaFormulario_Load(object sender, EventArgs e)
        {

        }
    }
}