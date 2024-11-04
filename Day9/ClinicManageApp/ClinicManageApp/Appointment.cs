using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ClinicManageApp
{
    internal class Appointment
    {
        public DateTime EndTime => StartTime.AddMinutes(30);
        public DateTime StartTime { get; set; }
        public Doctor Doctor { get; set; }
        public Patient Patient { get; set; }

        public Appointment() { }   
        public Appointment(DateTime startTime, Doctor doctor, Patient patient)
        {
            StartTime = startTime;
            Doctor = doctor;
            Patient = patient;
        }

        public override string ToString()
        {
            return "StartTime: " + StartTime + "Doctor: " + Doctor + "Patient: " + Patient;
        }
    }
}
