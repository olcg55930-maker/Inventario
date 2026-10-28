using Inventario.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventario.Controlador
{
    public static class TListaLocal
    {
        private static List<Local> listaLocales =
            new List<Local>();

        public static void Agregar(Local local)
        {
            if (local == null)
                return;

            listaLocales.Add(local);
        }

        public static bool Modificar(Local local)
        {
            if (local == null)
                return false;

            Local existente = BuscarPorId(local.IdLocal);

            if (existente == null)
                return false;

            existente.Nombre = local.Nombre;
            existente.Direccion = local.Direccion;
            existente.Ciudad = local.Ciudad;
            existente.Telefono = local.Telefono;
            existente.Responsable = local.Responsable;
            existente.TipoLocal = local.TipoLocal;

            return true;
        }

        public static bool Eliminar(int id)
        {
            Local local = BuscarPorId(id);

            if (local == null)
                return false;

            return listaLocales.Remove(local);
        }

        public static Local BuscarPorId(int id)
        {
            return listaLocales.FirstOrDefault(
                l => l.IdLocal == id);
        }

        public static List<Local> Listar()
        {
            return listaLocales.ToList();
        }

        public static List<Local> BuscarPorNombre(string nombre)
        {
            nombre = nombre ?? "";

            return listaLocales
                .Where(l => l.Nombre.Contains(nombre))
                .ToList();
        }

        public static List<Local> BuscarPorCiudad(string ciudad)
        {
            ciudad = ciudad ?? "";

            return listaLocales
                .Where(l => l.Ciudad.Contains(ciudad))
                .ToList();
        }
    }
}
