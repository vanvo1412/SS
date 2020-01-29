using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SSApp.Configurations
{
    public class ApplicationConfiguration
    {
        public LoggingConfiguration Logging { get; set; }
        public string AllowedHosts { get; set; }
        public AzureServiceBusConfiguration AzureServiceBusConfiguration { get; set; }
    }
}
