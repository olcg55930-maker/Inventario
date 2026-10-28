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

        // Constructor para registrar un producto nuevo
        public frmEditProducto() : this(null)
        {
        }

        // Constructor para modificar un producto existente
        public frmEditProducto(Producto producto)
        {
            InitializeComponent();
            productoActual = producto;
            StartPosition = FormStartPosition.CenterParent;
        }

        private void frmEditProducto_Load(object sender, EventArgs e)
        {
            // Cargar categorías
            comboBox1.DataSource = TListaCategoria.Listar().ToList();
            comboBox1.DisplayMember = "Nombre";
            comboBox1.ValueMember = "IdCategoria";
            comboBox1.DropDownStyle = ComboBoxStyle.DropDownList;

            // Cargar proveedores
            comboBox2.DataSource = TListaProveedor.Listar().ToList();
            comboBox2.DisplayMember = "Nombre";
            comboBox2.ValueMember = "IdProveedor";
            comboBox2.DropDownStyle = ComboBoxStyle.DropDownList;

            // Cargar locales
            comboBox3.DataSource = TListaLocal.Listar().ToList();
            comboBox3.DisplayMember = "Nombre";
            comboBox3.ValueMember = "IdLocal";
            comboBox3.DropDownStyle = ComboBoxStyle.DropDownList;

            // Cargar inventarios
            comboBox4.DataSource = TListaInventario.Listar().ToList();
            comboBox4.DisplayMember = "Nombre";
            comboBox4.ValueMember = "IdInventario";
            comboBox4.DropDownStyle = ComboBoxStyle.DropDownList;

            // Si se recibió un producto, cargar sus datos
            if (productoActual != null)
            {
                Text = "Modificar producto";

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
            else
            {
                // Generar el siguiente ID disponible
                int nuevoId = TListaProducto.Listar()
                    .Select(p => p.Id)
                    .DefaultIfEmpty(0)
                    .Max() + 1;

                Text = "Nuevo producto";
                textBox1.Text = nuevoId.ToString();
                textBox2.Clear();
                textBox3.Clear();
                textBox4.Text = "0";
                textBox5.Clear();
            }

            textBox1.ReadOnly = true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // Validar campos obligatorios
            if (string.IsNullOrWhiteSpace(textBox2.Text) ||
                string.IsNullOrWhiteSpace(textBox3.Text) ||
                string.IsNullOrWhiteSpace(textBox4.Text))
            {
                MessageBox.Show("Complete los campos obligatorios.");
                return;
            }

            // Validar precio
            if (!decimal.TryParse(textBox3.Text, out decimal precio) ||
                precio < 0)
            {
                MessageBox.Show("Ingrese un precio válido y no negativo.");
                textBox3.Focus();
                return;
            }

            // Validar stock
            if (!int.TryParse(textBox4.Text, out int stock) || stock < 0)
            {
                MessageBox.Show("Ingrese un stock válido y no negativo.");
                textBox4.Focus();
                return;
            }

            // Validar selecciones
            if (comboBox1.SelectedValue == null ||
                comboBox2.SelectedValue == null ||
                comboBox3.SelectedValue == null ||
                comboBox4.SelectedValue == null)
            {
                MessageBox.Show(
                    "Seleccione una categoría, proveedor, local e inventario.");
                return;
            }

            try
            {
                int idCategoria = Convert.ToInt32(comboBox1.SelectedValue);
                int idProveedor = Convert.ToInt32(comboBox2.SelectedValue);
                int idLocal = Convert.ToInt32(comboBox3.SelectedValue);
                int idInventario = Convert.ToInt32(comboBox4.SelectedValue);

                if (productoActual == null)
                {
                    // Registrar producto nuevo
                    int nuevoId = TListaProducto.Listar()
                        .Select(p => p.Id)
                        .DefaultIfEmpty(0)
                        .Max() + 1;

                    Producto nuevoProducto = new Producto
                    {
                        Id = nuevoId,
                        Nombre = textBox2.Text.Trim(),
                        Precio = precio,
                        Stock = stock,
                        IdCategoria = idCategoria,
                        IdProveedor = idProveedor,
                        IdLocal = idLocal,
                        IdInventario = idInventario,
                        Descripcion = textBox5.Text.Trim()
                    };

                    TListaProducto.Agregar(nuevoProducto);

                    MessageBox.Show("Producto registrado correctamente.");
                }
                else
                {
                    // Modificar producto existente
                    productoActual.Nombre = textBox2.Text.Trim();
                    productoActual.Precio = precio;
                    productoActual.Stock = stock;
                    productoActual.IdCategoria = idCategoria;
                    productoActual.IdProveedor = idProveedor;
                    productoActual.IdLocal = idLocal;
                    productoActual.IdInventario = idInventario;
                    productoActual.Descripcion = textBox5.Text.Trim();

                    if (!TListaProducto.Modificar(productoActual))
                    {
                        MessageBox.Show("No se pudo modificar el producto.");
                        return;
                    }

                    MessageBox.Show("Producto modificado correctamente.");
                }

                DialogResult = DialogResult.OK;
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Error al guardar el producto: " + ex.Message,
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}