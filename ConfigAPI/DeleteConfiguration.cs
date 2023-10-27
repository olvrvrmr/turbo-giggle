using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Azure.Data.Tables;
using Newtonsoft.Json;
using Azure;

namespace ConfigAPI
{
    public static class DeleteConfiguration
    {
        [FunctionName("DeleteConfiguration")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "delete", Route = "configurations/{id}")] HttpRequest req,
            ILogger log, string id)
        {
            var connectionString = Environment.GetEnvironmentVariable("AzureTableStorageConnectionString");
            var tableClient = new TableClient(connectionString, "pollingconf");

            var entity = await tableClient.GetEntityAsync<ConfigurationEntity>("PollingConfig", id);

            if (entity == null)
            {
                return new NotFoundResult();
            }

            await tableClient.DeleteEntityAsync(entity.Value.PartitionKey, entity.Value.RowKey);

            return new OkResult();
        }
    }
}
