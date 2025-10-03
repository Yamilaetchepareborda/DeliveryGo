using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeliveryGo.Interfaces
{
    public interface ICommand
    {
        //Iterface que implementan los comandos
        //Execute:Ejecuta la accion del comando
        void Execute();
        //Undo:Deshace la accion del comando
        void Undo();
    }
}
