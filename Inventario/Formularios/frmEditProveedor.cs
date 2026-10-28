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
    public partial class frmEditProveedor : Form
    {
        private Proveedor proveedorActual;

        public frmEditProveedor()
        {
            InitializeComponent();
            StartPosition = FormStartPosition.CenterParent;
            Load += frmEditProveedor_Load;
        }

        public frmEditProveedor(Proveedor proveedor) : this()
        {
            proveedorActual = proveedor;
        }

        private void frmEditProveedor_Load(object sender, EventArgs e)
        {
            textBox1.ReadOnly = true;

            if (proveedorActual != null)
            {
                Text = "Modificar proveedor";

                textBox1.Text = proveedorActual.IdProveedor.ToString();
                textBox2.Text = proveedorActual.Nombre;
                textBox3.Text = proveedorActual.Ruc;
                textBox4.Text = proveedorActual.Telefono;
                textBox5.Text = proveedorActual.Direccion;
            }
            else
            {
                Text = "Nuevo proveedor";

                int nuevoId = TListaProveedor.Listar()
                    .Select(p => p.IdProveedor)
                    .DefaultIfEmpty(0)
                    .Max() + 1;

                textBox1.Text = nuevoId.ToString();
                textBox2.Clear();
                textBox3.Clear();
                textBox4.Clear();
                textBox5.Clear();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBox2.Text))
            {
                MessageBox.Show("Ingrese el nombre del proveedor.");
                textBox2.Focus();
                return;
            }

            if (string.IsNullOrWhiteSpace(textBox3.Text))
            {
                MessageBox.Show("Ingrese el RUC del proveedor.");
                textBox3.Focus();
                return;
            }

            try
            {
                if (proveedorActual == null)
                {
                    Proveedor nuevo = new Proveedor
                    {
                        IdProveedor = int.Parse(textBox1.Text),
                        Nombre = textBox2.Text.Trim(),
                        Ruc = textBox3.Text.Trim(),
                        Telefono = textBox4.Text.Trim(),
                        Direccion = textBox5.Text.Trim()
                    };

                    TListaProveedor.Agregar(nuevo);

                    MessageBox.Show("Proveedor registrado correctamente.");
                }
                else
                {
                    proveedorActual.Nombre = textBox2.Text.Trim();
                    proveedorActual.Ruc = textBox3.Text.Trim();
                    proveedorActual.Telefono = textBox4.Text.Trim();
                    proveedorActual.Direccion = textBox5.Text.Trim();

                    if (!TListaProveedor.Modificar(proveedorActual))
                    {
                        MessageBox.Show("No se pudo modificar el proveedor.");
                        return;
                    }

                    MessageBox.Show("Proveedor modificado correctamente.");
                }

                DialogResult = DialogResult.OK;
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al guardar: " + ex.Message);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}