using Inventario.Controlador;
using Inventario.Entidades;
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
    public partial class frmEditProducto : Form
    {
        private Producto productoActual;
        public frmEditProducto() : this(null)
        {
        }
        public frmEditProducto(Producto producto)
        {
            InitializeComponent();
            productoActual = producto;
            StartPosition = FormStartPosition.CenterParent;
        }

        private void frmEditProducto_Load(object sender, EventArgs e)
        {
            comboBox1.DataSource = TListaCategoria.Listar().ToList();
            comboBox1.DisplayMember = "Nombre";
            comboBox1.ValueMember = "IdCategoria";

            comboBox2.DataSource = TListaProveedor.Listar().ToList();
            comboBox2.DisplayMember = "Nombre";
            comboBox2.ValueMember = "IdProveedor";

            comboBox3.DataSource = TListaLocal.Listar().ToList();
            comboBox3.DisplayMember = "Nombre";
            comboBox3.ValueMember = "IdLocal";

            comboBox4.DataSource = TListaInventario.Listar().ToList();
            comboBox4.DisplayMember = "Nombre";
            comboBox4.ValueMember = "IdInventario";

            if (productoActual == null)
            {
                MessageBox.Show("No se recibió ningún producto.");
                DialogResult = DialogResult.Cancel;
                Close();
                return;
            }

            textBox1.Text = productoActual.Id.ToString();
            textBox2.Text = productoActual.Nombre;
            textBox3.Text = productoActual.Precio.ToString();
            textBox4.Text = productoActual.Stock.ToString();
            textBox5.Text = productoActual.Descripcion;

            comboBox1.SelectedValue = productoActual.IdCategoria;
            comboBox2.SelectedValue = productoActual.IdProveedor;
            comboBox3.SelectedValue = productoActual.IdLocal;
            comboBox4.SelectedValue = productoActual.IdInventario;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBox2.Text) ||
                string.IsNullOrWhiteSpace(textBox3.Text) ||
                string.IsNullOrWhiteSpace(textBox4.Text))
            {
                MessageBox.Show("Complete todos los campos obligatorios.");
                return;
            }

            if (!decimal.TryParse(textBox3.Text, out decimal precio) ||
                precio < 0)
            {
                MessageBox.Show("Ingrese un precio válido y no negativo.");
                textBox3.Focus();
                return;
            }

            if (!int.TryParse(textBox4.Text, out int stock) || stock < 0)
            {
                MessageBox.Show("Ingrese un stock válido y no negativo.");
                textBox4.Focus();
                return;
            }

            if (comboBox1.SelectedValue == null ||
                comboBox2.SelectedValue == null ||
                comboBox3.SelectedValue == null ||
                comboBox4.SelectedValue == null)
            {
                MessageBox.Show("Seleccione categoría, proveedor, local e inventario.");
                return;
            }

            productoActual.Nombre = textBox2.Text.Trim();
            productoActual.Precio = precio;
            productoActual.Stock = stock;
            productoActual.IdCategoria = Convert.ToInt32(comboBox1.SelectedValue);
            productoActual.IdProveedor = Convert.ToInt32(comboBox2.SelectedValue);
            productoActual.IdLocal = Convert.ToInt32(comboBox3.SelectedValue);
            productoActual.IdInventario = Convert.ToInt32(comboBox4.SelectedValue);
            productoActual.Descripcion = textBox5.Text.Trim();

            if (TListaProducto.Modificar(productoActual))
            {
                MessageBox.Show("Producto modificado correctamente.");
                DialogResult = DialogResult.OK;
                Close();
            }
            else
            {
                MessageBox.Show("No se pudo modificar el producto.");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}