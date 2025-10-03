using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeliveryGo.Core.Command
{
    //Clase que representa un item en el carrito de compras
    public class Item
    {
        //Sku: identificador unico del item

        public string Sku { get; private set; } = "";
        public string Nombre { get; private set; } = "";
        public decimal Precio {get; private set; }
        public int Cantidad {get; set;}

        public Item(string sku, string nombre, decimal precio, int cantidad)
        {
            Sku = sku;
            Nombre = nombre;
            Precio = precio;
            Cantidad = cantidad;
        }
    }
}
