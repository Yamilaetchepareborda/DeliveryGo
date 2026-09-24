# DeliveryGo 🛵

Console mini e-commerce in **C#** that applies **9 GoF design patterns** to a real checkout flow: cart with undo/redo, shipping strategies, payment methods with taxes and coupons, and order status notifications.

> 🇦🇷 Mini e-commerce de consola en C# que aplica 9 patrones de diseño (Command, Strategy, Factory, Adapter, Decorator, Singleton, Builder, Observer y Facade). Trabajo integral en equipo de Programación II.

```
=== DELIVERY GO ===
1. Agregar producto
2. Cambiar cantidad
3. Quitar producto
4. Ver carrito y subtotal
5. Deshacer (Undo)
6. Rehacer (Redo)
7. Cambiar método de envío
8. Pagar pedido
9. Confirmar pedido
0. Salir
```

## Tech stack

- C# · .NET Framework 4.8 · console app
- Object-oriented design with interfaces (`ICommand`, `IEnvioStrategy`, `IPago`, `IPedidoBuilder`, `ICarritoPort`)

## Features

- Add, remove and change the quantity of cart items, with **undo / redo**
- Choose shipping at runtime: motorbike, mail or store pickup
- Pay by card, bank transfer or a (simulated) Mercado Pago SDK, with optional tax and coupon
- Build and confirm the order with a delivery address and validations
- Order status changes are notified to the client, logistics and audit modules

## Design patterns

| Pattern | Where | Why |
| --- | --- | --- |
| **Command** + undo/redo | `AgregarItemCommand`, `QuitarItemCommand`, `SetCantidadCommand`, `EditorCarrito` | Cart actions as objects with undo/redo stacks |
| **Strategy** | `IEnvioStrategy`, `EnvioMoto`, `EnvioCorreo`, `RetiroEnTienda`, `EnvioService` | Swap the shipping cost algorithm at runtime |
| **Factory** | `PagoFactory` | Create payment methods without scattered `switch` statements |
| **Adapter** | `PagoAdapterMp` → `MpSdkFalsa` | Fit an external payment SDK into the `IPago` contract |
| **Decorator** | `PagoConImpuesto`, `PagoConCupon` | Add tax / discount by wrapping any payment |
| **Singleton** | `ConfigManager` | Shared settings (VAT, free-shipping threshold) |
| **Builder** | `PedidoBuilder` | Build an order step by step and validate before `Build()` |
| **Observer** | `PedidoService` + `ClienteObserver`, `LogisticaObserver`, `AuditoriaObserver` | Notify status changes to several modules |
| **Facade** | `CheckoutFacade` | One simple entry point that orchestrates the whole checkout |

UML diagram: [Google Drive](https://drive.google.com/file/d/1Fl7Vb_uyqHzhXtvGhuZUbSdFqDyIgFaE/view?usp=sharing)

## Getting started

Requires Windows with the .NET SDK (or Visual Studio) and the .NET Framework 4.8 targeting pack.

```bash
dotnet build
bin/Debug/DeliveryGo.exe
```

Or open `DeliveryGo.sln` in Visual Studio and press F5.

## What I learned

- Designing the **order module** end to end: `Pedido`, a `PedidoBuilder` that validates before building, and an Observer-based `PedidoService`.
- Using a **Facade** to coordinate modules written by different teammates behind a single interface.
- Working as a team on one codebase with Git, splitting the work by pattern/module.

## Team

| Member | Main contributions |
| --- | --- |
| [Lorenzo Colombo](https://github.com/LoloColombo) | Shipping strategies, `EnvioService`, `ConfigManager` |
| [**Yamila Etchepareborda**](https://github.com/Yamilaetchepareborda) | `Pedido`, `PedidoBuilder`, `PedidoService` + observers, `CheckoutFacade` |
| [Emmanuel Espinosa](https://github.com/EmmanuelEspinosa) | Cart, commands with undo/redo, `CarritoPort` |
| [Iñaki Velo](https://github.com/kakovelo) | Payments: concrete methods, `PagoFactory`, `PagoAdapterMp`, decorators |

## Next steps

- Product catalog and product details when adding to the cart
- Graphical user interface
