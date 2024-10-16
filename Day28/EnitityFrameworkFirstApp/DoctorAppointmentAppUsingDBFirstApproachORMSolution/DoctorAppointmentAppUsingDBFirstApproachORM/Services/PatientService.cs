using DoctorAppointmentAppUsingDBFirstApproachORM.Interfaces;
using DoctorAppointmentAppUsingDBFirstApproachORM.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Numerics;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace DoctorAppointmentAppUsingDBFirstApproachORM.Services
{
    internal class PatientService : IPatientService
    {
        ClinicContext context = new ClinicContext();
        public IEnumerable<Patient> GetAllPatient()
        {
            var patients = context.Patients.ToList();
            return patients;
        }

        public void PatientRegistration(string name, string address, string gender, string email, string phone)
        {
            try
            {
                Patient patient = new Patient()
                {
                    PatientName = name,
                    Address = address,
                    Gender = gender,
                    Email = email,
                    Phone = phone
                };
                context.Patients.Add(patient);
                context.SaveChanges();
                Console.WriteLine("Patient added");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
