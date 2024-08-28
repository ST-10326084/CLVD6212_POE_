using Microsoft.AspNetCore.Mvc;
using testCLVD.Models;
using System.Collections.Generic;

namespace testCLVD.Controllers
{
    public class ProductController : Controller
    {
        public IActionResult Product()
        {
            var products = new List<Product>
            {
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
                    Name = "Sneakers",
                    Description = "Stylish and versatile.",
                    Price = 59.99M,
                    ImageUrl = "~/images/product5.jpg",
                    Stock = 8
                }
            };

            return View(products);
        }

        public IActionResult PrevOrders()
        {
            var orders = new List<Order>
            {
                new Order { OrderId = "12345", OrderDate = DateTime.Now.AddDays(-10), TotalAmount = 99.99M, Status = "Delivered" },
                new Order { OrderId = "12346", OrderDate = DateTime.Now.AddDays(-5), TotalAmount = 149.99M, Status = "Shipped" },
                new Order { OrderId = "12347", OrderDate = DateTime.Now.AddDays(-2), TotalAmount = 199.99M, Status = "Processing" }
            };

            return View(orders);
        }

        public IActionResult Cart()
        {
            return View();
        }
    }
}
