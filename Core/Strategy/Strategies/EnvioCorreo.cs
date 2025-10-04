using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DeliveryGo.Interfaces;
using DeliveryGo.Core.Singlenton;

namespace DeliveryGo.Core.Strategy.Strategies
{
    public class EnvioCorreo : IEnvioStrategy
    {
        public string Nombre => "Correo";

        public decimal Calcular(decimal subtotal)
        {
            if (subtotal >= ConfigManager.Instance.EnvioGratisDesde)
            {
                return 0m;
            }
            else
            {
                return 3500m;
            }
        }
    }
}
