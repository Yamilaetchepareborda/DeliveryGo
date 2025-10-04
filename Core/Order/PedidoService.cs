using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static DeliveryGo.Core.Enum.Enums;

namespace DeliveryGo.Core.Order
{
    public class PedidoService
    {
        public event EventHandler<PedidoChangedEventArgs> EstadoCambiado;

        public void CambiarEstado(int pedidoId, EstadoPedido nuevo)
            => EstadoCambiado?.Invoke(this, new PedidoChangedEventArgs(pedidoId, nuevo, DateTime.Now));
    }

}
