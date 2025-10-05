using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DeliveryGo.Interfaces;

namespace DeliveryGo.Core.Payment.Metodos
{
    internal class PagoMp : IPago
    {
        public string Nombre => "MercadoPago";

        public bool Procesar(decimal monto)
        {
            if (monto <= 0m)
            {
                return false;
            }
            else
            {
                return true;
            }
               

           
            
        }
    }
}
