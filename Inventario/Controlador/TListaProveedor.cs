using Inventario.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventario.Controlador
{
    public static class TListaProveedor
    {
        private static List<Proveedor> listaProveedores =
            new List<Proveedor>();

        public static void Agregar(Proveedor proveedor)
        {
            if (proveedor == null)
                return;

            listaProveedores.Add(proveedor);
        }

        public static bool Modificar(Proveedor proveedor)
        {
            if (proveedor == null)
                return false;

            Proveedor existente = BuscarPorId(proveedor.IdProveedor);

            if (existente == null)
                return false;

            existente.Nombre = proveedor.Nombre;
            existente.Ruc = proveedor.Ruc;
            existente.Telefono = proveedor.Telefono;
            existente.Direccion = proveedor.Direccion;

            return true;
        }

        public static bool Eliminar(int id)
        {
            Proveedor proveedor = BuscarPorId(id);

            if (proveedor == null)
                return false;

            return listaProveedores.Remove(proveedor);
        }

        public static Proveedor BuscarPorId(int id)
        {
            return listaProveedores.FirstOrDefault(
                p => p.IdProveedor == id);
        }

        public static List<Proveedor> Listar()
        {
            return listaProveedores.ToList();
        }

        public static List<Proveedor> BuscarPorNombre(string nombre)
        {
            nombre = nombre ?? "";

            return listaProveedores
                .Where(p => p.Nombre.Contains(nombre))
                .ToList();
        }
    }
}
