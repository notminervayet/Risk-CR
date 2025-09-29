using System;
using System.Drawing;
using System.Windows.Forms;

namespace Risk_CR.Formularios
{
    public partial class Tirada : Form
    {
        private Dado resultadoDado;
        private Territorio origen;
        private Territorio destino;

        public Tirada(Dado dado, Territorio origen, Territorio destino)
        {
            InitializeComponent();
            this.resultadoDado = dado;
            this.origen = origen;
            this.destino = destino;
            InicializarInterfaz();
        }

        private void InicializarInterfaz()
        {
            this.Text = "Resultado del Ataque";
            this.Size = new Size(500, 400);
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.StartPosition = FormStartPosition.CenterScreen;

            Panel panel = new Panel();
            panel.Dock = DockStyle.Fill;
            panel.BackColor = Color.LightGray;

            // Título
            Label lblTitulo = new Label();
            lblTitulo.Text = $"Ataque: {origen.Nombre} → {destino.Nombre}";
            lblTitulo.Font = new Font("Arial", 12, FontStyle.Bold);
            lblTitulo.Size = new Size(400, 30);
            lblTitulo.Location = new Point(50, 20);
            lblTitulo.TextAlign = ContentAlignment.MiddleCenter;

            // Dados atacante
            Label lblAtacante = new Label();
            lblAtacante.Text = "Dados Atacante:";
            lblAtacante.Size = new Size(150, 20);
            lblAtacante.Location = new Point(50, 70);

            string dadosAtacante = string.Join(", ", resultadoDado.AtacanteTiradas.ConvertirAArray());
            Label lblDadosAtacante = new Label();
            lblDadosAtacante.Text = dadosAtacante;
            lblDadosAtacante.Size = new Size(100, 20);
            lblDadosAtacante.Location = new Point(200, 70);
            lblDadosAtacante.Font = new Font("Arial", 10, FontStyle.Bold);

            // Dados defensor
            Label lblDefensor = new Label();
            lblDefensor.Text = "Dados Defensor:";
            lblDefensor.Size = new Size(150, 20);
            lblDefensor.Location = new Point(50, 100);

            string dadosDefensor = string.Join(", ", resultadoDado.DefensorTiradas.ConvertirAArray());
            Label lblDadosDefensor = new Label();
            lblDadosDefensor.Text = dadosDefensor;
            lblDadosDefensor.Size = new Size(100, 20);
            lblDadosDefensor.Location = new Point(200, 100);
            lblDadosDefensor.Font = new Font("Arial", 10, FontStyle.Bold);

            // Resultados
            Label lblPerdidasAtacante = new Label();
            lblPerdidasAtacante.Text = $"Ejércitos perdidos por atacante: {resultadoDado.EjercitosPerdidosAtacante}";
            lblPerdidasAtacante.Size = new Size(250, 20);
            lblPerdidasAtacante.Location = new Point(50, 140);

            Label lblPerdidasDefensor = new Label();
            lblPerdidasDefensor.Text = $"Ejércitos perdidos por defensor: {resultadoDado.EjercitosPerdidosDefensor}";
            lblPerdidasDefensor.Size = new Size(250, 20);
            lblPerdidasDefensor.Location = new Point(50, 170);

            // Estado final
            Label lblEstadoOrigen = new Label();
            lblEstadoOrigen.Text = $"{origen.Nombre}: {origen.Tropas} tropas restantes";
            lblEstadoOrigen.Size = new Size(250, 20);
            lblEstadoOrigen.Location = new Point(50, 210);

            Label lblEstadoDestino = new Label();
            lblEstadoDestino.Text = $"{destino.Nombre}: {destino.Tropas} tropas restantes";
            lblEstadoDestino.Size = new Size(250, 20);
            lblEstadoDestino.Location = new Point(50, 240);

            // Mensaje de conquista
            if (destino.Tropas == 0)
            {
                Label lblConquista = new Label();
                lblConquista.Text = "¡TERRITORIO CONQUISTADO!";
                lblConquista.ForeColor = Color.Red;
                lblConquista.Font = new Font("Arial", 12, FontStyle.Bold);
                lblConquista.Size = new Size(300, 30);
                lblConquista.Location = new Point(100, 280);
                lblConquista.TextAlign = ContentAlignment.MiddleCenter;
                panel.Controls.Add(lblConquista);
            }

            Button btnCerrar = new Button();
            btnCerrar.Text = "Cerrar";
            btnCerrar.Size = new Size(100, 30);
            btnCerrar.Location = new Point(200, 320);
            btnCerrar.Click += (s, e) => this.Close();

            panel.Controls.AddRange(new Control[] {
                lblTitulo,
                lblAtacante, lblDadosAtacante,
                lblDefensor, lblDadosDefensor,
                lblPerdidasAtacante, lblPerdidasDefensor,
                lblEstadoOrigen, lblEstadoDestino,
                btnCerrar
            });

            this.Controls.Add(panel);
        }

        private void Tirada_Load(object sender, EventArgs e)
        {
        }
    }
}