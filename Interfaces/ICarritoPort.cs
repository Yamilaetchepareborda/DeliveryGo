using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeliveryGo.Interfaces
{
    public interface ICarritoPort
    {
        decimal Subtotal();                      // suma de (precio * cantidad)
        void Run(ICommand cmd);                  // ejecuta comando y guarda en historial
        void Undo();
        void Redo();
    }

}
