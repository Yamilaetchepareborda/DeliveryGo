using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeliveryGo.Core.Order.Observers
{
    public class AuditoriaObserver
    {
        public void Suscribir (PedidoService servicio)
        {
            servicio.EstadoCambiado += OnEstadoCambiado;
        }

        public void Desuscribir(PedidoService servicio)
        {
            servicio.EstadoCambiado -= OnEstadoCambiado;
        }

        private void OnEstadoCambiado(object sender, PedidoChangedEventArgs e)
            => Console.WriteLine($"Auditoria: El pedido {e.PedidoId} ha cambiado a estado {e.NuevoEstado} en {e.Cuando}.");
    }
}
