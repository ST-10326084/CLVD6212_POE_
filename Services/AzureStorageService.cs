using Azure.Data.Tables;
using Azure.Storage.Blobs;
using Azure.Storage.Files.Shares;
using Azure.Storage.Queues;
using Microsoft.Extensions.Options;
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

        /*
        public AzureStorageService(IOptions<AzureStorageSettings> settings)
        {  
            var connectionString = settings.Value.ConnectionString;
            _blobServiceClient = new BlobServiceClient(connectionString);
            _fileServiceClient = new ShareServiceClient(connectionString);
            _queueServiceClient = new QueueServiceClient(connectionString);
            _tableServiceClient = new TableServiceClient(connectionString);
        }
*/

        public AzureStorageService(IOptions<AzureStorageSettings> options)
        {
            var settings = options.Value;
            _blobServiceClient = new BlobServiceClient(settings.ConnectionString);
            _blobContainerName = settings.BlobContainerName;
            _fileServiceClient = new ShareServiceClient(settings.ConnectionString);
            _queueServiceClient = new QueueServiceClient(settings.ConnectionString);
            _tableServiceClient = new TableServiceClient(settings.ConnectionString);
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

        public async Task<List<string>> GetTableEntitiesAsync(string tableName)
        {
            var tableClient = _tableServiceClient.GetTableClient(tableName);
            var entities = new List<string>();
            await foreach (var entity in tableClient.QueryAsync<TableEntity>())
            {
                entities.Add(entity.RowKey);
            }
            return entities;
        }
    }

}
