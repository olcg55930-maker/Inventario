using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Inventario.Entidades
{
    public class Local
    {
        public int IdLocal { get; set; }
        public string Nombre { get; set; } = "";
        public string Direccion { get; set; } = "";
        public string Ciudad { get; set; } = "";
        public string Telefono { get; set; } = "";
        public string Responsable { get; set; } = "";
        public string TipoLocal { get; set; } = "";

        public void ActualizarDireccion(string nuevaDireccion)
        {
            Direccion = nuevaDireccion ?? "";
        }

        public void ActualizarTelefono(string nuevoTelefono)
        {
            Telefono = nuevoTelefono ?? "";
        }

        public void CambiarResponsable(string nuevoResponsable)
        {
            Responsable = nuevoResponsable ?? "";
        }

        public void CambiarTipoLocal(string nuevoTipo)
        {
            TipoLocal = nuevoTipo ?? "";
        }

        public string MostrarInformacion()
        {
            return $"Local: {Nombre}\n" +
                   $"Dirección: {Direccion}\n" +
                   $"Ciudad: {Ciudad}\n" +
                   $"Teléfono: {Telefono}\n" +
                   $"Responsable: {Responsable}\n" +
                   $"Tipo: {TipoLocal}";
        }
    }
}

