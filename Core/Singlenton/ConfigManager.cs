using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeliveryGo.Core.Singlenton
{
    public sealed class ConfigManager
    {
        private static readonly Lazy<ConfigManager> _inst = new Lazy<ConfigManager>(() => new ConfigManager());
        public static ConfigManager Instance => _inst.Value;
        private ConfigManager() { }

        public decimal EnvioGratisDesde { get; set; } = 50000m;
        public decimal IVA { get; set; } = 0.21m; // usado por Decorator en Etapa 3
    }

}
