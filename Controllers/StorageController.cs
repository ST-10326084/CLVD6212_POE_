using Microsoft.AspNetCore.Mvc;
using testCLVD.Services;
using testCLVD.Models;

namespace testCLVD.Controllers
{
    public class StorageController : Controller
    {
        private readonly AzureStorageService _azureStorageService;

        public StorageController(AzureStorageService azureStorageService)
        {
            _azureStorageService = azureStorageService;
        }

        public async Task<IActionResult> Storage()
        {
            var imageUrls = await _azureStorageService.GetBlobUrlsAsync();
            var files = await _azureStorageService.GetFileNamesAsync("contractsandlogs", "");
            var messages = await _azureStorageService.GetQueueMessagesAsync("orderprocessing");
            var entities = await _azureStorageService.GetTableEntitiesAsync();

            var viewModel = new StorageViewModel
            {
                Blobs = imageUrls, // This should now be URLs of the images
                Files = files,
                QueueMessages = messages,
                TableEntities = entities 
            };

            return View(viewModel);
        }

        public async Task<IActionResult> Images()
        {
            var imageUrls = await _azureStorageService.GetBlobUrlsAsync();
            var viewModel = new StorageViewModel
            {
                Blobs = imageUrls
            };

            return View(viewModel);
        }

    }
}
