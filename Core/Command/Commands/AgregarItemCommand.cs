using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DeliveryGo.Interfaces;

namespace DeliveryGo.Core.Command.Commands
{
    public class AgregarItemCommand:ICommand
    {
    private readonly Carrito _c;
    private readonly Item _i;

        public AgregarItemCommand(Carrito c, Item i)
        {
            _c = c;
            _i= i;
        }
        //Ejecuta el comando Agregar de la clase carrito
        public void Execute() 
        {
            _c.Agregar(_i);
        }
        //Deshace el comando Agregar de la clase carrito
        public void Undo() 
        {
        _c.Quitar(_i.Sku);
        }
    }
}
