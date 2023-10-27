using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Azure.Data.Tables;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.WindowsAzure.Storage.Table;
using System.Linq;

namespace ConfigAPI
{
    public static class GetConfigurations
    {
        [FunctionName("GetConfigurations")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = "configurations")] HttpRequest req,
            ILogger log)
        {
            var connectionString = Environment.GetEnvironmentVariable("AzureTableStorageConnectionString");
            var tableClient = new TableClient(connectionString, "pollingconf");
            log.LogInformation("C# HTTP trigger function processed a request.");

            var configurations = new List<ConfigurationDTO>();
            await foreach (ConfigurationEntity entity in tableClient.QueryAsync<ConfigurationEntity>())
            {
                var dto = new ConfigurationDTO
                {
                    PartitionKey = entity.PartitionKey,
                    RowKey = entity.RowKey,
                    Timestamp = entity.Timestamp,
                    PollingFrequency = entity.PollingFrequency,
                    DownstreamServices = string.IsNullOrEmpty(entity.DownstreamServices) ? new List<string>() : entity.DownstreamServices.Split(",").Select(s => s.Trim()).ToList()
                };
                configurations.Add(dto);
            }
            return new OkObjectResult(configurations);
        }
    }
}
