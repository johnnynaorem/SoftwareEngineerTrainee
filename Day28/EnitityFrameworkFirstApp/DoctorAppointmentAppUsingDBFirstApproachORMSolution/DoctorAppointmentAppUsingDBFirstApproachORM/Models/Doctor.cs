using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoctorAppointmentAppUsingDBFirstApproachORM.Models
{
    internal class Doctor
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Specialist { get; set; } = string.Empty ;
        public string Gender { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;

        public IEnumerable<Appointment> Appointment { get; set; }
    }
}
