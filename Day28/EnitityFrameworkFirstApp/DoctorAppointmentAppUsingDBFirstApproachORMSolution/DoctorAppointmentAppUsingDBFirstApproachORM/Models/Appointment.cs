using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoctorAppointmentAppUsingDBFirstApproachORM.Models
{
    internal class Appointment
    {
        [Key]
        public int AppointmentID { get; set; }
        
        public int DoctorID { get; set; }
        [ForeignKey("DoctorID")]
        public Doctor Doctor { get; set; }
        public int PatientID { get; set; }
        [ForeignKey("PatientID")]
        public Patient Patient { get; set; }
        public DateTime DateOfAppointment { get; set; }
        public DateTime SartTime { get; set; }
        public DateTime EndTime { get; set; }
    }
}
