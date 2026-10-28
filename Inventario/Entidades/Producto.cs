using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace Inventario.Entidades
{
    public class Producto
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = "";
        public decimal Precio { get; set; }
        public int Stock { get; set; }
        public int IdCategoria { get; set; }
        public int IdProveedor { get; set; }
        public int IdLocal { get; set; }
        public int IdInventario { get; set; }
        public string Descripcion { get; set; } = "";

        public decimal CalcularValorTotal()
        {
            return Precio * Stock;
        }

        public bool TieneStockBajo(int stockMinimo)
        {
            return Stock <= stockMinimo;
        }

        public void ActualizarPrecio(decimal nuevoPrecio)
        {
            if (nuevoPrecio < 0)
                throw new ArgumentException(
                    "El precio no puede ser negativo.");

            Precio = nuevoPrecio;
        }

        public void ActualizarStock(int cantidad)
        {
            if (Stock + cantidad < 0)
                throw new ArgumentException(
                    "El stock no puede quedar negativo.");

            Stock += cantidad;
        }
    }
}
