using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace Inventario.Entidades
{
    public class Proveedor
    {
        public int IdProveedor { get; set; }
        public string Nombre { get; set; } = "";
        public string Ruc { get; set; } = "";
        public string Telefono { get; set; } = "";
        public string Direccion { get; set; } = "";

        public void ActualizarNombre(string nuevoNombre)
        {
            if (string.IsNullOrWhiteSpace(nuevoNombre))
                throw new ArgumentException(
                    "El nombre del proveedor es obligatorio.");

            Nombre = nuevoNombre;
        }

        public void ActualizarTelefono(string nuevoTelefono)
        {
            Telefono = nuevoTelefono ?? "";
        }

        public void ActualizarDireccion(string nuevaDireccion)
        {
            Direccion = nuevaDireccion ?? "";
        }

        public bool ValidarRuc()
        {
            return !string.IsNullOrWhiteSpace(Ruc)
                   && Ruc.Length == 13
                   && EsNumerico(Ruc);
        }

        private bool EsNumerico(string valor)
        {
            foreach (char caracter in valor)
            {
                if (!char.IsDigit(caracter))
                    return false;
            }

            return true;
        }

        public string MostrarInformacion()
        {
            return $"Proveedor: {Nombre}\n" +
                   $"RUC: {Ruc}\n" +
                   $"Teléfono: {Telefono}\n" +
                   $"Dirección: {Direccion}";
        }
    }
}

