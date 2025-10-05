using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DeliveryGo.Interfaces;

namespace DeliveryGo.Core.Payment.Adapters
{
    internal class PagoAdapterMp : IPago
    {
        private readonly MpSdkFalsa _sdk;

        public PagoAdapterMp(MpSdkFalsa sdk)
        {
            _sdk = sdk;
        }

        public string Nombre => "MercadoPagoAdapter";
        
        public bool Procesar(decimal monto)
        {
            // Delegamos en la SDK externa
            return _sdk.Cobrar(monto);
        }
    }
}
