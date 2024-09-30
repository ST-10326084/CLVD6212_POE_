using Azure;
using Azure.Data.Tables;
using Azure.Storage.Blobs;
using Azure.Storage.Files.Shares;
using Azure.Storage.Queues;
using Azure.Storage.Queues.Models;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
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


        // READ SECTION
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

        public async Task<List<QueueMessage>> GetQueueMessagesAsync(string queueName)
        {
            var queueClient = _queueServiceClient.GetQueueClient(queueName);
            var messages = new List<QueueMessage>();
            var receivedMessages = await queueClient.ReceiveMessagesAsync(maxMessages: 10);
            messages.AddRange(receivedMessages.Value);
            return messages;
        }


        public async Task<List<CustomerTableEntity>> GetTableEntitiesAsync()
        {
            var tableClient = _tableServiceClient.GetTableClient(_tableName);
            var entities = new List<CustomerTableEntity>();

            // QueryAsync can now use CustomerTableEntity directly since it implements ITableEntity
            await foreach (CustomerTableEntity entity in tableClient.QueryAsync<CustomerTableEntity>())
            {
                entities.Add(entity);
            }

            return entities;
        }

        // WRITE SECTION

        // Blob upload
        public async Task UploadBlobAsync(string fileName, Stream fileStream)
            {
                var containerClient = _blobServiceClient.GetBlobContainerClient(_blobContainerName);
                var blobClient = containerClient.GetBlobClient(fileName);
                await blobClient.UploadAsync(fileStream);
            }

            // Add file to File Storage
            public async Task UploadFileAsync(string shareName, string directoryName, string fileName, Stream fileStream)
            {
                var shareClient = _fileServiceClient.GetShareClient(shareName);
                var directoryClient = shareClient.GetDirectoryClient(directoryName);
                var fileClient = directoryClient.GetFileClient(fileName);
                await fileClient.CreateAsync(fileStream.Length);
                await fileClient.UploadRangeAsync(new HttpRange(0, fileStream.Length), fileStream);
            }

            // Add message to Queue Storage
            public async Task AddMessageToQueueAsync(string queueName, string message)
            {
                var queueClient = _queueServiceClient.GetQueueClient(queueName);
                await queueClient.SendMessageAsync(message);
            }

            // Insert entity into Table Storage
            public async Task InsertTableEntityAsync(CustomerTableEntity entity)
            {
                var tableClient = _tableServiceClient.GetTableClient(_tableName);
                await tableClient.AddEntityAsync(entity);
            }
        }

    }


