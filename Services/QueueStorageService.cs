using Azure.Storage.Queues;
using System.Threading.Tasks;

namespace ABC_Retail2.Services
{
    public class QueueStorageService
    {
        private readonly QueueClient _queueClient;

        public QueueStorageService(string connectionString, string queueName)
        {
            _queueClient = new QueueClient(connectionString, queueName);
            _queueClient.CreateIfNotExists();
        }

        public async Task SendMessageAsync(string message)
        {
            await _queueClient.SendMessageAsync(message);
        }

        public async Task<string[]> PeekMessagesAsync(int maxMessages = 10)
        {
            var messages = await _queueClient.ReceiveMessagesAsync(maxMessages);
            var list = new List<string>();
            foreach (var msg in messages.Value)
                list.Add(msg.MessageText);
            return list.ToArray();
        }
    }
}
