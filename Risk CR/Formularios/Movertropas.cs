using System;
using System.Drawing;
using System.Windows.Forms;

namespace Risk_CR.Formularios
{
    public partial class movertropas : Form
    {
        private Territorio territorioOrigen;
        private ListBox listDestinos;
        private NumericUpDown nudCantidad;

        public movertropas(Territorio origen)
        {
            territorioOrigen = origen;
            Text = "Mover Tropas - Planeación";
            Size = new Size(360, 350);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            StartPosition = FormStartPosition.CenterParent;
            MaximizeBox = false;
            MinimizeBox = false;
            BackColor = Color.LightGray;
            InicializarControles();
        }

        private void InicializarControles()
        {
            // Lista de territorios destino
            listDestinos = new ListBox
            {
                Location = new Point(20, 20),
                Size = new Size(300, 120)
            };
            // Llenar la lista con territorios adyacentes ocupados por el jugador actual
            for (int i = 0; i < territorioOrigen.TerritoriosAdyacentes.Count; i++)
            {
                var v = territorioOrigen.TerritoriosAdyacentes.Obtener(i);
                if (v.Ocupante == Juego.Instance.JugadorActual)
                    listDestinos.Items.Add(new Item(v, $"{v.Nombre} ({v.Tropas} tropas)"));
            }

            nudCantidad = new NumericUpDown
            {
                //cuantos mover
                Location = new Point(20, 160),
                Minimum = 1,
                Maximum = Math.Max(1, territorioOrigen.Tropas - 1),
                Value = 1
            };

            var lblInfo = new Label
            {
                //info del territorio origen
                Text = $"Origen: {territorioOrigen.Nombre} - Tropas: {territorioOrigen.Tropas}",
                Location = new Point(20, 200),
                AutoSize = true
            };

            var btnMover = new Button
            {
                Text = "Mover",
                Size = new Size(80, 30),
                Location = new Point(20, 240)
            };
            btnMover.Click += BtnMover_Click;

            var btnCancelar = new Button
            {
                Text = "Cancelar",
                Size = new Size(80, 30),
                Location = new Point(120, 240),
                DialogResult = DialogResult.Cancel
            };

            // Agregar controles al formulario
            Controls.Add(listDestinos);
            Controls.Add(nudCantidad);
            Controls.Add(lblInfo);
            Controls.Add(btnMover);
            Controls.Add(btnCancelar);

            AcceptButton = btnMover;
            CancelButton = btnCancelar;
        }

        private void BtnMover_Click(object sender, EventArgs e)
        {
            if (listDestinos.SelectedItem == null)
            {
                MessageBox.Show("Selecciona un territorio destino.", "Error",
                                MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (territorioOrigen.Tropas < 2)
            {
                MessageBox.Show("Necesitas al menos 2 tropas para mover.", "Error",
                                MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Realizar el movimiento
            var destino = ((Item)listDestinos.SelectedItem).Territorio;
            int cantidad = (int)nudCantidad.Value;

            bool ok = Juego.Instance.MoverTropasPlaneacion(
                territorioOrigen, destino, cantidad
            );
            if (!ok)
            {
                MessageBox.Show(
                  "Movimiento inválido.\n- Deja al menos 1 tropa en el origen.\n- Verifica ruta de adyacencia.",
                  "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning
                );
                return;
            }

            DialogResult = DialogResult.OK;
        }

        private class Item
        {
            //clase para mostrar territorios en la lista
            public Territorio Territorio { get; }
            public string Texto { get; }
            public Item(Territorio t, string texto)
            {
                Territorio = t;
                Texto = texto;
            }
            public override string ToString() => Texto;
        }

        private void movertropas_Load(object sender, EventArgs e)
        {

        }
    }
}
