using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeliveryGo.Interfaces
{
    public interface IEnvioStrategy
    {
        decimal Calcular(decimal subtotal);
        string Nombre { get; }
    }

}
