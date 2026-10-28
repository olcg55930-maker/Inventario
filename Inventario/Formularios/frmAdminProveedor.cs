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
    public partial class frmAdminProveedor : Form
    {
        public frmAdminProveedor()
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

            Load += frmAdminProveedor_Load;
        }

        private void frmAdminProveedor_Load(object sender, EventArgs e)
        {
            MostrarProveedores();
        }

        private void MostrarProveedores()
        {
            dataGridView1.DataSource = null;
            dataGridView1.DataSource = TListaProveedor.Listar()
                .Select(p => new
                {
                    p.IdProveedor,
                    p.Nombre,
                    p.Ruc,
                    p.Telefono,
                    p.Direccion
                })
                .ToList();
        }

        // Ingresar
        private void button1_Click(object sender, EventArgs e)
        {
            using (frmEditProveedor frm = new frmEditProveedor())
            {
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    MostrarProveedores();
                }
            }
        }

        // Modificar
        private void button2_Click(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow == null)
            {
                MessageBox.Show("Seleccione un proveedor.");
                return;
            }

            int id = Convert.ToInt32(
                dataGridView1.CurrentRow.Cells["IdProveedor"].Value);

            Proveedor proveedor = TListaProveedor.Listar()
                .FirstOrDefault(p => p.IdProveedor == id);

            if (proveedor == null)
            {
                MessageBox.Show("No se encontró el proveedor.");
                return;
            }

            using (frmEditProveedor frm = new frmEditProveedor(proveedor))
            {
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    MostrarProveedores();
                }
            }
        }

        // Eliminar
        private void button3_Click(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow == null)
            {
                MessageBox.Show("Seleccione un proveedor.");
                return;
            }

            int id = Convert.ToInt32(
                dataGridView1.CurrentRow.Cells["IdProveedor"].Value);

            Proveedor proveedor = TListaProveedor.Listar()
                .FirstOrDefault(p => p.IdProveedor == id);

            if (proveedor == null)
            {
                MessageBox.Show("No se encontró el proveedor.");
                return;
            }

            DialogResult respuesta = MessageBox.Show(
                "¿Está seguro de eliminar al proveedor "
                + proveedor.Nombre + "?",
                "Confirmar eliminación",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (respuesta == DialogResult.Yes)
            {
                if (TListaProveedor.Eliminar(id))
                {
                    MessageBox.Show("Proveedor eliminado correctamente.");
                    MostrarProveedores();
                }
                else
                {
                    MessageBox.Show("No se pudo eliminar el proveedor.");
                }
            }
        }
    }
}