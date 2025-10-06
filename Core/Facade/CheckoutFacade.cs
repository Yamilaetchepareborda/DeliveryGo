using DeliveryGo.Core.Command;
using DeliveryGo.Core.Command.Commands;
using DeliveryGo.Core.Order;
using DeliveryGo.Core.Payment;
using DeliveryGo.Core.Payment.Adapters;
using DeliveryGo.Interfaces;
using DeliveryGo.Core.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static DeliveryGo.Core.Enum.Enums;
using DeliveryGo.Core.Payment.Decorators;


namespace DeliveryGo.Core.Facade
{
    public class CheckoutFacade
    {

        private readonly ICarritoPort _carrito;
        private IEnvioStrategy _envioActual;
        private readonly PedidoService _pedidoService;
        public CheckoutFacade(ICarritoPort carrito, IEnvioStrategy envio, PedidoService pedidos)
        {
            _carrito = carrito;
            _envioActual = envio;
            _pedidoService = pedidos;
        }

        public void AgregarItem(string sku, string nombre, decimal precio, int cantidad)
        {
            var item = new Item (sku, nombre, precio, cantidad);
            _carrito.Run(_carrito.AgregarItem(item));
        }
        public void CambiarCantidad(string sku, int cantidad) 
        { 
           _carrito.Run(_carrito.SetCantidad(sku, cantidad));
        }
        public void QuitarItem(string sku)
        {
            _carrito.Run(_carrito.QuitarItem(sku));
        }
        public void ElegirEnvio(IEnvioStrategy estrategia) 
        {
            _envioActual = estrategia;
        }
        public decimal CalcularTotal() 
        { 
            var subtotal = _carrito.Subtotal();
           
            return subtotal + _envioActual.Calcular(subtotal);
        }
        public bool Pagar(string tipoPago, bool aplicarIVA, decimal? cupon = null)
        {
            IPago pago;

            //FACTORY + ADAPTER
            if (tipoPago == "mp-adapter")
                pago = new PagoAdapterMp(new MpSdkFalsa());
            else
                pago = PagoFactory.Create(tipoPago);

            //DECORATOR
            if(aplicarIVA)
                pago = new PagoConImpuesto(pago);


            if (cupon.HasValue)
                pago = new PagoConCupon(pago, cupon.Value);


            return pago.Procesar(CalcularTotal());
        }
        public Pedido ConfirmarPedido(string direccion, string tipoPago)
        {
            var builder = new PedidoBuilder()
             .ConItems(
             _carrito.ObtenerItems()
            .Select(i => (sku: i.Sku, nombre: i.Nombre, precio: i.Precio, cantidad: i.Cantidad)))
            .ConDireccion(direccion)
            .ConMetodoPago(tipoPago)
            .ConMonto(CalcularTotal());

            var pedido = builder.Build();
            pedido.Id = new Random().Next(1000, 9999);

            _pedidoService.CambiarEstado(pedido.Id, EstadoPedido.Recibido);
            _pedidoService.CambiarEstado(pedido.Id, EstadoPedido.Preparando);
            _pedidoService.CambiarEstado(pedido.Id, EstadoPedido.Enviado);
            _pedidoService.CambiarEstado(pedido.Id, EstadoPedido.Entregado);

            return pedido;
        }
    }

}
