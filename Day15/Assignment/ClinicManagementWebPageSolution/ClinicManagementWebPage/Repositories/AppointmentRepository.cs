using ClinicManagementWebPage.Interfaces;
using ClinicManagementWebPage.Models;

namespace ClinicManagementWebPage.Repositories
{
    public class AppointmentRepository : IRepository<Appointment>
    {
        private static List<Appointment> _appointments = new List<Appointment>();

        public List<Appointment> GetAll()
        {
            return _appointments;
        }

        public Appointment Get(int id)
        {
            return _appointments.FirstOrDefault(a => a.AppointmentId == id);
        }

        public void Add(Appointment appointment)
        {
            appointment.AppointmentId = _appointments.Count + 1;
            _appointments.Add(appointment);
        }

        public void Update(Appointment appointment)
        {
            var existingAppointment = Get(appointment.AppointmentId);
            if (existingAppointment != null)
            {
                existingAppointment.StartTime = appointment.StartTime;
                existingAppointment.Doctor = appointment.Doctor;
                existingAppointment.User = appointment.User;
            }
        }

        public void Delete(int id)
        {
            var appointment = Get(id);
            if (appointment != null)
            {
                _appointments.Remove(appointment);
            }
        }

        public Appointment Get(string email, string password)
        {
            throw new NotImplementedException();
        }
    }
}
