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

        public async Task<IActionResult> Index()
        {
            var blobs = await _azureStorageService.GetBlobNamesAsync("productimages");
            var files = await _azureStorageService.GetFileNamesAsync("contractsandlogs", "");
            var messages = await _azureStorageService.GetQueueMessagesAsync("orderprocessing");
            var entities = await _azureStorageService.GetTableEntitiesAsync("CustomerProfiles");

            var viewModel = new StorageViewModel
            {
                Blobs = blobs,
                Files = files,
                QueueMessages = messages,
                TableEntities = entities
            };

            return View(viewModel);
        }
    }

}
