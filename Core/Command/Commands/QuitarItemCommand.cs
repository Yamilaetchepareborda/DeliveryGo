using DeliveryGo.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeliveryGo.Core.Command.Commands
{
    public class QuitarItemCommand:ICommand
    {
        private readonly Carrito _c;
        private readonly string _sku;
        private Item _backup;
        public QuitarItemCommand(Carrito c, String sku)
        {
            _c = c;
            _sku = sku;
        }
        //Ejecuta el comando Quitar de la clase carrito y guarda una copia del item en _backup para poder deshacerlo
        public void Execute() 
        {
        _backup=_c.Quitar(_sku);
        }
        //Deshace el comando Quitar de la clase carrito, si _backup es null no hace nada
        public void Undo() 
        {
            if (_backup != null) 
            {
            _c.Agregar(_backup);
            }
        }


    }
}
