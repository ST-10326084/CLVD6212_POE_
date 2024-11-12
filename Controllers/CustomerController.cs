using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using testCLVD.DB_Models;
using testCLVD.Services;

namespace testCLVD.Controllers
{
    public class CustomerController : Controller
    {
        private readonly DatabaseService _databaseService;

        public CustomerController(DatabaseService databaseService)
        {
            _databaseService = databaseService;
        }

        [HttpGet]
        public IActionResult RegCustomer()
        {
            return View("~/Views/DB_Account/RegCustomer.cshtml");
        }

        [HttpPost]
        public async Task<IActionResult> RegCustomer(Customer customer)
        {
            if (!ModelState.IsValid)
            {
                return View("~/Views/DB_Account/RegCustomer.cshtml", customer);
            }

            await _databaseService.RegCustomerAsync(customer);
            ViewBag.Message = "Customer added successfully!";
            return View("~/Views/DB_Account/RegCustomer.cshtml");
        }

    }
}
