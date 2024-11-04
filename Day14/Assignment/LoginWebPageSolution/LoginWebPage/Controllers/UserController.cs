using LoginWebPage.Interface;
using LoginWebPage.Models;
using LoginWebPage.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace LoginWebPage.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(int Id, string password)
        {
            try
            {
                var user = _userService.GetUser(Id, password);
                if (user != null)
                {
                    HttpContext.Session.SetString("username", user.Name);
                    HttpContext.Session.SetString("email", user.Email);
                    return RedirectToAction("Index", "Home");
                }
                throw new InvalidUserCredentials();
            }
            catch (InvalidUserCredentials)
            {
                ModelState.AddModelError(string.Empty, "Invalid username or password");
                return View();
            }
        }


        public IActionResult UserProfile(int userId)
        {
            var user = _userService.GetUser(userId);
            return View(user);
        }
    }
}
