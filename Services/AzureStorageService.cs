using Azure.Data.Tables;
using Azure.Storage.Blobs;
using Azure.Storage.Files.Shares;
using Azure.Storage.Queues;
using Microsoft.Extensions.Options;
using testCLVD.Models;
using testCLVD.Settings;

namespace testCLVD.Services
{
    public class AzureStorageService
    {
        private readonly BlobServiceClient _blobServiceClient;
        private readonly string _blobContainerName;

        private readonly ShareServiceClient _fileServiceClient;

        private readonly QueueServiceClient _queueServiceClient;

        private readonly TableServiceClient _tableServiceClient;
        private readonly string _tableName;

        public AzureStorageService(IOptions<AzureStorageSettings> options)
        {
            var settings = options.Value;
            _blobServiceClient = new BlobServiceClient(settings.ConnectionString);
            _blobContainerName = settings.BlobContainerName;

            _fileServiceClient = new ShareServiceClient(settings.ConnectionString);


            _queueServiceClient = new QueueServiceClient(settings.ConnectionString);


            _tableServiceClient = new TableServiceClient(settings.ConnectionString);
            _tableName = settings.TableName;
        }

        public async Task<List<string>> GetBlobUrlsAsync()
        {
            var containerClient = _blobServiceClient.GetBlobContainerClient(_blobContainerName);
            var blobUrls = new List<string>();

            await foreach (var blobItem in containerClient.GetBlobsAsync())
            {
                var blobClient = containerClient.GetBlobClient(blobItem.Name);
                blobUrls.Add(blobClient.Uri.ToString());
            }

            return blobUrls;
        }



        public async Task<List<string>> GetFileNamesAsync(string shareName, string directoryName)
        {
            var shareClient = _fileServiceClient.GetShareClient(shareName);
            var directoryClient = shareClient.GetDirectoryClient(directoryName);
            var files = new List<string>();
            await foreach (var fileItem in directoryClient.GetFilesAndDirectoriesAsync())
            {
                files.Add(fileItem.Name);
            }
            return files;
        }

        public async Task<List<string>> GetQueueMessagesAsync(string queueName)
        {
            var queueClient = _queueServiceClient.GetQueueClient(queueName);
            var messages = new List<string>();
            var receivedMessages = await queueClient.ReceiveMessagesAsync(maxMessages: 10);
            foreach (var message in receivedMessages.Value)
            {
                messages.Add(message.MessageText);
            }
            return messages;
        }

        // Inside your AzureStorageService class
        public async Task<List<CustomerTableEntity>> GetTableEntitiesAsync()
        {
            var tableClient = _tableServiceClient.GetTableClient(_tableName);
            var entities = new List<CustomerTableEntity>();

            await foreach (var entity in tableClient.QueryAsync<TableEntity>())
            {
                var customerEntity = new CustomerTableEntity
                {
                    PartitionKey = entity.PartitionKey,
                    RowKey = entity.RowKey,
                    Timestamp = entity.Timestamp.ToString(),
                    CustomerName = entity.GetPropertyValue<string>("CustomerName"),
                    Email = entity.GetPropertyValue<string>("Email"),
                    PhoneNumber = entity.GetPropertyValue<string>("PhoneNumber")
                };
                entities.Add(customerEntity);
            }

            return entities;
        }


    }

}
