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
                Blobs = imageUrls,
                Files = files,
                QueueMessages = messages,
                TableEntities = entities 
            };

            return View(viewModel);
        }

        // product page called Images
        public async Task<IActionResult> Images()
        {
            var imageUrls = await _azureStorageService.GetBlobUrlsAsync();
            var products = new List<Product>
    {
        //  these are hard coded values for now, and should be pulled from a DB, but as its not a requirement of the rubrik ill try get to it later. 
        new Product
                {
                    Name = "Classic Tee",
                    Description = "Simple yet stylish.",
                    Price = 19.99M,
                    ImageUrl = "~/images/product1.jpg",
                    Stock = 10
                },
                new Product
                {
                    Name = "Denim Jacket",
                    Description = "Perfect for any season.",
                    Price = 49.99M,
                    ImageUrl = "~/images/product2.jpg",
                    Stock = 5
                },
                new Product
                {
                    Name = "Leather Shoes",
                    Description = "Comfortable and durable.",
                    Price = 79.99M,
                    ImageUrl = "~/images/product3.jpg",
                    Stock = 15
                },
                new Product
                {
                    Name = "Casual Shirt",
                    Description = "Perfect for everyday wear.",
                    Price = 29.99M,
                    ImageUrl = "~/images/product4.jpg",
                    Stock = 20
                },
                new Product
                {
                    Name = "Test",
                    Description = "Perfect for everyday wear.",
                    Price = 29.99M,
                    ImageUrl = "~/images/product4.jpg",
                    Stock = 20
                },
                new Product
                {
                    Name = "Test2",
                    Description = "Perfect for everyday wear.",
                    Price = 29.99M,
                    ImageUrl = "",
                    Stock = 20
                },
                new Product
                {
                    Name = "Sneakers",
                    Description = "Stylish and versatile.",
                    Price = 59.99M,
                    ImageUrl = "~/images/product5.jpg",
                    Stock = 8
                }
    };

            var viewModel = new ProductViewModel
            {
                Products = products,
                ImageUrls = imageUrls
            };

            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> UploadBlob(IFormFile file)
        {
            if (file != null && file.Length > 0)
            {
                using (var stream = file.OpenReadStream())
                {
                    await _azureStorageService.UploadBlobAsync(file.FileName, stream);
                }
            }
            return RedirectToAction("Storage");
        }

        [HttpPost]
        public async Task<IActionResult> UploadFile(IFormFile file, string shareName, string directoryName)
        {
            if (file != null && file.Length > 0)
            {
                using (var stream = file.OpenReadStream())
                {
                    await _azureStorageService.UploadFileAsync(shareName, directoryName, file.FileName, stream);
                }
            }
            return RedirectToAction("Storage");
        }

        [HttpPost]
        public async Task<IActionResult> AddQueueMessage(string message)
        {
            if (!string.IsNullOrEmpty(message))
            {
                await _azureStorageService.AddMessageToQueueAsync("orderprocessing", message);
            }
            return RedirectToAction("Storage");
        }

        [HttpPost]
        public async Task<IActionResult> InsertTableEntity(CustomerTableEntity entity)
        {
            await _azureStorageService.InsertTableEntityAsync(entity);
            return RedirectToAction("Storage");
        }

    }
}
