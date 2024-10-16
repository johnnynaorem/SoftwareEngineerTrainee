using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoctorAppointmentAppUsingDBFirstApproachORM.Models
{
    internal class Patient
    {
        public int PatientID { get; set; }
        public string PatientName { get; set; } = string.Empty;
        public string Gender {  get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public string Email {  get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public IEnumerable<Appointment> Appointments { get; set;}

    }
}
