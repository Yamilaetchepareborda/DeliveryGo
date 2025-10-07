# DeliveryGo
----------------------------------------------------------------------------------------------------------------------------------------------------------------
 *Mini-ecommerce en consola desarrollado en C# como trabajo integral de Patrones de Diseño para la materia Programación II.*

  El programa ofrece a el cliente la gestion de un Mini-ecommerce.
  Utilizando las herramientas que nos brinda C#(.NET) construimos diferentes estructuras y funcionalidades que nos permiten:

- Gestionar/Manejar diferentes productos(Permite agregar,eliminar,modificar y controlar el Stock).
- Gestionar productos en un "carrito"(Permite sumar,restar y eliminar productos). 
- Concretar el/los pagos(Permite pagos con tarjeta,transferencia bancaria y billeteras virtuales).
- Gestionar la logistica del envio(Permite envio en moto, por correo y retiro en el local ,ademas de seguimiento del mismo).
  
  Por todo lo dicho anteriormente y mucho mas consideramos que este software es ideal para todas aquellas empresas pequeñas/medianas dedicadas a la comercializacion de uno o mas producto que encuentran oportuno migrar hacia el entorno digital,automatizando asi sus ventas.

  ## Como usar la aplicacion
1. Clonar o descargar el repositorio.
2. Abrir el proyecto en Visual Studio / Rider / VS Code con .NET SDK.
3. Compilar el proyecto.
4. Ejecutar en consola (o ejecutar el debug del IDE): dotnet run
5. Seguir las opciones del menú (agregar ítems, elegir envío, pagar, confirmar pedido).

  ## Integrantes del equipo
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
   - ### *[Inaki Velo](https://github.com/kakovelo)*
  ## Patrones aplicados
  ## Caso narrado de uso
  ## UML
  ## Retos Futuros
  ## Notas Finales



