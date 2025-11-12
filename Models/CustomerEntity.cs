using Azure;
using Azure.Data.Tables;

namespace ABC_Retail2.Models
{
    public class CustomerEntity : ITableEntity
    {
        public string PartitionKey { get; set; } = "Customers";
        public string RowKey { get; set; } // Unique ID
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }

        // New properties to fix Index.cshtml
        public string Phone { get; set; }
        public string Address { get; set; }

        // Computed property for FullName
        public string FullName => $"{FirstName} {LastName}";

        public DateTimeOffset? Timestamp { get; set; }
        public ETag ETag { get; set; }
    }
}
