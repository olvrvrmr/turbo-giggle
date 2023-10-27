using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System.Text.Json;
using Azure.Data.Tables;
using Newtonsoft.Json;

namespace ConfigAPI
{
    public static class CreateConfiguration
    {
        [FunctionName("CreateConfiguration")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = "configurations")] HttpRequest req,
            ILogger log)
        {
            try
            {
                string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
                log.LogInformation(requestBody);
                var data = JsonConvert.DeserializeObject<ConfigurationEntity>(requestBody);
                log.LogInformation(data.ToString());

                var connectionString = Environment.GetEnvironmentVariable("AzureTableStorageConnectionString");
                var tableClient = new TableClient(connectionString, "pollingconf");

                var entity = new ConfigurationEntity
                {
                    PartitionKey = "PollingConfig", // or whatever partition key you're using
                    RowKey = data.RowKey,
                    PollingFrequency = data.PollingFrequency,
                    DownstreamServices = data.DownstreamServices is string
                        ? data.DownstreamServices
                        : string.Join(",", data.DownstreamServices)
                };

                await tableClient.AddEntityAsync(entity);

                return new OkObjectResult(entity);
            }
            catch (Exception ex)
            {
                log.LogError($"An error occurred: {ex.Message}");
                return new StatusCodeResult(500);
            }
        }
    }
}
