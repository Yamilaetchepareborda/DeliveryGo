using DeliveryGo.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DeliveryGo.Core.Command;
using DeliveryGo.Core.Enum;
using static DeliveryGo.Core.Enum.Enums;




namespace DeliveryGo.Core.Order
{
    public class PedidoBuilder : IPedidoBuilder
    {
        private readonly Pedido _pedido = new Pedido();

        public IPedidoBuilder ConDireccion(string direccion)
        {
            _pedido.Direccion = direccion?.Trim() ?? "";
            return this;
        }

        public IPedidoBuilder ConMetodoPago(string tipoPago)
        {
            _pedido.TipoPago = tipoPago?.Trim() ?? "";
            return this;
        }

        public IPedidoBuilder ConItems(IEnumerable<(string sku, string nombre, decimal precio, int cantidad)> items)
        {
            _pedido.Items = items
            .Select(i => new Item(i.sku, i.nombre, i.precio, i.cantidad))
            .ToList();
            return this;
        }



        public IPedidoBuilder ConMonto(decimal monto)
        {
            _pedido.Monto = monto;
            return this;
        }

        public Pedido Build()
        {
            if (_pedido.Items == null || _pedido.Items.Count == 0)

                throw new InvalidOperationException("El pedido debe tener al menos un item.");

            

            if(string.IsNullOrWhiteSpace(_pedido.Direccion))
            
                throw new InvalidOperationException("La direccion no puede estar vacia.");
            

            _pedido.Estado = EstadoPedido.Recibido;
            return _pedido;
        }
    }
}
