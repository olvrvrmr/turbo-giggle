using System;
using System.Collections.Generic;
using System.Linq;
using Azure;
using Azure.Data.Tables;

namespace ConfigAPI
{
    public class ConfigurationEntity : ITableEntity
    {
        public string PartitionKey { get; set; } = "default";
        public string RowKey { get; set; }
        public DateTimeOffset? Timestamp { get; set; }
        public ETag ETag { get; set; }

        public int PollingFrequency { get; set; }
        public string DownstreamServices { get; set; }
        public List<string> GetDownstreamServicesList { get; set; }
    }
}