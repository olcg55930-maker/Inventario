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
    public partial class frmAdminLocal : Form
    {
        public frmAdminLocal()
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

            Load += frmAdminLocal_Load;
        }

        private void frmAdminLocal_Load(object sender, EventArgs e)
        {
            MostrarLocales();
        }

        private void MostrarLocales()
        {
            dataGridView1.DataSource = null;
            dataGridView1.DataSource = TListaLocal.Listar()
                .Select(l => new
                {
                    l.IdLocal,
                    l.Nombre,
                    l.Direccion,
                    l.Ciudad,
                    l.Telefono,
                    l.Responsable,
                    l.TipoLocal
                })
                .ToList();
        }

        // Ingresar
        private void button1_Click(object sender, EventArgs e)
        {
            using (frmEditLocal frm = new frmEditLocal())
            {
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    MostrarLocales();
                }
            }
        }

        // Modificar
        private void button2_Click(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow == null)
            {
                MessageBox.Show("Seleccione un local.");
                return;
            }

            int id = Convert.ToInt32(
                dataGridView1.CurrentRow.Cells["IdLocal"].Value);

            Local local = TListaLocal.Listar()
                .FirstOrDefault(l => l.IdLocal == id);

            if (local == null)
            {
                MessageBox.Show("No se encontró el local.");
                return;
            }

            using (frmEditLocal frm = new frmEditLocal(local))
            {
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    MostrarLocales();
                }
            }
        }

        // Eliminar
        private void button3_Click(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow == null)
            {
                MessageBox.Show("Seleccione un local.");
                return;
            }

            int id = Convert.ToInt32(
                dataGridView1.CurrentRow.Cells["IdLocal"].Value);

            Local local = TListaLocal.Listar()
                .FirstOrDefault(l => l.IdLocal == id);

            if (local == null)
            {
                MessageBox.Show("No se encontró el local.");
                return;
            }

            DialogResult respuesta = MessageBox.Show(
                "¿Está seguro de eliminar el local " + local.Nombre + "?",
                "Confirmar eliminación",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (respuesta == DialogResult.Yes)
            {
                if (TListaLocal.Eliminar(id))
                {
                    MessageBox.Show("Local eliminado correctamente.");
                    MostrarLocales();
                }
                else
                {
                    MessageBox.Show("No se pudo eliminar el local.");
                }
            }
        }
    }
}