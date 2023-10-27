using System;
using System.Collections.Generic;

namespace ConfigAPI
{
    public class ConfigurationDTO
    {
        public string PartitionKey { get; set; }
        public string RowKey { get; set; }
        public DateTimeOffset? Timestamp { get; set; }
        public int PollingFrequency { get; set; }
        public List<string> DownstreamServices { get; set; }
    }
}