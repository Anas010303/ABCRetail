using Azure;
using Azure.Data.Tables;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ABC_Retail2.Services
{
    public class TableStorageService
    {
        private readonly TableServiceClient _serviceClient;

        public TableStorageService(string connectionString)
        {
            _serviceClient = new TableServiceClient(connectionString);
        }

        public TableClient GetTableClient(string tableName)
        {
            var tableClient = _serviceClient.GetTableClient(tableName);
            tableClient.CreateIfNotExists();
            return tableClient;
        }

        // Generic methods
        public async Task AddEntityAsync<T>(string tableName, T entity) where T : class, ITableEntity, new()
        {
            var tableClient = GetTableClient(tableName);
            await tableClient.AddEntityAsync(entity);
        }

        public async Task<List<T>> GetEntitiesAsync<T>(string tableName) where T : class, ITableEntity, new()
        {
            var tableClient = GetTableClient(tableName);
            var list = new List<T>();
            await foreach (var entity in tableClient.QueryAsync<T>())
            {
                list.Add(entity);
            }
            return list;
        }

        // Customer-specific methods
        public async Task AddOrUpdateCustomerAsync(Models.CustomerEntity customer)
        {
            var tableClient = GetTableClient("Customers");
            await tableClient.UpsertEntityAsync(customer);
        }

        public async Task<List<Models.CustomerEntity>> GetAllCustomersAsync()
        {
            var tableClient = GetTableClient("Customers");
            var list = new List<Models.CustomerEntity>();
            await foreach (var c in tableClient.QueryAsync<Models.CustomerEntity>())
                list.Add(c);
            return list;
        }

        public async Task DeleteCustomerAsync(string rowKey)
        {
            var tableClient = GetTableClient("Customers");
            await tableClient.DeleteEntityAsync("Customers", rowKey);
        }
    }
}
