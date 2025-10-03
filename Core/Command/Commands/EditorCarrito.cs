using DeliveryGo.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DeliveryGo.Core.Command.Commands
{
    //EditorCarrito es el invoker que ejecuta, deshace y rehace los comandos
    public class EditorCarrito
    {
        private readonly Stack<ICommand> _undo = new Stack<ICommand>();
        private readonly Stack<ICommand> _redo = new Stack<ICommand>();

        //Run=Ejecutar
        public void Run(ICommand cmd) 
        {
            //Ejecuta el comando
            cmd.Execute();
            // Lo guarda en la pila de deshacer 
            _undo.Push(cmd);
            //Limpia la pila de rehacer
            _redo.Clear();  
        }
        //undo=Deshacer
        public void Undo() 
        {
            //Si no hay comandos para deshacer, sale
            if (_undo.Count == 0) return;
            //Saca el ultimo comando de la pila de deshacer(Usando Pop())
            var cmd = _undo.Pop();
            //Deshace el comando
            cmd.Undo();
            //Lo guarda en la pila de rehacer
            _redo.Push(cmd);
        }
        //redo=Rehacer
        public void Redo() 
        {
            //Si no hay comandos para rehacer, sale
            if (_redo.Count == 0) return;
            //Saca el ultimo comando de la pila de rehacer(Usando Pop())
            var cmd = _redo.Pop();
            //Rehace el comando
            cmd.Execute();
            //Lo guarda en la pila de deshacer
            _undo.Push(cmd);
        }
    }
}
