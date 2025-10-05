using DeliveryGo.Core.Order;
using DeliveryGo.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeliveryGo.Core.Facade
{
    public class CheckoutFacade
    {
        public CheckoutFacade(ICarritoPort carrito, IEnvioStrategy envio, PedidoService pedidos)
        {
            /* ... */
        }

        public void AgregarItem(string sku, string nombre, decimal precio, int cantidad) { /* Command */ }
        public void CambiarCantidad(string sku, int cantidad) { /* Command */ }
        public void QuitarItem(string sku) { /* Command */ }
        public void ElegirEnvio(IEnvioStrategy estrategia) { /* Strategy swap */ }
        public decimal CalcularTotal() { /* subtotal + envío (no aplicar IVA acá) */ return 0; }
        public bool Pagar(string tipoPago, bool aplicarIVA, decimal? cupon = null)
        {
            /* Factory/Adapter/Decorator */
            return false;
        }
        public Pedido ConfirmarPedido(string direccion, string tipoPago)
        {
            /* Builder + Observer */
            return new Pedido();
        }
    }

}
