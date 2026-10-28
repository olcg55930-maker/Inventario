using Inventario.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventario.Controlador
{
    public static class TListaProducto
    {
        private static List<Producto> listaProductos =
            new List<Producto>();

        public static void Agregar(Producto producto)
        {
            if (producto == null)
                return;

            if (producto.Precio < 0)
                throw new ArgumentException(
                    "El precio no puede ser negativo.");

            if (producto.Stock < 0)
                throw new ArgumentException(
                    "El stock no puede ser negativo.");

            if (BuscarPorId(producto.Id) != null)
                throw new ArgumentException(
                    "Ya existe un producto con ese ID.");

            listaProductos.Add(producto);
        }

        public static bool Modificar(Producto producto)
        {
            if (producto == null ||
                producto.Precio < 0 ||
                producto.Stock < 0)
                return false;

            Producto existente = BuscarPorId(producto.Id);

            if (existente == null)
                return false;

            existente.Nombre = producto.Nombre;
            existente.Precio = producto.Precio;
            existente.Stock = producto.Stock;
            existente.IdCategoria = producto.IdCategoria;
            existente.IdProveedor = producto.IdProveedor;
            existente.IdLocal = producto.IdLocal;
            existente.IdInventario = producto.IdInventario;
            existente.Descripcion = producto.Descripcion;

            return true;
        }

        public static bool Eliminar(int id)
        {
            Producto producto = BuscarPorId(id);

            if (producto == null)
                return false;

            return listaProductos.Remove(producto);
        }

        public static Producto BuscarPorId(int id)
        {
            return listaProductos.FirstOrDefault(
                p => p.Id == id);
        }

        public static List<Producto> Listar()
        {
            return listaProductos.ToList();
        }

        public static List<Producto> BuscarPorNombre(string nombre)
        {
            nombre = nombre ?? "";

            return listaProductos
                .Where(p => p.Nombre.IndexOf(
                    nombre, StringComparison.OrdinalIgnoreCase) >= 0)
                .ToList();
        }

        public static List<Producto> ProductosStockBajo(int stockMinimo)
        {
            return listaProductos
                .Where(p => p.Stock <= stockMinimo)
                .ToList();
        }

        public static decimal CalcularValorTotal()
        {
            return listaProductos.Sum(
                p => p.Precio * p.Stock);
        }
    }
}
