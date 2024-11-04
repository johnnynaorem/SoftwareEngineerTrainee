using ClinicManagementWebPage.Exceptions;
using ClinicManagementWebPage.Interfaces;
using ClinicManagementWebPage.Models;
using Microsoft.AspNetCore.Mvc;
using System.Numerics;

namespace ClinicManagementWebPage.Controllers
{
    public class AppointmentController : Controller
    {
        private readonly IAppointmentService _appointmentService;

        public AppointmentController(IAppointmentService appointmentService)
        {
            _appointmentService = appointmentService;
        }

        public IActionResult CreateAppointment()
        {
            var username = HttpContext.Session.GetString("username");
            string id = HttpContext.Session.GetString("doctorId");
            int did;
            if (id == null)
            {
                return NotFound();
            }
            did = Convert.ToInt16(id);
            var doctor = _appointmentService.GetDoctor(did);
            ViewBag.User = new
            {
                Doctor = doctor.Name,
                Name = username,
            };

            return View();
        }

        [HttpPost]
        public IActionResult CreateAppointment(string doctor, string user, DateTime startTime)
        {
            try
            {
                var appointment = _appointmentService.CreateAppointment(doctor, user, startTime);
                if(appointment!=null) return RedirectToAction("DisplayAppointment");
                throw new InvalidAppointment();
            }
            catch (InvalidAppointment ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                ViewBag.User = new { Name = user, Doctor = doctor };
                return View(); 
            }
        }

        public IActionResult DisplayAppointment()
        {
            var username = HttpContext.Session.GetString("username");
            Console.WriteLine(username);
            ViewBag.User = new
            {
                Name = username,
            };
            var appointments = _appointmentService.GetAppointment(username);
            //var appointments = _appointmentService.GetAppointmentList();
            return View(appointments);
        }
    }
}
