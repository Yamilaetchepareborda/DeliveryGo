using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeliveryGo.Core.Payment.Adapters
{
    internal class MpSdkFalsa
    {
        public bool Cobrar(decimal monto)
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
