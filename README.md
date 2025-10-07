# DeliveryGo🛵
----------------------------------------------------------------------------------------------------------------------------------------------------------------
 *Mini-ecommerce en consola desarrollado en C# como trabajo integral de Patrones de Diseño para la materia Programación II.*

  El programa ofrece a el cliente la gestion de un Mini-ecommerce.
  Utilizando las herramientas que nos brinda C# construimos diferentes estructuras y funcionalidades que nos permiten:

- Gestionar/Manejar diferentes productos(Permite agregar,eliminar,modificar y controlar el Stock).
- Gestionar productos en un "carrito"(Permite sumar,restar y eliminar productos). 
- Concretar el/los pagos(Permite pagos con tarjeta,transferencia bancaria y billeteras virtuales).
- Gestionar la logistica del envio(Permite envio en moto, por correo y retiro en el local ,ademas de seguimiento del mismo).

   Por todo lo dicho anteriormente y mucho mas consideramos que este software es ideal para todas aquellas empresas pequeñas/medianas dedicadas a la comercializacion de uno o mas producto que encuentran oportuno migrar hacia el entorno digital,automatizando asi sus ventas.

  ## Como usar la aplicacion🤔
1. Clonar o descargar el repositorio.
2. Abrir el proyecto en Visual Studio / Rider / VS Code con .NET SDK.
3. Compilar el proyecto.
4. Ejecutar en consola (o ejecutar el debug del IDE): dotnet run
5. Seguir las opciones del menú (agregar ítems, elegir envío, pagar, confirmar pedido).

  ## Integrantes del equipo🚹🚺
   - ### *[Lorenzo Colombo](https://github.com/LoloColombo)*
         - Implementó las estrategias de envío (EnvioMoto, EnvioCorreo y RetiroEnTienda)
         - Configuró el ConfigManager, encargado de almacenar valores globales como el IVA y el umbral para envío gratis
         - Desarrolló la clase EnvioService, que permite cambiar dinámicamente la estrategia de envío y calcular el costo total según la opción elegida 
   - ### *[Yamila Etchepareborda](https://github.com/Yamilaetchepareborda)*
         - Implemento la clase Pedido
         - Desarrolló el PedidoBuilder para construir pedidos paso a paso y validar los datos antes de crearlos.
         - Implemento PedidoService y sus observadores (ClienteObserver,LogisticaObserver,AuditoriaObserver) para notificar los cambios de estado del pedido.
         - Cordino todo en la CheckoutFacade(módulos de carrito, envío, pago y pedido)
   - ### *[Emmanuel Espinosa](https://github.com/EmmanuelEspinosa)*
         - Desarrolló la clase carrito,con comandos de agregar, quitar y modificar ítems.
         - Creó las clases AgregarItemCommand, QuitarItemCommand, SetCantidadCommand y EditorCarrito que sirven como comandos y historial.
         - Implementó la clase CarritoPort, que actúa como adaptador del carrito principal y expone los métodos públicos. 
   - ### *[Inaki Velo](https://github.com/kakovelo)*
         - Implementó el sistema de pagos.
         - Desarrolló las clases de pago concretas (PagoTarjeta, PagoTransfer y PagoMp) junto con la PagoFactory, encargada de instanciar el tipo de pago.
         - Creó el PagoAdapterMp, que adapta una SDK externa (MpSdkFalsa) al formato del sistema, permitiendo integrar un método de pago externo.
         - Implementó los decoradores PagoConImpuesto y PagoConCupon, que agregan funcionalidad adicional al pago base.
  
  ## Patrones aplicados🧩
  
 ### Command + Undo/Redo 🎮 

  Encapsula acciones como objetos y permite deshacer/rehacer sin acoplar UI ↔ lógica.

**Implementado en:**
- AgregarItemCommand, QuitarItemCommand, SetCantidadCommand (implementan ICommand)
- EditorCarrito (invoker con pilas _undo / _redo)
- Carrito (receiver de altas/bajas/modificaciones)
- CarritoPort / ICarritoPort (puerto seguro para ejecutar comandos)	


 ### Strategy (envíos)♟️ 

  Selecciona el algoritmo de cálculo de envío en runtime.

**Implementado en:**
- IEnvioStrategy
- Estrategias: EnvioMoto, EnvioCorreo, RetiroEnTienda
- EnvioService (contexto: SetStrategy, Calcular())


 ### Factory (pagos) 🏭 

  Centraliza la creación de métodos de pago sin if/switch esparcidos.

**Implementado en:**
- PagoFactory.Create(tipo)
- Productos: PagoTarjeta, PagoMp, PagoTransferencia (todos IPago)


 ### Adapter (SDK de pago) 🔌 

  Adapta una API/SDK externa a nuestra interfaz de pagos.

**Implementado en:**
- PagoAdapterMp : IPago
- Adapta MpSdkFalsa (u otra SDK) al contrato IPago


 ### Decorator (impuestos y cupones) 🎀 

  Agrega responsabilidades al pago (IVA/descuento) envolviendo objetos.

**Implementado en:**
- PagoConImpuesto : IPago
- PagoConCupon : IPago
- Composición: decoran un IPago base (p. ej., PagoMp)


 ### Singleton (configuración)1️⃣ 

  Unica instancia compartida de parámetros globales.

**Implementado en:**
- ConfigManager.Instance (p. ej., IVA, EnvioGratisDesde)
- Consumido por Strategy y Decorators de pago


 ### Builder (pedido) 🏗️ 

  Construye Pedido paso a paso con validaciones previas a Build().

 **Implementado en:**

- IPedidoBuilder, PedidoBuilder (métodos ConItems(...), ConDireccion(...), ConMetodoPago(...), Build())
- Validaciones internas antes de crear el objeto final 


 ### Observer (estado del pedido) 👀 

  Desacopla la notificación de cambios de estado a múltiples interesados.

 **Implementado en:**

- PedidoService (evento/notify en cambios: Recibido → ... → Entregado)
- Observers: ClienteObserver, LogisticaObserver, AuditoriaObserver


 ### Facade (checkout) 🚪 

  Orquesta el flujo de compra detrás de una interfaz simple.

 **Implementado en:**

- CheckoutFacade (coordina carrito/Command, envío/Strategy, pago/Factory+Adapter+Decorator, armado/Builder y notificaciones/Observer)

   ## Caso narrado de uso📖
  1. El usuario agrega varios productos al carrito.
  2. Se equivoca y elimina un producto del carrito.
  3. Cambia la cantidad de un producto ya agregado.
  4. Selecciona ver carrito para verificar que todos los productos se haya agregado bien.
  5. Calcula el precio total de la compra con envio incluido.
  6. Elige un metodo de pago y realiza la compra.
  7. Confirma el pedido agregando a direccion de entrega.

  ## [UML](https://drive.google.com/file/d/1Fl7Vb_uyqHzhXtvGhuZUbSdFqDyIgFaE/view?usp=sharing)↔️
    *Link hacia el UML del proyecto.*

   ## Retos Futuros🔜
    Dentro de un futuro estamos interesados en agregar distintas funcionalidades que completen ciertas falencias del programa en su estado actual.
    Algunas de estas son:
    
    - Catalogo de productos.
    - Detalles del producto al momento de agregar al carrito.
    - Integracion de una interfaz grafica para mejorar la UI y UE.
  



