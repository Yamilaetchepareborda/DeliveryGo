using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DeliveryGo.Interfaces;
using DeliveryGo.Core.Singlenton;

namespace DeliveryGo.Core.Payment.Decorators
{
    internal class PagoConImpuesto : IPago
    {
        private readonly IPago _inner;

        public PagoConImpuesto(IPago inner)
        {
            if (inner == null) throw new ArgumentNullException(nameof(inner));
            _inner = inner;
        }

        public string Nombre => $"{_inner.Nombre}+IVA";

        public bool Procesar(decimal monto)
        {
            if (monto <= 0m) return false;

            var iva = ConfigManager.Instance.IVA;   
            if (iva < 0m) iva = 0m;
            if (iva > 1m) iva = 1m;

            var montoConIva = monto * (1m + iva);
            return _inner.Procesar(montoConIva);
        }

    }
}
