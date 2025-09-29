using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DeliveryGo.Interfaces;

namespace DeliveryGo.Core.Strategy.Strategies
{
    public class RetiroEnTienda : IEnvioStrategy
    {
        public string Nombre => "Retiro en tienda";

        public decimal Calcular(decimal subtotal)
        {
            return 0m;
        }
    }
}
