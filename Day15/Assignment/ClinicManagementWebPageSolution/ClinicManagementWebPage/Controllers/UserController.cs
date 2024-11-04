using ClinicManagementWebPage.Exceptions;
using ClinicManagementWebPage.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ClinicManagementWebPage.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService) { 
            _userService = userService;
        }
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(string email, string password)
        {
            try
            {
                var user = _userService.GetUser(email, password);
                if (user != null)
                {
                    HttpContext.Session.SetString("username", user.Name);
                    HttpContext.Session.SetString("email", user.Email);
                    return RedirectToAction("Index", "Doctor");
                }
                throw new InvalidUserException();
            }
            catch (InvalidUserException e)
            {
                ModelState.AddModelError(string.Empty, e.Message);
                return View();
            }
        }
    }
}
