using DoctorAppointmentAppUsingDBFirstApproachORM.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoctorAppointmentAppUsingDBFirstApproachORM
{
    public class ClinicService : IClinicService
    {
        ClinicContext context = new ClinicContext();
        public void AppointmentList()
        {
            try
            {
                var appointments = context.Appointments.ToList();
                foreach (var appointment in appointments)
                {
                    Console.WriteLine($"Id:{appointment.AppointmentID}\nDoctorID:{appointment.DoctorID}\nPatientID:{appointment.PatientID}\nDate Of Appointment:{appointment.DateOfAppointment}\nStart Time: {appointment.SartTime}\nEnd Time: {appointment.EndTime}");
                    var doctor = context.Doctors.FirstOrDefault(d => d.Id == appointment.DoctorID);
                    Console.WriteLine("Doctor Detalis");
                    Console.WriteLine($"\tName:{doctor.Name}");
                    Console.WriteLine($"\tSpecialist:{doctor.Specialist}");

                    var patient = context.Patients.FirstOrDefault(p => p.PatientID == appointment.PatientID);
                    Console.WriteLine("Patient Details: ");
                    Console.WriteLine($"\tName:{patient.PatientName}");

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public void BookAppointment()
        {
            Console.Write("Enter the DoctorID: ");
            int id = Convert.ToInt16(Console.ReadLine());
            Console.Write("Enter the PatientID: ");
            int pId = Convert.ToInt16(Console.ReadLine());
            Console.Write("Enter Time (eg. 2024-10-15 14:30): ");
            DateTime startTime = Convert.ToDateTime(Console.ReadLine());
            try
            {
                Appointment appointment = new Appointment()
                {
                    DoctorID = id,
                    PatientID = pId,
                    DateOfAppointment = DateTime.Now,
                    SartTime = startTime,
                    EndTime = startTime.AddMinutes(45),
                };
                context.Appointments.Add(appointment);
                context.SaveChanges();
                Console.WriteLine("Appointment added");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public void DoctorRegistration()
        {
            Console.Write("Enter the Name: ");
            string name = Console.ReadLine();
            Console.Write("Enter the Specialist: ");
            string specialist = Console.ReadLine();
            Console.Write("Enter the Gender: ");
            string gender = Console.ReadLine();
            Console.Write("Enter the Email: ");
            string email = Console.ReadLine();
            Console.Write("Enter the Phone: ");
            string phone = Console.ReadLine();
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

        public void PatientRegistration()
        {
            Console.Write("Enter the Name: ");
            string name = Console.ReadLine();
            Console.Write("Enter the Address: ");
            string address = Console.ReadLine();
            Console.Write("Enter the Gender: ");
            string gender = Console.ReadLine();
            Console.Write("Enter the Email: ");
            string email = Console.ReadLine();
            Console.Write("Enter the Phone: ");
            string phone = Console.ReadLine();

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
