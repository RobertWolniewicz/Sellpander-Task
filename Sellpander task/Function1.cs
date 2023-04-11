using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;
using Sellpander_task.BaseLinker;
using Sellpander_task.Faire;
using Sellpander_task.Services;

namespace Sellpander_task;

public class Transfer
{
    [FunctionName("Transfer")]
    public void Run([TimerTrigger("0 */10 * * * *")] TimerInfo myTimer, ILogger log)
    {
        log.LogInformation($"C# Timer trigger function executed at: {DateTime.Now}");
        string faireAccessToken= Environment.GetEnvironmentVariable("X-FAIRE-ACCESS-TOKEN");
        string baselinkerToken= Environment.GetEnvironmentVariable("X-BLToken");

        try
        {
            var faireService = new FaireService(faireAccessToken);
            var faireOrders = faireService.GetFaireOrders();
            var baselinkerService = new BaselinkerService(baselinkerToken);
            var existingOrders = baselinkerService.GetBaselinkerOrders();
            var newOrders = faireOrders.Where(order =>
                !existingOrders.Any(existingOrder => existingOrder.ExtraField1 == order.Id.ToString())).ToList();
            baselinkerService.AddBaselinkerOrders(newOrders);
        }
        catch (Exception e)
        {
            log.LogError(e, e.Message);
        }
    }
}