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
    public partial class frmAdminProducto : Form
    {
        public frmAdminProducto()
        {
            InitializeComponent();

            StartPosition = FormStartPosition.CenterParent;
            dataGridView1.AutoSizeColumnsMode =
                DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView1.ReadOnly = true;
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.SelectionMode =
                DataGridViewSelectionMode.FullRowSelect;
            dataGridView1.MultiSelect = false;

            MostrarProductos();
        }


        private void MostrarProductos()
        {
            dataGridView1.DataSource = null;

            var lista = TListaProducto.Listar()
                .Select(p => new
                {
                    p.Id,
                    p.Nombre,
                    p.Descripcion,
                    p.Precio,
                    p.Stock,

                    Categoria = TListaCategoria.Listar()
                        .FirstOrDefault(c => c.IdCategoria == p.IdCategoria)?.Nombre,

                    Proveedor = TListaProveedor.Listar()
                        .FirstOrDefault(pr => pr.IdProveedor == p.IdProveedor)?.Nombre,

                    Local = TListaLocal.Listar()
                        .FirstOrDefault(l => l.IdLocal == p.IdLocal)?.Nombre,

                    Inventario = TListaInventario.Listar()
                        .FirstOrDefault(i => i.IdInventario == p.IdInventario)?.Nombre
                })
                .ToList();

            dataGridView1.DataSource = lista;
        }


        // BOTÓN 1: INGRESAR
        private void button1_Click(object sender, EventArgs e)
        {
            using (frmEditProducto frm = new frmEditProducto())
            {
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    MostrarProductos();
                }
            }
        }

        // BOTÓN 2: MODIFICAR
        private void button2_Click(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow == null)
            {
                MessageBox.Show("Seleccione un producto.");
                return;
            }

            Producto producto =
                dataGridView1.CurrentRow.DataBoundItem as Producto;

            if (producto == null)
                return;

            using (frmEditProducto frm = new frmEditProducto(producto))
            {
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    MostrarProductos();
                }
            }
        }

        // BOTÓN 3: ELIMINAR
        private void button3_Click(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow == null)
            {
                MessageBox.Show("Seleccione un producto.");
                return;
            }

            Producto producto =
                dataGridView1.CurrentRow.DataBoundItem as Producto;

            if (producto == null)
                return;

            DialogResult respuesta = MessageBox.Show(
                "¿Desea eliminar " + producto.Nombre + "?",
                "Confirmar eliminación",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question
            );

            if (respuesta == DialogResult.Yes)
            {
                if (TListaProducto.Eliminar(producto.Id))
                {
                    MessageBox.Show("Producto eliminado correctamente.");
                    MostrarProductos();
                }
                else
                {
                    MessageBox.Show("No se pudo eliminar el producto.");
                }
            }
        }

        private void frmAdminProducto_Load(object sender, EventArgs e)
        {
            MostrarProductos();
        }
    }
}