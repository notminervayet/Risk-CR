using Risk_CR.Formularios;
using Risk_CR.Jugadores;
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
        private ListaGod<Territorio> territorios;
        private Label lblJugadorActual;
        private Label lblTropasDisponibles;
        private Label lblFase;
        private ListaGod<Jugador> jugadores;
        //private Territorio territorioOrigen ;
        //private Territorio territorioDestino;

        private Button btnSiguienteFase;




        public MapaFormulario(ListaGod<Jugador> jugadores)
        {
            InitializeComponent();
            this.jugadores = jugadores;

            ConfigurarVentana();
            InicializarControlesUI();
            InicializarTerritorios();
            EstablecerAdyacencias();
            InicializarMapa();
            CrearBotonesTerritorios();

      
            Juego.Instance.IniciarJuego(this.jugadores, territorios);
            ActualizarUI();
        }

        private void ConfigurarVentana()
        {
            this.Text = "Risk Costa Rica";
            this.Size = new Size(1200, 800);
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.StartPosition = FormStartPosition.CenterScreen;
        }

        private void InicializarControlesUI()
        {
     
            Panel panelInfo = new Panel();
            panelInfo.Size = new Size(200, 150);
            panelInfo.Location = new Point(10, 10);
            panelInfo.BackColor = Color.FromArgb(180, Color.White);

            lblJugadorActual = new Label();
            lblJugadorActual.Location = new Point(10, 10);
            lblJugadorActual.Size = new Size(180, 20);
            lblJugadorActual.ForeColor = Color.Black;

            lblTropasDisponibles = new Label();
            lblTropasDisponibles.Location = new Point(10, 40);
            lblTropasDisponibles.Size = new Size(180, 20);
            lblTropasDisponibles.ForeColor = Color.Black;

            lblFase = new Label();
            lblFase.Location = new Point(10, 70);
            lblFase.Size = new Size(180, 20);
            lblFase.ForeColor = Color.Black;

            panelInfo.Controls.Add(lblJugadorActual);
            panelInfo.Controls.Add(lblTropasDisponibles);
            panelInfo.Controls.Add(lblFase);

            this.Controls.Add(panelInfo);
            panelInfo.BringToFront();

           

           
            btnSiguienteFase = new Button();
            btnSiguienteFase.Text = "Siguiente Fase";
            btnSiguienteFase.Size = new Size(150, 30);
            btnSiguienteFase.Location = new Point(25, 300); 
            btnSiguienteFase.Click += BtnSiguienteFase_Click;

            this.Controls.Add(btnSiguienteFase);
            btnSiguienteFase.BringToFront();



        }


        private void InicializarTerritorios()
        {
            territorios = new ListaGod<Territorio>();

            // San José
            territorios.Agregar(new Territorio(1, "UCR", "San José"));
            territorios.Agregar(new Territorio(2, "Plazasol", "San José"));
            territorios.Agregar(new Territorio(3, "desampa", "San José"));
            territorios.Agregar(new Territorio(4, "MuseoNiñ", "San José"));
            territorios.Agregar(new Territorio(5, "Vinet", "San José"));
            territorios.Agregar(new Territorio(6, "Cali", "San José"));
            territorios.Agregar(new Territorio(7, "UCIMED", "San José"));

            // Alajuela
            territorios.Agregar(new Territorio(8, "PiedadesN", "Alajuela"));
            territorios.Agregar(new Territorio(9, "SnRamón", "Alajuela"));
            territorios.Agregar(new Territorio(10, "CasaEythan", "Alajuela"));
            territorios.Agregar(new Territorio(11, "SnCarlos", "Alajuela"));
            territorios.Agregar(new Territorio(12, "SuperM", "Alajuela"));
            territorios.Agregar(new Territorio(13, "Bolivar", "Alajuela"));
            territorios.Agregar(new Territorio(14, "Palmares", "Alajuela"));

            // Cartago
            territorios.Agregar(new Territorio(15, "TEC", "Cartago"));
            territorios.Agregar(new Territorio(16, "LasRuinas", "Cartago"));
            territorios.Agregar(new Territorio(17, "LaBasilica", "Cartago"));
            territorios.Agregar(new Territorio(18, "Irazu", "Cartago"));
            territorios.Agregar(new Territorio(19, "Eythan´s", "Cartago"));
            territorios.Agregar(new Territorio(20, "Litzy´s", "Cartago"));
            territorios.Agregar(new Territorio(21, "Casacampus", "Cartago"));

            // Limon
            territorios.Agregar(new Territorio(22, "P.Viejo", "Limon"));
            territorios.Agregar(new Territorio(23, "Expo", "Limon"));
            territorios.Agregar(new Territorio(24, "Bataan", "Limon"));
            territorios.Agregar(new Territorio(25, "CasaLitzy", "Limon"));
            territorios.Agregar(new Territorio(26, "Argentina", "Limon"));
            territorios.Agregar(new Territorio(27, "Cariari", "Limon"));
            territorios.Agregar(new Territorio(28, "Yugo", "Limon"));

            // Guanacaste
            territorios.Agregar(new Territorio(29, "Samara", "Guanacaste"));
            territorios.Agregar(new Territorio(30, "Nicoya", "Guanacaste"));
            territorios.Agregar(new Territorio(31, "Hermosa", "Guanacaste"));
            territorios.Agregar(new Territorio(32, "Conchal", "Guanacaste"));
            territorios.Agregar(new Territorio(33, "Avellana", "Guanacaste"));
            territorios.Agregar(new Territorio(34, "Bejuco", "Guanacaste"));
            territorios.Agregar(new Territorio(35, "Coyote", "Guanacaste"));

            // Puntarenas
            territorios.Agregar(new Territorio(36, "Dominical", "Puntarenas"));
            territorios.Agregar(new Territorio(37, "Parrita", "Puntarenas"));
            territorios.Agregar(new Territorio(38, "ElPuerto", "Puntarenas"));
            territorios.Agregar(new Territorio(39, "Jaco", "Puntarenas"));
            territorios.Agregar(new Territorio(40, "Esparza", "Puntarenas"));
            territorios.Agregar(new Territorio(41, "Quepos", "Puntarenas"));
            territorios.Agregar(new Territorio(42, "Golfito", "Puntarenas"));
        }

        private void EstablecerAdyacencias()
        {
            // San jose
            ConectarAdyacentes(1, new int[] { 2, 3, 4, 5, 6, 9 });
            ConectarAdyacentes(2, new int[] { 1, 3, 8, 9 });
            ConectarAdyacentes(3, new int[] { 8, 2, 1, 5, 4 });
            ConectarAdyacentes(4, new int[] { 3, 5, 17, 7 });
            ConectarAdyacentes(5, new int[] { 1, 3, 4, 17, 16, 15, 6 });
            ConectarAdyacentes(6, new int[] { 1, 5, 15, 9, 26 });
            ConectarAdyacentes(7, new int[] { 4, 17, 21, 38 });

            // Guanacaste
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
            ConectarAdyacentes(18, new int[] { 28, 15, 16, 20, 19 });
            ConectarAdyacentes(19, new int[] { 18, 20, 36 });
            ConectarAdyacentes(20, new int[] { 16, 17, 18, 19, 21, 36, 37 });
            ConectarAdyacentes(21, new int[] { 7, 17, 20, 37, 38 });

            ConectarAdyacentes(22, new int[] { 14, 24, 23, 25 });
            ConectarAdyacentes(23, new int[] { 22, 25 });
            ConectarAdyacentes(24, new int[] { 14, 22, 23, 25, 27, 26, 9 });
            ConectarAdyacentes(25, new int[] { 22, 23, 24, 27, 28 });
            ConectarAdyacentes(26, new int[] { 9, 6, 24, 15 });
            ConectarAdyacentes(27, new int[] { 24, 25, 28, 15 });
            ConectarAdyacentes(28, new int[] { 15, 18, 25, 27 });

            ConectarAdyacentes(29, new int[] { 30, 31 });
            ConectarAdyacentes(30, new int[] { 12, 11, 29, 31, 32 });
            ConectarAdyacentes(31, new int[] { 29, 30, 32, 33 });
            ConectarAdyacentes(32, new int[] { 30, 31, 33, 34, 11 });
            ConectarAdyacentes(33, new int[] { 31, 32, 34, 35 });
            ConectarAdyacentes(34, new int[] { 11, 10, 8, 32, 33, 35 });
            ConectarAdyacentes(35, new int[] { 34, 33 });

            ConectarAdyacentes(36, new int[] { 19, 20, 37 });
            ConectarAdyacentes(37, new int[] { 36, 20, 21, 38, 40, 39 });
            ConectarAdyacentes(38, new int[] { 7, 21, 27, 40, 41, 42 });
            ConectarAdyacentes(39, new int[] { 37, 40 });
            ConectarAdyacentes(40, new int[] { 37, 39, 38, 41 });
            ConectarAdyacentes(41, new int[] { 40, 38 });
            ConectarAdyacentes(42, new int[] { 38 });
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
            for (int i = 0; i < territorios.Count; i++)
            {
                Territorio territorio = territorios.Obtener(i);
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

            string rutaTile = Path.Combine(Application.StartupPath, "imagenes", "majestad2.png");
            if (File.Exists(rutaTile))
                picMapa.BackgroundImage = Image.FromFile(rutaTile);

            picMapa.BackgroundImageLayout = ImageLayout.Tile;

            string ruta = Path.Combine(Application.StartupPath, "imagenes", "MapaImagenPNG.png");
            if (File.Exists(ruta))
                picMapa.Image = Image.FromFile(ruta);

            this.Controls.Add(picMapa);
            picMapa.SendToBack();
        }

        private void CrearBotonesTerritorios()
        {
            Dictionary<int, Point> posiciones = new Dictionary<int, Point>()
            {
                // San José
                {1, new Point(547, 285)},
                {2, new Point(500, 300)},
                {3, new Point(540, 340)},
                {4, new Point(600, 357)},
                {5, new Point(610, 300)},
                {6, new Point(585, 235)},
                {7, new Point(640, 410)},
                
                // Alajuela
                {8, new Point(455, 265)},
                {9, new Point(537, 245)}, 
                {10, new Point(465, 200)},
                {11, new Point(410, 150)},
                {12, new Point(415, 105)},
                {13, new Point(475, 100)},
                {14, new Point(525, 125)},
                
                // Cartago
                {15, new Point(650, 250)},
                {16, new Point(690, 300)},
                {17, new Point(655, 350)},
                {18, new Point(730, 250)},
                {19, new Point(800, 300)},
                {20, new Point(730, 320)},
                {21, new Point(695, 415)},
                
                // Limon
                {22, new Point(590, 127)},
                {23, new Point(662, 123)}, 
                {24, new Point(590, 167)},
                {25, new Point(670, 166)},
                {26, new Point(585, 202)}, 
                {27, new Point(650, 202)},
                {28, new Point(717, 206)},
                
                // Guanacaste
                {29, new Point(272, 57)},
                {30, new Point(350, 80)}, 
                {31, new Point(292, 127)},
                {32, new Point(355, 160)},
                {33, new Point(275, 220)},
                {34, new Point(395, 200)},
                {35, new Point(355, 280)},
                
                // Puntarenas
                {36, new Point(766, 354)},
                {37, new Point(760, 410)},
                {38, new Point(700, 455)},
                {39, new Point(815, 455)},
                {40, new Point(760, 462)}, 
                {41, new Point(775, 530)},
                {42, new Point(675, 530)}
            };

            for (int i = 0; i < territorios.Count; i++)
            {
                Territorio territorio = territorios.Obtener(i);
                if (posiciones.ContainsKey(territorio.Id))
                {
                    Button btn = new Button();
                    btn.Size = new Size(55, 35);
                    btn.Location = posiciones[territorio.Id];
                    btn.BackColor = Color.LightGray;
                    btn.FlatStyle = FlatStyle.Flat;
                    btn.FlatAppearance.BorderSize = 1;
                    btn.FlatAppearance.BorderColor = Color.Black;
                    btn.Font = new Font("Arial", 5, FontStyle.Bold);
                    btn.TextAlign = ContentAlignment.MiddleCenter;
                    btn.ForeColor = Color.Black;

                    btn.Tag = territorio;
                    territorio.BotonAsociado = btn;


                    btn.Click += (sender, e) => {
                        Territorio terrClickeado = (Territorio)((Button)sender).Tag;

                        if (Juego.Instance != null)
                        {
                            switch (Juego.Instance.FaseActual)
                            {
                                case Juego.FaseTurno.ColocacionInicial:
                                    bool exito = Juego.Instance.ColocarTropaInicial(terrClickeado);
                                    if (exito)
                                    {
                                        terrClickeado.ActualizarVisualmente();
                                        ActualizarUI();
                                    }
                                    break;

                                case Juego.FaseTurno.Refuerzo:
                                    bool reforzado = Juego.Instance.ReforzarTerritorio(terrClickeado, 1);
                                    if (reforzado)
                                    {
                                        terrClickeado.ActualizarVisualmente();
                                        ActualizarUI();
                                    }
                                    break;

                                case Juego.FaseTurno.Ataque:
                                    ManejarAtaque(terrClickeado);
                                    break;

                                case Juego.FaseTurno.Planeacion:
                                    using (var dlg = new movertropas(terrClickeado))
                                    {
                                        if (dlg.ShowDialog() == DialogResult.OK)
                                        {
                                            foreach (var t in Juego.Instance.Territorios)
                                                t.ActualizarVisualmente();
                                            ActualizarUI();
                                        }
                                    }
                                    break;

                                default:
                                    break;
                            }
                        }
                    };

                    this.Controls.Add(btn);
                    btn.BringToFront();

                  
                    territorio.ActualizarVisualmente();
                }
            }
        }
        private void ManejarAtaque(Territorio territorioClickeado)
        {
            // Si es territorio propio, seleccionar para atacar
            if (Juego.Instance.TerritorioPerteneceAJugadorActual(territorioClickeado))
            {
                if (!territorioClickeado.PuedeAtacar())
                {
                    MessageBox.Show("Este territorio no tiene suficientes tropas para atacar (mínimo 2)");
                    return;
                }

                using (var formSeleccion = new SeleccionAtaqueForm(territorioClickeado))
                {
                    if (formSeleccion.ShowDialog() == DialogResult.OK)
                    {
                        var destino = formSeleccion.TerritorioDestinoSeleccionado;

                        if (Juego.Instance.IniciarAtaque(territorioClickeado, destino))
                        {
                            using (var formConfig = new ConfiguracionAtaqueForm(territorioClickeado, destino))
                            {
                                if (formConfig.ShowDialog() == DialogResult.OK)
                                {
                                    var resultado = Juego.Instance.ResolverAtaque(
                                        formConfig.TropasAtacante,
                                        formConfig.TropasDefensor);

                                    // Mostrar resultados
                                    using (var formResultado = new Tirada(resultado, territorioClickeado, destino))
                                    {
                                        formResultado.ShowDialog();
                                    }

                                    // Actualizar interfaz
                                    territorioClickeado.ActualizarVisualmente();
                                    destino.ActualizarVisualmente();
                                    ActualizarUI();
                                }
                            }
                        }
                    }
                }
            }
        }
       
        private void ActualizarUI()
        {
            if (Juego.Instance != null && Juego.Instance.JugadorActual != null)
            {
                lblJugadorActual.Text = $"Jugador: {Juego.Instance.JugadorActual.Nombre}";
                lblTropasDisponibles.Text = $"Tropas: {Juego.Instance.JugadorActual.TropasDisponibles}";
                lblFase.Text = $"Fase: {Juego.Instance.FaseActual}";

                // Mostrar/ocultar botón de ataque según la fase
               

                for (int i = 0; i < territorios.Count; i++)
                {
                    territorios.Obtener(i).ActualizarVisualmente();
                }

                bool enColocacionInicial = Juego.Instance.FaseActual == Juego.FaseTurno.ColocacionInicial;
                btnSiguienteFase.Enabled = !enColocacionInicial;
            }
        }

        private void BtnSiguienteFase_Click(object sender, EventArgs e)
        {
            Juego.Instance.AvanzarFase();
            ActualizarUI();
        }


        private void MapaFormulario_Load(object sender, EventArgs e)
        {
            ActualizarUI();
        }


    }
}