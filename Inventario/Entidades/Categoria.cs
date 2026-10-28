using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Inventario.Entidades
{
    public class Categoria
    {
        public int IdCategoria { get; set; }
        public string Nombre { get; set; } = "";
        public string Descripcion { get; set; } = "";
        public bool Estado { get; set; } = true;

        public void ModificarNombre(string nuevoNombre)
        {
            if (string.IsNullOrWhiteSpace(nuevoNombre))
                throw new System.ArgumentException(
                    "El nombre de la categoría es obligatorio.");

            Nombre = nuevoNombre;
        }

        public void ModificarDescripcion(string nuevaDescripcion)
        {
            Descripcion = nuevaDescripcion ?? "";
        }

        public void Activar()
        {
            Estado = true;
        }

        public void Desactivar()
        {
            Estado = false;
        }
    }
}
