using Microsoft.AspNetCore.Mvc;
using testCLVD.Models;
using System.Collections.Generic;
using static System.Net.WebRequestMethods;

namespace testCLVD.Controllers
{
    public class ProductController : Controller
    {

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

        public IActionResult Cart() // will implement code for working cart for part 3
        {
            return View();
        }
    }
}
