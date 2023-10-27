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
    public static class UpdateConfiguration
    {
        [FunctionName("UpdateConfiguration")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "put", Route = "configurations/{id}")] HttpRequest req,
            ILogger log, string id)
        {
            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            var updated = JsonConvert.DeserializeObject<ConfigurationEntity>(requestBody);

            var connectionString = Environment.GetEnvironmentVariable("AzureTableStorageConnectionString");
            var tableClient = new TableClient(connectionString, "pollingconf");

            var entity = await tableClient.GetEntityAsync<ConfigurationEntity>("PollingConfig", id);

            if (entity == null)
            {
                return new NotFoundResult();
            }

            entity.Value.PollingFrequency = updated.PollingFrequency;
            entity.Value.DownstreamServices = string.Join(",", updated.GetDownstreamServicesList);

            await tableClient.UpdateEntityAsync(entity.Value, ETag.All);

            return new OkObjectResult(entity.Value);
        }
    }
}
