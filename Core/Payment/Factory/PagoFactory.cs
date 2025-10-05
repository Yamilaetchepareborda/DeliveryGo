using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DeliveryGo.Core.Payment.Metodos;
using DeliveryGo.Interfaces;
using DeliveryGo.Core.Payment.Adapters;

namespace DeliveryGo.Core.Payment
{
    internal class PagoFactory
    {
        public static IPago Create(string tipo)
        {
            if (string.IsNullOrWhiteSpace(tipo))
                throw new ArgumentException("Debe indicar un tipo de pago.", nameof(tipo));

            switch (tipo.Trim().ToLowerInvariant())
            {
                case "tarjeta":
                    return new PagoTarjeta();

                case "transferencia":
                    return new PagoTransferencia();

                case "mp":
                case "mercadopago":
                    // Adapter con SDK externa
                    return new PagoAdapterMp(new MpSdkFalsa());

                case "mp-directo":
                    // Clase concreta sin usar el Adapter (opcional, para comparar)
                    return new PagoMp();

                default:
                    throw new ArgumentException($"Tipo de pago no válido: {tipo}");
            }

        }
    }
}
