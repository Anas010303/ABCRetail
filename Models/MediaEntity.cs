using Azure;
using Azure.Data.Tables;
using System;

namespace ABC_Retail2.Models
{
    public class MediaEntity : ITableEntity
    {
        public string PartitionKey { get; set; } = "Media";
        public string RowKey { get; set; } = Guid.NewGuid().ToString();

        // Custom properties
        public string FileName { get; set; }          // Name of the file (e.g., image1.jpg)
        public string BlobUrl { get; set; }           // Public URL to access blob
        public string ContentType { get; set; }       // e.g., image/png, video/mp4
        public long FileSize { get; set; }            // Size in bytes
        public DateTime UploadedOn { get; set; } = DateTime.UtcNow;

        // Required by ITableEntity
        public ETag ETag { get; set; }
        public DateTimeOffset? Timestamp { get; set; }
    }
}
