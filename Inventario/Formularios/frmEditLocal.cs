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
    public partial class frmEditLocal : Form
    {
        private Local localActual;

        public frmEditLocal()
        {
            InitializeComponent();

            StartPosition = FormStartPosition.CenterParent;
            Load += frmEditLocal_Load;
        }

        public frmEditLocal(Local local) : this()
        {
            localActual = local;
        }

        private void frmEditLocal_Load(object sender, EventArgs e)
        {
            textBox1.ReadOnly = true;

            comboBox1.Items.Clear();
            comboBox1.Items.Add("Principal");
            comboBox1.Items.Add("Sucursal");
            comboBox1.Items.Add("Bodega");
            comboBox1.DropDownStyle = ComboBoxStyle.DropDownList;

            if (localActual != null)
            {
                Text = "Modificar local";

                textBox1.Text = localActual.IdLocal.ToString();
                textBox2.Text = localActual.Nombre;
                textBox3.Text = localActual.Direccion;
                textBox4.Text = localActual.Ciudad;
                textBox5.Text = localActual.Telefono;
                textBox6.Text = localActual.Responsable;
                comboBox1.SelectedItem = localActual.TipoLocal;
            }
            else
            {
                Text = "Nuevo local";

                int nuevoId = TListaLocal.Listar()
                    .Select(l => l.IdLocal)
                    .DefaultIfEmpty(0)
                    .Max() + 1;

                textBox1.Text = nuevoId.ToString();
                textBox2.Clear();
                textBox3.Clear();
                textBox4.Clear();
                textBox5.Clear();
                textBox6.Clear();
                comboBox1.SelectedIndex = 0;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBox2.Text))
            {
                MessageBox.Show("Ingrese el nombre del local.");
                textBox2.Focus();
                return;
            }

            if (comboBox1.SelectedIndex == -1)
            {
                MessageBox.Show("Seleccione el tipo de local.");
                comboBox1.Focus();
                return;
            }

            try
            {
                if (localActual == null)
                {
                    Local nuevo = new Local
                    {
                        IdLocal = int.Parse(textBox1.Text),
                        Nombre = textBox2.Text.Trim(),
                        Direccion = textBox3.Text.Trim(),
                        Ciudad = textBox4.Text.Trim(),
                        Telefono = textBox5.Text.Trim(),
                        Responsable = textBox6.Text.Trim(),
                        TipoLocal = comboBox1.SelectedItem.ToString()
                    };

                    TListaLocal.Agregar(nuevo);

                    MessageBox.Show("Local registrado correctamente.");
                }
                else
                {
                    localActual.Nombre = textBox2.Text.Trim();
                    localActual.Direccion = textBox3.Text.Trim();
                    localActual.Ciudad = textBox4.Text.Trim();
                    localActual.Telefono = textBox5.Text.Trim();
                    localActual.Responsable = textBox6.Text.Trim();
                    localActual.TipoLocal = comboBox1.SelectedItem.ToString();

                    if (!TListaLocal.Modificar(localActual))
                    {
                        MessageBox.Show("No se pudo modificar el local.");
                        return;
                    }

                    MessageBox.Show("Local modificado correctamente.");
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
