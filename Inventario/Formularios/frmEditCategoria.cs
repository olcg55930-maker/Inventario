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
    public partial class frmEditCategoria : Form
    {
        private Categoria categoriaActual;

        public frmEditCategoria()
        {
            InitializeComponent();
            StartPosition = FormStartPosition.CenterParent;
            Load += frmEditCategoria_Load;
        }

        public frmEditCategoria(Categoria categoria) : this()
        {
            categoriaActual = categoria;
        }

        private void frmEditCategoria_Load(object sender, EventArgs e)
        {
            textBox1.ReadOnly = true;

            if (categoriaActual != null)
            {
                Text = "Modificar categoría";

                textBox1.Text = categoriaActual.IdCategoria.ToString();
                textBox2.Text = categoriaActual.Nombre;
                textBox3.Text = categoriaActual.Descripcion;
                checkBox1.Checked = categoriaActual.Estado;
            }
            else
            {
                Text = "Nueva categoría";

                int nuevoId = TListaCategoria.Listar()
                    .Select(c => c.IdCategoria)
                    .DefaultIfEmpty(0)
                    .Max() + 1;

                textBox1.Text = nuevoId.ToString();
                textBox2.Clear();
                textBox3.Clear();
                checkBox1.Checked = true;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBox2.Text))
            {
                MessageBox.Show("Ingrese el nombre de la categoría.");
                textBox2.Focus();
                return;
            }

            try
            {
                if (categoriaActual == null)
                {
                    Categoria nueva = new Categoria
                    {
                        IdCategoria = int.Parse(textBox1.Text),
                        Nombre = textBox2.Text.Trim(),
                        Descripcion = textBox3.Text.Trim(),
                        Estado = checkBox1.Checked
                    };

                    TListaCategoria.Agregar(nueva);
                    MessageBox.Show("Categoría registrada correctamente.");
                }
                else
                {
                    categoriaActual.Nombre = textBox2.Text.Trim();
                    categoriaActual.Descripcion = textBox3.Text.Trim();
                    categoriaActual.Estado = checkBox1.Checked;

                    if (!TListaCategoria.Modificar(categoriaActual))
                    {
                        MessageBox.Show("No se pudo modificar la categoría.");
                        return;
                    }

                    MessageBox.Show("Categoría modificada correctamente.");
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