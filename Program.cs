using DeliveryGo.Core.Command;
using DeliveryGo.Core.Facade;
using DeliveryGo.Core.Order;
using DeliveryGo.Core.Order.Observers;
using DeliveryGo.Core.Strategy.Strategies;
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
                Console.WriteLine("=== DELIVERY GO 🚚 ===");
                Console.WriteLine("1. Agregar producto");
                Console.WriteLine("2. Quitar producto");
                Console.WriteLine("3. Cambiar cantidad");
                Console.WriteLine("4. Ver carrito");
                Console.WriteLine("5. Calcular total con envío");
                Console.WriteLine("6. Pagar pedido");
                Console.WriteLine("7. Confirmar pedido");
                Console.WriteLine("8. Cambiar tipo de envío");
                Console.WriteLine($"9. Observers: {(observersActivos ? "ON ✅" : "OFF ❌")}");
                Console.WriteLine("0. Salir");
                Console.Write("\nSeleccioná una opción: ");

                if (!int.TryParse(Console.ReadLine(), out opcion))
                {
                    Console.WriteLine("⚠️ Opción inválida");
                    continue;
                }

                switch (opcion)
                {
                    case 1:
                        Console.Write("SKU: ");
                        string sku = Console.ReadLine();
                        Console.Write("Nombre: ");
                        string nombre = Console.ReadLine();
                        Console.Write("Precio: ");
                        decimal precio = decimal.Parse(Console.ReadLine());
                        Console.Write("Cantidad: ");
                        int cantidad = int.Parse(Console.ReadLine());
                        checkout.AgregarItem(sku, nombre, precio, cantidad);
                        Console.WriteLine("\n✅ Producto agregado.");
                        break;

                    case 2:
                        Console.Write("SKU a quitar: ");
                        checkout.QuitarItem(Console.ReadLine());
                        Console.WriteLine("\n🗑️ Producto quitado.");
                        break;

                    case 3:
                        Console.Write("SKU: ");
                        string skuCambiar = Console.ReadLine();
                        Console.Write("Nueva cantidad: ");
                        int nuevaCant = int.Parse(Console.ReadLine());
                        checkout.CambiarCantidad(skuCambiar, nuevaCant);
                        Console.WriteLine("\n♻️ Cantidad actualizada.");
                        break;

                    case 4:
                        var items = carrito.ObtenerItems();
                        Console.WriteLine("\n🛒 CARRITO ACTUAL:");
                        if (items.Any())
                        {
                            foreach (var item in items)
                                Console.WriteLine($"- {item.Nombre} ({item.Sku}) x{item.Cantidad} = ${item.Precio * item.Cantidad}");
                            Console.WriteLine($"Subtotal: ${carrito.Subtotal()}");
                        }
                        else
                            Console.WriteLine("Carrito vacío.");
                        break;

                    case 5:
                        var total = checkout.CalcularTotal();
                        Console.WriteLine($"\n🚚 Total con envío: ${total}");
                        break;

                    case 6:
                        Console.Write("Tipo de pago (mp-adapter / tarjeta / transferencia): ");
                        string tipoPago = Console.ReadLine();
                        Console.Write("¿Aplicar IVA? (s/n): ");
                        bool aplicarIVA = Console.ReadLine().ToLower() == "s";
                        Console.Write("¿Cupón de descuento? (dejar vacío si no): ");
                        string cuponStr = Console.ReadLine();
                        decimal? cupon = null;
                        if (!string.IsNullOrWhiteSpace(cuponStr))
                        {
                            cupon = decimal.Parse(cuponStr);
                        }

                        bool ok = checkout.Pagar(tipoPago, aplicarIVA, cupon);
                        Console.WriteLine(ok ? "\n✅ Pago procesado correctamente." : "\n❌ Error al procesar el pago.");
                        break;

                    case 7:
                        Console.Write("Dirección de entrega: ");
                        string dir = Console.ReadLine();
                        Console.Write("Tipo de pago usado: ");
                        string tipo = Console.ReadLine();
                        var pedido = checkout.ConfirmarPedido(dir, tipo);
                        Console.WriteLine($"\n📦 Pedido #{pedido.Id} confirmado. Total: ${pedido.Monto}");
                        break;

                    case 8:
                        Console.WriteLine("Seleccione método de envío:");
                        Console.WriteLine("1. Moto");
                        Console.WriteLine("2. Correo");
                        Console.WriteLine("3. Retiro en tienda");
                        string opEnvio = Console.ReadLine();

                        switch (opEnvio)
                        {
                            case "1": envio = new EnvioMoto(); break;
                            case "2": envio = new EnvioCorreo(); break;
                            case "3": envio = new RetiroEnTienda(); break;
                            default: Console.WriteLine("Opción inválida."); break;
                        }
                        checkout.ElegirEnvio(envio);
                        Console.WriteLine("🚚 Tipo de envío actualizado.");
                        break;

                    case 9:
                        observersActivos = !observersActivos;
                        if (observersActivos)
                        {
                            clienteObs.Suscribir(pedidoService);
                            logisticaObs.Suscribir(pedidoService);
                            auditoriaObs.Suscribir(pedidoService);
                            Console.WriteLine("🔔 Observers activados.");
                        }
                        else
                        {
                            clienteObs.Desuscribir(pedidoService);
                            logisticaObs.Desuscribir(pedidoService);
                            auditoriaObs.Desuscribir(pedidoService);
                            Console.WriteLine("🔕 Observers desactivados.");
                        }
                        break;

                    case 0:
                        Console.WriteLine("👋 ¡Gracias por usar DeliveryGO!");
                        break;

                    default:
                        Console.WriteLine("⚠️ Opción inválida.");
                        break;
                }

                Console.WriteLine("\nPresione una tecla para continuar...");
                Console.ReadKey();

            } while (opcion != 0);
        }
    }
}
