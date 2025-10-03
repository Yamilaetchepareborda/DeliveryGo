using DeliveryGo.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeliveryGo.Core.Command.Commands
{
    public class SetCantidadCommand:ICommand
    {
        private readonly Carrito _c;
        private readonly string _sku;
        private readonly int _nueva;
        private int _anterior;

        public SetCantidadCommand(Carrito c, string sku, int nueva)
        {
            _c = c;
            _sku = sku;
            _nueva = nueva;
        }
        //Ejecuta el comando SetCantidad de la clase carrito y guarda la cantidad anterior en _anterior para poder deshacerlo
        public void Execute() 
        {
        _anterior=_c.GetCantidad(_sku);
        
        _c.SetCantidad(_sku, _nueva);
        }
        //Deshace el comando SetCantidad de la clase carrito, si la cantidad anterior es 0 o menor, quita el item
        public void Undo() 
        {
            if (_anterior <= 0) 
            {
            _c.Quitar(_sku);
            }
            else
            {
                _c.SetCantidad(_sku, _anterior);
            }
        }
    }
}
