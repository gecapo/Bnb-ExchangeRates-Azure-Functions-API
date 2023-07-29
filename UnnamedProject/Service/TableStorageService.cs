using System;
using System.Threading.Tasks;
using Azure.Data.Tables;
using Microsoft.Extensions.Configuration;

namespace UnnamedProject
{
    public class TableStorageService : ITableStorageService
    {
        private const string TableName = "ExchangeRate";
        private readonly IConfiguration _configuration;

        public TableStorageService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        private async Task<TableClient> GetTableClient()
        {
            var serviceClient = new TableServiceClient(_configuration["StorageConnectionString"]);
            var tableClient = serviceClient.GetTableClient(TableName);
            await tableClient.CreateIfNotExistsAsync();
            return tableClient;
        }

        public async Task<BnbExcangeRates> GetEntityAsync(DateTime date, string code)
        {
            var tableClient = await GetTableClient();
            return await tableClient.GetEntityAsync<BnbExcangeRates>(date.ToString(), code);
        }

        public async Task<BnbExcangeRates> UpsertEntityAsync(BnbExcangeRates entity)
        {
            var tableClient = await GetTableClient();
            await tableClient.UpsertEntityAsync(entity);
            return entity;
        }

        public async Task DeleteEntityAsync(DateTime date, string code)
        {
            var tableClient = await GetTableClient();
            await tableClient.DeleteEntityAsync(date.ToString(), code);
        }
    }
}