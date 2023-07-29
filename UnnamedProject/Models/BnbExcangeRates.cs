using System;
using System.Threading.Tasks;
using Azure;
using Azure.Data.Tables;

namespace UnnamedProject
{
    public class BnbExcangeRates : ITableEntity
    {
        public DateTime ReportDate { get; set; }
        public string CurrencyName { get; set; }
        public string CurrencyCode { get; set; }
        public int Ratio { get; set; }
        public decimal ReverseRate { get; set; }
        public decimal Rate { get; set; }

        public string PartitionKey { get; set; }
        public string RowKey { get; set; }
        public DateTimeOffset? Timestamp { get; set; }
        public ETag ETag { get; set; }
    }
}