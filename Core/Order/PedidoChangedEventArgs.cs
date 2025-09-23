using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static DeliveryGo.Core.Enum.Enums;

namespace DeliveryGo.Core.Order
{
    public class PedidoChangedEventArgs : EventArgs
    {
        public int PedidoId { get; }
        public EstadoPedido NuevoEstado { get; }
        public DateTime Cuando { get; }

        public PedidoChangedEventArgs(int id, EstadoPedido estado, DateTime cuando)
            => (PedidoId, NuevoEstado, Cuando) = (id, estado, cuando);
    }

}
