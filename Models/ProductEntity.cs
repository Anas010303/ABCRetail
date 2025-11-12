using Azure;
using Azure.Data.Tables;

namespace ABC_Retail2.Models
{
    public class ProductEntity : ITableEntity
    {
        public string PartitionKey { get; set; } = "Products";
        public string RowKey { get; set; } // Unique ID
        public string Name { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }

        // New properties to fix errors
        public int StockLevel { get; set; }
        public string BlobImageUrl { get; set; }

        public DateTimeOffset? Timestamp { get; set; }
        public ETag ETag { get; set; }
    }
}
