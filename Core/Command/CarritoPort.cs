using DeliveryGo.Core.Command.Commands;
using DeliveryGo.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeliveryGo.Core.Command
{
    public class CarritoPort : ICarritoPort
    {
        private readonly Carrito _carrito= new Carrito();
        private readonly EditorCarrito _editorC=new EditorCarrito();
        //En esta clase se delegan las operaciones al carrito y al editor de comandos(EditorCarrito)
        public decimal Subtotal()=> _carrito.Subtotal();
        public void Run(ICommand cmd) => _editorC.Run(cmd);
        public void Redo()=> _editorC.Redo();
        public void Undo()=> _editorC.Undo();
        
        
        //Estos metodos crean los comandos y los retornan para que puedan ser ejecutados por el editor.
        //Sirve para que no quede expuesto a accesos indeseados el carrito.
        public ICommand AgregarItem(Item i)=> new AgregarItemCommand(_carrito, i);
        public ICommand QuitarItem(string sku)=> new QuitarItemCommand(_carrito, sku);
        public ICommand SetCantidad(string sku, int cantidad)=> new SetCantidadCommand(_carrito, sku, cantidad);

        public List<Item> ObtenerItems()
        {
            // Devuelve una copia de la lista actual de items del carrito
            return _carrito.GetItems();
        }


    }
}
