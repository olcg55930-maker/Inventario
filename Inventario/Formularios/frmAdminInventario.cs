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
    public partial class frmAdminInventario : Form
    {
        public frmAdminInventario()
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

            MostrarInventarios();
        }

        private void MostrarInventarios()
        {
            dataGridView1.DataSource = null;
            dataGridView1.DataSource = TListaInventario.Listar();
            dataGridView1.Refresh();
        }

        // INGRESAR
        private void button1_Click(object sender, EventArgs e)
        {
            using (frmEditInventario frm = new frmEditInventario())
            {
                if (frm.ShowDialog() == DialogResult.OK)
                    MostrarInventarios();
            }
        }

        // MODIFICAR

        private void button2_Click(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow == null)
            {
                MessageBox.Show("Seleccione un inventario.");
                return;
            }

            global::Inventario.Entidades.Inventario inventario =
                dataGridView1.CurrentRow.DataBoundItem
                as global::Inventario.Entidades.Inventario;

            if (inventario == null)
            {
                MessageBox.Show("No se pudo obtener el inventario seleccionado.");
                return;
            }

            using (frmEditInventario frm = new frmEditInventario(inventario))
            {
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    MostrarInventarios();
                }
            }
        }


        // ELIMINAR
        private void button3_Click(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow == null)
            {
                MessageBox.Show("Seleccione un inventario.");
                return;
            }

            var inventario = dataGridView1.CurrentRow.DataBoundItem
                as global::Inventario.Entidades.Inventario;

            if (inventario == null)
                return;

            DialogResult respuesta = MessageBox.Show(
                "¿Desea eliminar el inventario " + inventario.Nombre + "?",
                "Confirmar eliminación",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (respuesta == DialogResult.Yes)
            {
                if (TListaInventario.Eliminar(inventario.IdInventario))
                {
                    MessageBox.Show("Inventario eliminado correctamente.");
                    MostrarInventarios();
                }
                else
                {
                    MessageBox.Show("No se pudo eliminar el inventario.");
                }
            }
        }
    }
}