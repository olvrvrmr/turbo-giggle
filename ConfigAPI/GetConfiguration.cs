using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Azure.Data.Tables;
using Azure;
using System.Linq;

namespace ConfigAPI
{
    public static class GetConfiguration
    {
        [FunctionName("GetConfiguration")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get",Route = "configurations/{partitionKey}/{id}")] HttpRequest req, string partitionKey, string id, ILogger log)
        {
            var connectionString = Environment.GetEnvironmentVariable("AzureTableStorageConnectionString");
            var tableClient = new TableClient(connectionString, "pollingconf");
            log.LogInformation("C# HTTP trigger function processed a request.");
            try
            {
                var response = tableClient.GetEntity<ConfigurationEntity>(partitionKey, id);
                var entity = response.Value;

                entity.GetDownstreamServicesList = entity.DownstreamServices.Split(",").Select(s => s.Trim()).ToList();

                return new OkObjectResult(entity);

            }
            catch (RequestFailedException e) when (e.Status == 404)
            {
                return new NotFoundResult();
            }
            catch (Exception e)
            {
                log.LogError($"An error occurred: {e.Message}");
                return new StatusCodeResult(500);
            }
        }
    }
}
