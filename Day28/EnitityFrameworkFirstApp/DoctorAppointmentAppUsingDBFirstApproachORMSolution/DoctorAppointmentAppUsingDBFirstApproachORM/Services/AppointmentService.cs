using DoctorAppointmentAppUsingDBFirstApproachORM.Interfaces;
using DoctorAppointmentAppUsingDBFirstApproachORM.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace DoctorAppointmentAppUsingDBFirstApproachORM.Services
{
    internal class AppointmentService : IAppointmentService
    {
        ClinicContext context = new ClinicContext();
        public void BookAppointment(int doctorId, int patientId, DateTime startTime)
        {
            var appointments = GetAllAppointment();
            var isDoctorFree = appointments.FirstOrDefault(a => a.DoctorID == doctorId && a.SartTime == startTime);
            var isPatientFree = appointments.FirstOrDefault(a => a.PatientID == patientId && a.SartTime == startTime);
            if (isDoctorFree == null && isPatientFree == null) {
                try
                {
                    Appointment appointment = new Appointment()
                    {
                        DoctorID = doctorId,
                        PatientID = patientId,
                        DateOfAppointment = DateTime.Now,
                        SartTime = startTime,
                        EndTime = startTime.AddMinutes(45),
                    };
                    context.Appointments.Add(appointment);
                    context.SaveChanges();
                    Console.WriteLine("Appointment Booked");
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
            else
            {
                if (isPatientFree != null) Console.WriteLine("Patient have appointment in this time");
                if (isDoctorFree != null) Console.WriteLine("Doctor have appointment in this time");
            }
            
        }

        public IEnumerable<Appointment> GetAllAppointment()
        {
            var appointments = context.Appointments.ToList();
            return appointments;
        }
    }
}
