using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeliveryGo.Interfaces
{
    public interface IPago
    {
        string Nombre { get; }
        bool Procesar(decimal monto);
    }

}
