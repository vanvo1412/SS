using System;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;

namespace OrderPaidFunction
{
    public static class ProcessPaidOrder
    {
        [FunctionName("ProcessPaidOrder")]
        public static void Run([ServiceBusTrigger("orderpaid", "orderpaid", Connection = "ConnectionString")]string mySbMsg, ILogger log)
        {
            log.LogInformation($"C# ServiceBus topic trigger function ProcessPaidOrder to processed message: {mySbMsg}");
        }
    }
}
