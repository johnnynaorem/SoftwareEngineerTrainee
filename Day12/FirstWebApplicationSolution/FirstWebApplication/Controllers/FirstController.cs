using FirstWebApplication.Models;
using Microsoft.AspNetCore.Mvc;

namespace FirstWebApplication.Controllers
{
    public class FirstController : Controller
    {
        public IActionResult Index()
        {
            ViewData["userName"] = "Johnny";
            ViewData["customer"] = new Customer { Id = 1, Age = 22, Name = "Johnny" };

            ViewBag.customer = new Customer { Id = 2, Age = 21, Name = "Rohit" };
            return View();
        }

        public IActionResult ViewCustomer()
        {
            Customer customer = new Customer { Id = 1, Age = 22, Name = "Johnny" };
            return View(customer);
        }
    }
}
