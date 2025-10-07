using DeliveryGo.Core.Command;
using DeliveryGo.Core.Facade;
using DeliveryGo.Core.Order;
using DeliveryGo.Core.Order.Observers;
using DeliveryGo.Core.Strategy.Strategies;
using DeliveryGo.Core.Utils;
using DeliveryGo.Interfaces;
using System;
using System.Linq;

namespace DeliveryGo
{
    internal class Program
    {
        static void Main(string[] args)
        {
            ICarritoPort carrito = new CarritoPort();
            IEnvioStrategy envio = new EnvioMoto(); // Por defecto
            PedidoService pedidoService = new PedidoService();
            var checkout = new CheckoutFacade(carrito, envio, pedidoService);

            // Observers
            var clienteObs = new ClienteObserver();
            var logisticaObs = new LogisticaObserver();
            var auditoriaObs = new AuditoriaObserver();
            bool observersActivos = true;

            // Suscribir por defecto
            clienteObs.Suscribir(pedidoService);
            logisticaObs.Suscribir(pedidoService);
            auditoriaObs.Suscribir(pedidoService);

            int opcion;
           
            do
            {
                Console.Clear();
                Console.WriteLine("=== DELIVERY GO ===");
                Console.WriteLine("1. Agregar producto");
                Console.WriteLine("2. Cambiar cantidad");
                Console.WriteLine("3. Quitar producto");
                Console.WriteLine("4. Ver carrito y subtotal");
                Console.WriteLine("5. Deshacer (Undo)");
                Console.WriteLine("6. Rehacer (Redo)");
                Console.WriteLine("7. Cambiar método de envío");
                Console.WriteLine("8. Pagar pedido");
                Console.WriteLine("9. Confirmar pedido");
                Console.WriteLine($"10. Observers: {(observersActivos ? "ON " : "OFF ")}");
                Console.WriteLine("0. Salir");
                Console.Write("\nSeleccioná una opción: ");

                if (!int.TryParse(Console.ReadLine(), out opcion))
                {
                    Console.WriteLine(" Opción inválida. Intente nuevamente.");
                    continue;
                }

                Console.WriteLine("\n-----------------------------------\n");

                switch (opcion)
                {
                    case 1:
                        string sku = Validador.PedirTexto("SKU: ");
                        string nombre = Validador.PedirTexto("Nombre: ");
                        decimal precio = Validador.PedirDecimal("Precio: ");
                        int cantidad = Validador.PedirEntero("Cantidad: ");
                        checkout.AgregarItem(sku, nombre, precio, cantidad);
                        Console.WriteLine("\n Producto agregado al carrito.");
                        break;

                    case 2:
                        string skuCambiar = Validador.PedirTexto("SKU: ");
                        int nuevaCant = Validador.PedirEntero("Nueva cantidad: ");
                        checkout.CambiarCantidad(skuCambiar, nuevaCant);
                        Console.WriteLine("\n Cantidad actualizada.");
                        break;

                    case 3:
                        string skuQuitar = Validador.PedirTexto("SKU a quitar: ");
                        checkout.QuitarItem(skuQuitar);
                        Console.WriteLine("\n Producto quitado del carrito.");
                        break;

                    case 4:
                        var items = carrito.ObtenerItems();
                        Console.WriteLine("\n CARRITO ACTUAL:");
                        if (items.Any())
                        {
                            foreach (var item in items)
                                Console.WriteLine($"- {item.Nombre} ({item.Sku}) x{item.Cantidad} = ${item.Precio * item.Cantidad}");
                            Console.WriteLine($"\nSubtotal: ${carrito.Subtotal()}");
                            Console.WriteLine($"Total con envío: ${checkout.CalcularTotal()}");
                        }
                        else
                            Console.WriteLine("Carrito vacío.");
                        break;

                    case 5:
                        carrito.Undo();
                        Console.WriteLine("↩️ Se deshizo la última acción (Undo).");
                        break;

                    case 6:
                        carrito.Redo();
                        Console.WriteLine("↪️ Se rehízo la última acción (Redo).");
                        break;

                    case 7:
                        Console.WriteLine("Seleccione método de envío:");
                        Console.WriteLine("1. Moto");
                        Console.WriteLine("2. Correo");
                        Console.WriteLine("3. Retiro en tienda");
                        string opEnvio = Validador.PedirTexto("Opción: ");

                        switch (opEnvio)
                        {
                            case "1": envio = new EnvioMoto(); break;
                            case "2": envio = new EnvioCorreo(); break;
                            case "3": envio = new RetiroEnTienda(); break;
                            default:
                                Console.WriteLine("Opción inválida, se mantiene el envío actual.");
                                break;
                        }
                        checkout.ElegirEnvio(envio);
                        Console.WriteLine(" Tipo de envío actualizado.");
                        break;

                    case 8:
                        string tipoPago = Validador.PedirTexto("Tipo de pago (mp-adapter / tarjeta / transferencia): ");
                        bool aplicarIVA = Validador.PedirConfirmacion("¿Aplicar IVA?");
                        Console.Write("¿Cupón de descuento? (dejar vacío si no): ");
                        string cuponStr = Console.ReadLine();
                        decimal? cupon = null;
                        if (!string.IsNullOrWhiteSpace(cuponStr))
                        {
                            if (decimal.TryParse(cuponStr, out decimal valor))
                                cupon = valor;
                            else
                                Console.WriteLine(" Cupón inválido. Se ignorará el descuento.");
                        }

                        bool ok = checkout.Pagar(tipoPago, aplicarIVA, cupon);
                        Console.WriteLine(ok ? "\nPago procesado correctamente." : "\n Error al procesar el pago.");
                        break;

                    case 9:
                        string dir = Validador.PedirTexto("Dirección de entrega: ");
                        string tipo = Validador.PedirTexto("Tipo de pago usado: ");
                        var pedido = checkout.ConfirmarPedido(dir, tipo);
                        Console.WriteLine($"\n Pedido #{pedido.Id} confirmado. Total: ${pedido.Monto}");
                        break;

                    case 10:
                        observersActivos = !observersActivos;
                        if (observersActivos)
                        {
                            clienteObs.Suscribir(pedidoService);
                            logisticaObs.Suscribir(pedidoService);
                            auditoriaObs.Suscribir(pedidoService);
                            Console.WriteLine(" Observers activados.");
                        }
                        else
                        {
                            clienteObs.Desuscribir(pedidoService);
                            logisticaObs.Desuscribir(pedidoService);
                            auditoriaObs.Desuscribir(pedidoService);
                            Console.WriteLine(" Observers desactivados.");
                        }
                        break;

                    case 0:
                        Console.WriteLine("¡Gracias por usar DeliveryGO!");
                        break;

                    default:
                        Console.WriteLine(" Opción inválida.");
                        break;
                }

                Console.WriteLine("\n-----------------------------------");
                Console.WriteLine("Presione una tecla para continuar...");
                Console.ReadKey();

            } while (opcion != 0);


        }
    }
}
