using Azure.Data.Tables;

namespace testCLVD.Models
{
    public class CustomerTableEntity
    {
        public string PartitionKey { get; set; }
        public string RowKey { get; set; }
        public string Timestamp { get; set; }
        public string CustomerName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
    }
}
