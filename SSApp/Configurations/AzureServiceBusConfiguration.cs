using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SSApp.Configurations
{
    public class AzureServiceBusConfiguration
    {
        public string ConnectionString { get; set; }
        public string TopicName { get; set; }
    }
}
