using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeliveryGo.Core.Order.Observers
{
    public class ClienteObserver
    {
        public void Suscribir(PedidoService servicio)
        {
            servicio.EstadoCambiado += OnEstadoCambiado;
        }

        public void Desuscribir(PedidoService servicio)
        {
            servicio.EstadoCambiado -= OnEstadoCambiado;
        }

        public void OnEstadoCambiado(object sender, PedidoChangedEventArgs e)
        {
            //notificar al cliente sobre el cambio de estado del pedido
            Console.WriteLine($"Notificacion a Cliente : El pedido {e.PedidoId} ha cambiado a estado {e.NuevoEstado} en {e.Cuando}.");
        }
    }
}
