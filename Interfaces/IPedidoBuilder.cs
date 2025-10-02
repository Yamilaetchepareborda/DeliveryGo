using DeliveryGo.Core.Order;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeliveryGo.Interfaces
{
    public interface IPedidoBuilder
    {
        IPedidoBuilder ConItems(IEnumerable<(string sku, string nombre, decimal precio, int cantidad)> items);
        IPedidoBuilder ConDireccion(string direccion);
        IPedidoBuilder ConMetodoPago(string tipoPago); // "tarjeta", "mp", "transf", "adapter"
        IPedidoBuilder ConMonto(decimal monto);
        Pedido Build();
    }

}
