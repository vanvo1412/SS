using System;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;

namespace OrderPaidFunction
{
    public static class ProcessDoneAlteration
    {
        [FunctionName("ProcessDoneAlteration")]
        public static void Run([ServiceBusTrigger("orderpaid", "alterationfinished", Connection = "ConnectionString")]string mySbMsg, ILogger log)
        {
            log.LogInformation($"C# ServiceBus topic trigger function ProcessDoneAlteration to processed message: {mySbMsg}");
        }
    }
}
