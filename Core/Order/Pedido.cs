using DeliveryGo.Core.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using static DeliveryGo.Core.Enum.Enums;

namespace DeliveryGo.Core.Order
{
    public class Pedido
    {
        public int Id { get; set; }
        public List<Item> Items { get; set; } = new List<Item>();
        public string Direccion { get; set; } = "";
        public string TipoPago { get; set;} = "";
        public EstadoPedido Estado { get; set;}
        public decimal Monto { get; set; }

    }
}
