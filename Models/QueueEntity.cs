using System;

namespace ABC_Retail2.Models
{
    public class QueueEntity
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string MessageType { get; set; }  // e.g., "Order", "CustomerUpdate", etc.
        public string Description { get; set; }  // Message content or summary
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
