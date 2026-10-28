using Inventario.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventario.Controlador
{
    public static class TListaCategoria
    {
        private static List<Categoria> listaCategorias =
            new List<Categoria>();

        public static void Agregar(Categoria categoria)
        {
            if (categoria == null)
                return;

            listaCategorias.Add(categoria);
        }

        public static bool Modificar(Categoria categoria)
        {
            if (categoria == null)
                return false;

            Categoria existente = BuscarPorId(categoria.IdCategoria);

            if (existente == null)
                return false;

            existente.Nombre = categoria.Nombre;
            existente.Descripcion = categoria.Descripcion;
            existente.Estado = categoria.Estado;

            return true;
        }

        public static bool Eliminar(int id)
        {
            Categoria categoria = BuscarPorId(id);

            if (categoria == null)
                return false;

            return listaCategorias.Remove(categoria);
        }

        public static Categoria BuscarPorId(int id)
        {
            return listaCategorias.FirstOrDefault(
                c => c.IdCategoria == id);
        }

        public static List<Categoria> Listar()
        {
            return listaCategorias.ToList();
        }

        public static List<Categoria> BuscarPorNombre(string nombre)
        {
            nombre = nombre ?? "";

            return listaCategorias
                .Where(c => c.Nombre.Contains(nombre))
                .ToList();
        }
    }
}