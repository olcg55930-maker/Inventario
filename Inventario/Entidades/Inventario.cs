using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;




namespace Inventario.Entidades
{
    public class Inventario
    {
        public int IdInventario { get; set; }
        public string Nombre { get; set; } = "";
        public string CodigoInventario { get; set; } = "";
        public DateTime FechaActualizacion { get; set; } = DateTime.Now;
        public int StockMinimo { get; set; } = 5;
        public int StockMaximo { get; set; } = 1000;
        public bool Estado { get; set; } = true;

        public void ActualizarFecha()
        {
            FechaActualizacion = DateTime.Now;
        }

        public void ActualizarStockMinimo(int nuevoMinimo)
        {
            if (nuevoMinimo < 0)
                throw new ArgumentException(
                    "El stock mínimo no puede ser negativo.");

            if (nuevoMinimo > StockMaximo)
                throw new ArgumentException(
                    "El stock mínimo no puede superar el máximo.");

            StockMinimo = nuevoMinimo;
        }

        public void ActualizarStockMaximo(int nuevoMaximo)
        {
            if (nuevoMaximo < StockMinimo)
                throw new ArgumentException(
                    "El stock máximo no puede ser menor que el mínimo.");

            StockMaximo = nuevoMaximo;
        }

        public bool VerificarStock(int stock)
        {
            return stock >= StockMinimo && stock <= StockMaximo;
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

