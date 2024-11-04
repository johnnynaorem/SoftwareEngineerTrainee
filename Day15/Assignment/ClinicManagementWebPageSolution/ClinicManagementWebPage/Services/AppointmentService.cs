using ClinicManagementWebPage.Exceptions;
using ClinicManagementWebPage.Interfaces;
using ClinicManagementWebPage.Models;
using ClinicManagementWebPage.Repositories;

namespace ClinicManagementWebPage.Services
{
    public class AppointmentService : IAppointmentService
    {
        private readonly IRepository<Appointment> _appointmentRepo;
        private readonly IRepository<User> _userRepo;
        private readonly IRepository<Doctor> _doctorRepo;

        public AppointmentService(IRepository<Appointment> appointmentRepo, IRepository<Doctor> doctorRepo, IRepository<User> userRepo)
        { 
            _userRepo = userRepo;
            _doctorRepo = doctorRepo;
            _appointmentRepo = appointmentRepo;
        }
        public Appointment CreateAppointment(string doctor, string user, DateTime startTime)
        {
            if (doctor == null || user == null)
            {
                throw new InvalidAppointment("Doctor and user must not be null.");
            }

            if (startTime <= DateTime.Now)
            {
                throw new InvalidAppointment("Appointment start time must be in the future.");
            }

            var appointments = _appointmentRepo.GetAll();
            var isUserHaveAppointment = appointments.Where(a => a.User == user && a.StartTime == startTime);
            var isDoctorHaveAppointment = appointments.Where(a => a.Doctor == doctor && a.StartTime == startTime);
            if (isUserHaveAppointment.Any())
            {
                throw new InvalidAppointment("You have another appointment at this time. Choose another time.");
            }

            if (isDoctorHaveAppointment.Any())
            {
                throw new InvalidAppointment("Doctor have another appointment at this time. Choose another time.");
            }

            var appointment = new Appointment
            {
                StartTime = startTime,
                Doctor = doctor,
                User = user
            };

            _appointmentRepo.Add(appointment);
            return appointment;
        }

        public Doctor GetDoctor(int did)
        {
            var doctor = _doctorRepo.Get(did);
            return doctor;
        }

        public List<Appointment> GetAppointmentList()
        {
            return _appointmentRepo.GetAll().OrderBy(e => e.StartTime).ToList();
        }


        public List<Appointment> GetAppointment(string user)
        {
            var appointments = _appointmentRepo.GetAll();
            var returnAppointments = appointments.Where(a => a.User == user).ToList();
            return returnAppointments;
        }
    }
}
