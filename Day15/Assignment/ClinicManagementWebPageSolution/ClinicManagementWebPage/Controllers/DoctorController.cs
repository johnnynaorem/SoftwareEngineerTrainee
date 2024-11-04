using ClinicManagementWebPage.Interfaces;
using ClinicManagementWebPage.Models;
using ClinicManagementWebPage.Repositories;
using Microsoft.AspNetCore.Mvc;


namespace ClinicManagementWebPage.Controllers
{
    public class DoctorController : Controller
    {
        private readonly IDoctorService _doctorService;

        public DoctorController(IDoctorService doctorService)
        {
            _doctorService = doctorService;
        }
        public IActionResult Index()
        {
            var username = HttpContext.Session.GetString("username");
            var email = HttpContext.Session.GetString("email");
            if (username == null)
            {
                return RedirectToAction("Login", "User");
            }
            ViewBag.User = new
            {
                Email = email,
                Name = username,
            };
            var doctors = _doctorService.GetAllDoctor();
            return View(doctors);

        }

        public IActionResult Details(int did) 
        {
            var doctors = _doctorService.GetAllDoctor();
            var doctor = doctors.FirstOrDefault(x => x.Id == did);
            HttpContext.Session.SetString("doctorId", Convert.ToString(doctor.Id));

            return RedirectToAction("CreateAppointment", "Appointment");
        }
    }
}
