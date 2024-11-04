namespace ClinicManagementWebPage.Models
{
    public class Appointment
    {
        public int AppointmentId { get; set; }
        public DateTime EndTime => StartTime.AddMinutes(30);
        public DateTime StartTime { get; set; }
        public string Doctor { get; set; }
        public string User { get; set; }

    }
}
