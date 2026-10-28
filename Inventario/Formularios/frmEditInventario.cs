using Inventario.Controlador;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Inventario.Formularios
{
    public partial class frmEditInventario : Form
    {
        private global::Inventario.Entidades.Inventario inventarioActual;

        // CONSTRUCTOR PARA INGRESAR
        public frmEditInventario()
        {
            InitializeComponent();

            StartPosition = FormStartPosition.CenterParent;
            Load += frmEditInventario_Load;
        }

        // CONSTRUCTOR PARA MODIFICAR
        public frmEditInventario(
            global::Inventario.Entidades.Inventario inventario) : this()
        {
            inventarioActual = inventario;
        }

        // CARGAR LOS DATOS EN LOS CONTROLES
        private void frmEditInventario_Load(object sender, EventArgs e)
        {
            textBox1.ReadOnly = true;

            if (inventarioActual != null)
            {
                Text = "Modificar inventario";

                textBox1.Text = inventarioActual.IdInventario.ToString();
                textBox2.Text = inventarioActual.Nombre;
                textBox3.Text = inventarioActual.CodigoInventario;
                dateTimePicker1.Value = inventarioActual.FechaActualizacion;
                textBox4.Text = inventarioActual.StockMinimo.ToString();
                textBox5.Text = inventarioActual.StockMaximo.ToString();
                checkBox1.Checked = inventarioActual.Estado;
            }
            else
            {
                Text = "Nuevo inventario";

                int nuevoId = TListaInventario.Listar()
                    .Select(i => i.IdInventario)
                    .DefaultIfEmpty(0)
                    .Max() + 1;

                textBox1.Text = nuevoId.ToString();
                textBox2.Clear();
                textBox3.Clear();
                dateTimePicker1.Value = DateTime.Now;
                textBox4.Text = "0";
                textBox5.Text = "100";
                checkBox1.Checked = true;
            }
        }

        // BOTÓN GUARDAR
        private void button1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBox2.Text) ||
                string.IsNullOrWhiteSpace(textBox3.Text))
            {
                MessageBox.Show("Ingrese el nombre y código del inventario.");
                return;
            }

            if (!int.TryParse(textBox4.Text, out int minimo) || minimo < 0)
            {
                MessageBox.Show("Ingrese un stock mínimo válido.");
                textBox4.Focus();
                return;
            }

            if (!int.TryParse(textBox5.Text, out int maximo) ||
                maximo < minimo)
            {
                MessageBox.Show(
                    "El stock máximo debe ser mayor o igual al mínimo.");
                textBox5.Focus();
                return;
            }

            try
            {
                if (inventarioActual == null)
                {
                    // REGISTRAR INVENTARIO NUEVO
                    var nuevo = new global::Inventario.Entidades.Inventario
                    {
                        IdInventario = int.Parse(textBox1.Text),
                        Nombre = textBox2.Text.Trim(),
                        CodigoInventario = textBox3.Text.Trim(),
                        FechaActualizacion = dateTimePicker1.Value,
                        StockMinimo = minimo,
                        StockMaximo = maximo,
                        Estado = checkBox1.Checked
                    };

                    TListaInventario.Agregar(nuevo);

                    MessageBox.Show("Inventario registrado correctamente.");
                }
                else
                {
                    // MODIFICAR INVENTARIO EXISTENTE
                    inventarioActual.Nombre = textBox2.Text.Trim();
                    inventarioActual.CodigoInventario = textBox3.Text.Trim();
                    inventarioActual.FechaActualizacion = dateTimePicker1.Value;
                    inventarioActual.StockMinimo = minimo;
                    inventarioActual.StockMaximo = maximo;
                    inventarioActual.Estado = checkBox1.Checked;

                    if (!TListaInventario.Modificar(inventarioActual))
                    {
                        MessageBox.Show("No se pudo modificar el inventario.");
                        return;
                    }

                    MessageBox.Show("Inventario modificado correctamente.");
                }

                DialogResult = DialogResult.OK;
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al guardar: " + ex.Message);
            }
        }

        // BOTÓN CANCELAR
        private void button2_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}