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
    public partial class frmAdminCategoria : Form
    {
        public frmAdminCategoria()
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

            Load += frmAdminCategoria_Load;
        }

        private void frmAdminCategoria_Load(object sender, EventArgs e)
        {
            MostrarCategorias();
        }

        private void MostrarCategorias()
        {
            dataGridView1.DataSource = null;
            dataGridView1.DataSource = TListaCategoria.Listar()
                .Select(c => new
                {
                    c.IdCategoria,
                    c.Nombre,
                    c.Descripcion,
                    Estado = c.Estado ? "Activo" : "Inactivo"
                })
                .ToList();
        }

        // Ingresar
        private void button1_Click(object sender, EventArgs e)
        {
            using (frmEditCategoria frm = new frmEditCategoria())
            {
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    MostrarCategorias();
                }
            }
        }

        // Modificar
        private void button2_Click(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow == null)
            {
                MessageBox.Show("Seleccione una categoría.");
                return;
            }

            int id = Convert.ToInt32(
                dataGridView1.CurrentRow.Cells["IdCategoria"].Value);

            Categoria categoria = TListaCategoria.Listar()
                .FirstOrDefault(c => c.IdCategoria == id);

            if (categoria == null)
            {
                MessageBox.Show("No se encontró la categoría.");
                return;
            }

            using (frmEditCategoria frm = new frmEditCategoria(categoria))
            {
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    MostrarCategorias();
                }
            }
        }

        // Eliminar
        private void button3_Click(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow == null)
            {
                MessageBox.Show("Seleccione una categoría.");
                return;
            }

            int id = Convert.ToInt32(
                dataGridView1.CurrentRow.Cells["IdCategoria"].Value);

            Categoria categoria = TListaCategoria.Listar()
                .FirstOrDefault(c => c.IdCategoria == id);

            if (categoria == null)
            {
                MessageBox.Show("No se encontró la categoría.");
                return;
            }

            DialogResult respuesta = MessageBox.Show(
                "¿Está seguro de eliminar la categoría "
                + categoria.Nombre + "?",
                "Confirmar eliminación",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (respuesta == DialogResult.Yes)
            {
                if (TListaCategoria.Eliminar(id))
                {
                    MessageBox.Show("Categoría eliminada correctamente.");
                    MostrarCategorias();
                }
                else
                {
                    MessageBox.Show("No se pudo eliminar la categoría.");
                }
            }
        }
    }
}