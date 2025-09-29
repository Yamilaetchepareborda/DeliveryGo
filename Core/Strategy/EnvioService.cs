using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DeliveryGo.Interfaces;

namespace DeliveryGo.Core.Strategy
{
    public class EnvioService
    {
        private IEnvioStrategy _actual;

        public EnvioService(IEnvioStrategy envioInicial)
        {
            this._actual = envioInicial;
        }

        public void SetStrategy(IEnvioStrategy s)
        {
            _actual = s;
        }

        public decimal Calcular(decimal subtotal)
        {
            return _actual.Calcular(subtotal);
        }

        public string NombreActual => _actual.Nombre;

    }
}
