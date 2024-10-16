using DoctorAppointmentAppUsingDBFirstApproachORM.Interfaces;
using DoctorAppointmentAppUsingDBFirstApproachORM.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoctorAppointmentAppUsingDBFirstApproachORM.Services
{
    internal class DoctorService:IDoctorService
    {
        ClinicContext context = new ClinicContext();

        public void DoctorRegistration(string name, string specialist, string gender, string email, string phone)
        {
            try
            {
                Doctor doctor = new Doctor()
                {
                    Name = name,
                    Specialist = specialist,
                    Gender = gender,
                    Email = email,
                    Phone = phone
                };
                context.Doctors.Add(doctor);
                context.SaveChanges();
                Console.WriteLine("Doctor added");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public IEnumerable<Doctor> GetAllDoctors()
        {
            var doctors = context.Doctors.ToList();
            return doctors;
        }
    }
}
