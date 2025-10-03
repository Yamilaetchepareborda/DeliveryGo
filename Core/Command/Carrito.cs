using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeliveryGo.Core.Command
{
    //Clase que representa un carrito de compras
    public class Carrito
    {
        //Diccionario que contiene los items del carrito, la Key(identificador) es el Sku
        Dictionary<string, Item> _items = new Dictionary<string, Item>();
        //Agregar:Si existe el item sumar "Cantidad", si no agregarlo al carrito.
        public void Agregar(Item i)
        {
            if (i.Cantidad > 0)
            {
                if (_items.ContainsKey(i.Sku))
                {
                    _items[i.Sku].Cantidad += i.Cantidad;
                }
                else
                {
                    _items.Add(i.Sku, i);
                }
            }
            else
            {
                Console.WriteLine("ERROR:[EL OBJETO TIENE CANTIDAD NEGATIVA]");
            }

        }
        //Quitar:Si existe el item, quitarlo del carrito y devolverlo, si no existe devolver null e imprimir error.
        public Item Quitar(string sku)
        {
            if (_items.ContainsKey(sku))
            {
                Item backup = _items[sku];
                _items.Remove(sku);
                return backup;
            }
            else
            {
                Console.WriteLine("ERROR:[SKU INEXISTENTE]");
                return null;
            }
        }
        //SetCantidad:Si existe el item, setear la nueva cantidad, si la nueva cantidad es 0 o negativa quitar el item del carrito y devolver true, si no existe devolver "false" e imprimir error.
        public bool SetCantidad(string sku, int nueva)
        {
            if (_items.ContainsKey(sku))
            {
                if (nueva <= 0)
                {
                    _items.Remove(sku);
                    return true;
                }
                else
                {
                    _items[sku].Cantidad = nueva;
                    return true;
                }
            }
            else
            {
                Console.WriteLine("ERROR:[SKU INEXISTENTE]");
                return false;
            }

        }
        //GetCantidad:Si existe el item, devolver la cantidad, si no existe devolver 0.
        public int GetCantidad(string sku)
        {
            if (_items.TryGetValue(sku, out Item item))
            {
                return item.Cantidad;
            }        
            return 0;
        }
        //Subtotal:Devolver el subtotal del carrito (suma de precio * cantidad de cada item)
        public decimal Subtotal() 
        {
            decimal total = 0;  
            foreach (var item in _items.Values)
            {
            total +=  item.Precio * item.Cantidad;
            }
            return total;   
        }
    }
}
