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
    public partial class frmAdmin : Form
    {
        public frmAdmin()
        {
            InitializeComponent();

            CargarOpcionesComboBox();

            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView1.ReadOnly = true;
            dataGridView1.AllowUserToAddRows = false;

            CargarDatosIniciales();
            MostrarProductos();
        }
        private void CargarOpcionesComboBox()
        {
            comboBox1.Items.Clear();
            comboBox1.Items.Add("Productos");
            comboBox1.Items.Add("Categorías");
            comboBox1.Items.Add("Proveedores");
            comboBox1.Items.Add("Locales");
            comboBox1.Items.Add("Inventarios");

            comboBox1.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBox1.SelectedIndex = 0;
        }
        private void CargarDatosIniciales()
        {
            // Evitar duplicar los datos al abrir nuevamente el formulario.
            if (TListaProducto.Listar().Count > 0)
                return;

            // CATEGORIAS: 4 registros
            TListaCategoria.Agregar(new Categoria { IdCategoria = 1, Nombre = "Computadoras", Descripcion = "Equipos de escritorio y portátiles", Estado = true });
            TListaCategoria.Agregar(new Categoria { IdCategoria = 2, Nombre = "Perifericos", Descripcion = "Teclados, ratones y accesorios", Estado = true });
            TListaCategoria.Agregar(new Categoria { IdCategoria = 3, Nombre = "Componentes", Descripcion = "Procesadores y memorias", Estado = true });
            TListaCategoria.Agregar(new Categoria { IdCategoria = 4, Nombre = "Almacenamiento", Descripcion = "Discos SSD y HDD", Estado = true });

            // PROVEEDORES: 5 registros
            TListaProveedor.Agregar(new Proveedor { IdProveedor = 1, Nombre = "Tecno Ecuador", Ruc = "0791234567001", Telefono = "0991234567", Direccion = "Machala" });
            TListaProveedor.Agregar(new Proveedor { IdProveedor = 2, Nombre = "CompuMundo", Ruc = "0791234567002", Telefono = "0981234567", Direccion = "Pasaje" });
            TListaProveedor.Agregar(new Proveedor { IdProveedor = 3, Nombre = "PC Soluciones", Ruc = "0791234567003", Telefono = "0971234567", Direccion = "Cuenca" });
            TListaProveedor.Agregar(new Proveedor { IdProveedor = 4, Nombre = "Digital Store", Ruc = "0791234567004", Telefono = "0961234567", Direccion = "Loja" });
            TListaProveedor.Agregar(new Proveedor { IdProveedor = 5, Nombre = "Mega Hardware", Ruc = "0791234567005", Telefono = "0951234567", Direccion = "Guayaquil" });

            // LOCALES: 5 registros
            TListaLocal.Agregar(new Local { IdLocal = 1, Nombre = "Local Principal", Direccion = "Av. Central", Ciudad = "Machala", Telefono = "072123456", Responsable = "Juan Perez", TipoLocal = "Principal" });
            TListaLocal.Agregar(new Local { IdLocal = 2, Nombre = "Sucursal Norte", Direccion = "Calle Norte", Ciudad = "Pasaje", Telefono = "072234567", Responsable = "Ana Lopez", TipoLocal = "Sucursal" });
            TListaLocal.Agregar(new Local { IdLocal = 3, Nombre = "Sucursal Sur", Direccion = "Av. Sur", Ciudad = "Cuenca", Telefono = "072345678", Responsable = "Luis Torres", TipoLocal = "Sucursal" });
            TListaLocal.Agregar(new Local { IdLocal = 4, Nombre = "Bodega Central", Direccion = "Calle Bodega", Ciudad = "Loja", Telefono = "072456789", Responsable = "Maria Castro", TipoLocal = "Bodega" });
            TListaLocal.Agregar(new Local { IdLocal = 5, Nombre = "Local Oeste", Direccion = "Av. Oeste", Ciudad = "Guayaquil", Telefono = "072567890", Responsable = "Pedro Ruiz", TipoLocal = "Sucursal" });

            TListaInventario.Agregar(new global::Inventario.Entidades.Inventario { IdInventario = 2, Nombre = "Inventario Norte", CodigoInventario = "INV002", FechaActualizacion = DateTime.Now, StockMinimo = 5, StockMaximo = 800, Estado = true });
            TListaInventario.Agregar(new global::Inventario.Entidades.Inventario { IdInventario = 3, Nombre = "Inventario Sur", CodigoInventario = "INV003", FechaActualizacion = DateTime.Now, StockMinimo = 3, StockMaximo = 500, Estado = true });
            TListaInventario.Agregar(new global::Inventario.Entidades.Inventario { IdInventario = 4, Nombre = "Inventario Bodega", CodigoInventario = "INV004", FechaActualizacion = DateTime.Now, StockMinimo = 10, StockMaximo = 2000, Estado = true });
            TListaInventario.Agregar(new global::Inventario.Entidades.Inventario { IdInventario = 5, Nombre = "Inventario Oeste", CodigoInventario = "INV005", FechaActualizacion = DateTime.Now, StockMinimo = 5, StockMaximo = 600, Estado = true });

            TListaInventario.Agregar(new global::Inventario.Entidades.Inventario { IdInventario = 1, Nombre = "Inventario Principal", CodigoInventario = "INV001", FechaActualizacion = DateTime.Now, StockMinimo = 5, StockMaximo = 1000, Estado = true });
            // PRODUCTOS: 5 registros
            TListaProducto.Agregar(new Producto { Id = 1, Nombre = "Laptop Lenovo", Precio = 650.00m, Stock = 10, IdCategoria = 1, IdProveedor = 1, IdLocal = 1, IdInventario = 1, Descripcion = "Laptop para oficina" });
            TListaProducto.Agregar(new Producto { Id = 2, Nombre = "Teclado Logitech", Precio = 25.50m, Stock = 4, IdCategoria = 2, IdProveedor = 2, IdLocal = 2, IdInventario = 2, Descripcion = "Teclado USB" });
            TListaProducto.Agregar(new Producto { Id = 3, Nombre = "Procesador Intel", Precio = 220.00m, Stock = 8, IdCategoria = 3, IdProveedor = 3, IdLocal = 3, IdInventario = 3, Descripcion = "Procesador para PC" });
            TListaProducto.Agregar(new Producto { Id = 4, Nombre = "SSD Kingston 1TB", Precio = 85.00m, Stock = 3, IdCategoria = 4, IdProveedor = 4, IdLocal = 4, IdInventario = 4, Descripcion = "Unidad de estado solido" });
            TListaProducto.Agregar(new Producto { Id = 5, Nombre = "Mouse Gamer", Precio = 18.75m, Stock = 15, IdCategoria = 2, IdProveedor = 5, IdLocal = 5, IdInventario = 5, Descripcion = "Mouse con sensor optico" });
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


        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (comboBox1.SelectedIndex)
            {
                case 0:
                    dataGridView1.DataSource = null;
                    dataGridView1.DataSource = TListaProducto.Listar();
                    break;

                case 1:
                    dataGridView1.DataSource = null;
                    dataGridView1.DataSource = TListaCategoria.Listar();
                    break;

                case 2:
                    dataGridView1.DataSource = null;
                    dataGridView1.DataSource = TListaProveedor.Listar();
                    break;

                case 3:
                    dataGridView1.DataSource = null;
                    dataGridView1.DataSource = TListaLocal.Listar();
                    break;

                case 4:
                    dataGridView1.DataSource = null;
                    dataGridView1.DataSource = TListaInventario.Listar();
                    break;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            frmAdminProducto frm = new frmAdminProducto();
            frm.ShowDialog();
        }
        private void button2_Click(object sender, EventArgs e)
        {
            // Lo programaremos despues.
        }

        private void button3_Click(object sender, EventArgs e)
        {
            // Lo programaremos despues.
        }

        private void button4_Click(object sender, EventArgs e)
        {
            // Lo programaremos despues.
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            // No necesitamos codigo aqui por ahora.
        }
    }
}