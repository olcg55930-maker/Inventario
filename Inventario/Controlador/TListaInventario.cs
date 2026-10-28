using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventario.Controlador
{
    public static class TListaInventario
    {
        private static List<Inventario.Entidades.Inventario>
            listaInventarios =
            new List<Inventario.Entidades.Inventario>();

        public static void Agregar(Inventario.Entidades.Inventario inventario)
        {
            if (inventario == null)
                return;

            listaInventarios.Add(inventario);
        }

        public static bool Modificar(Inventario.Entidades.Inventario inventario)
        {
            if (inventario == null)
                return false;

            var existente = BuscarPorId(inventario.IdInventario);

            if (existente == null)
                return false;

            existente.Nombre = inventario.Nombre;
            existente.CodigoInventario = inventario.CodigoInventario;
            existente.FechaActualizacion = inventario.FechaActualizacion;
            existente.StockMinimo = inventario.StockMinimo;
            existente.StockMaximo = inventario.StockMaximo;
            existente.Estado = inventario.Estado;

            return true;
        }

        public static bool Eliminar(int id)
        {
            var inventario = BuscarPorId(id);

            if (inventario == null)
                return false;

            return listaInventarios.Remove(inventario);
        }

        public static Inventario.Entidades.Inventario BuscarPorId(int id)
        {
            return listaInventarios.FirstOrDefault(
                i => i.IdInventario == id);
        }

        public static List<Inventario.Entidades.Inventario> Listar()
        {
            return listaInventarios.ToList();
        }

        public static List<Inventario.Entidades.Inventario>
            BuscarPorNombre(string nombre)
        {
            nombre = nombre ?? "";

            return listaInventarios
                .Where(i => i.Nombre.Contains(nombre))
                .ToList();
        }
    }
}