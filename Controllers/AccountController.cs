using Microsoft.AspNetCore.Mvc;

namespace testCLVD.Controllers
{
    public class AccountController : Controller
    {
        public IActionResult Login()
        {
            return View();
        }

        public IActionResult Signup()
        {
            return View();
        }

        public IActionResult Profile()
        {
            return View();
        }
    }
}
