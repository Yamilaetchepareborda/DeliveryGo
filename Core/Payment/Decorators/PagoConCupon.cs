using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DeliveryGo.Interfaces;

namespace DeliveryGo.Core.Payment.Decorators
{
    internal class PagoConCupon : IPago
    {
        private readonly IPago _inner;
        private readonly decimal _porcentaje;

        public PagoConCupon(IPago inner, decimal porcentaje)
        {
            if (inner == null) throw new ArgumentNullException(nameof(inner));
            _inner = inner;
            _porcentaje = porcentaje;
        }

        public string Nombre => $"{_inner.Nombre}+Cupon";

        public bool Procesar(decimal monto)
        {
            if (monto <= 0m) return false;
            if (_porcentaje <= 0m || _porcentaje >= 1m) return false; // porcentaje inválido

            var montoConDescuento = monto * (1m - _porcentaje);
            return _inner.Procesar(montoConDescuento);
        }

    }
}
