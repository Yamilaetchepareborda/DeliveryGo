using DeliveryGo.Core.Command;
using DeliveryGo.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace DeliveryGo.Core.Order
{
    public class PedidoBuilderBase
    {


        public IPedidoBuilder ConItems(IEnumerable<Item> items)
        {
            _pedido.Items = items?.ToList() ?? new List<Item>();
            return this;
        }
    }
}