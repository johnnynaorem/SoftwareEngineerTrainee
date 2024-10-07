using ClinicManagementWebPage.Models;

namespace ClinicManagementWebPage.Interfaces
{
    public interface IAppointmentService
    {
        Appointment CreateAppointment(string doctor, string user, DateTime startTime);
        List<Appointment> GetAppointmentList();

        List<Appointment> GetAppointment(string user);

        Doctor GetDoctor(int did);
    }
}
