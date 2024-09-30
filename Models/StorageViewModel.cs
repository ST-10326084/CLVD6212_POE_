using Azure.Data.Tables;
using Azure.Storage.Queues.Models;

namespace testCLVD.Models
{
    public class StorageViewModel
    {
        public List<string> Blobs { get; set; }
        public List<string> Files { get; set; }
        public List<QueueMessage> QueueMessages { get; set; }
        public List<CustomerTableEntity> TableEntities { get; set; } 
    }


}
