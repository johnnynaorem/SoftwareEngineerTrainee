using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoctorAppointmentAppUsingDBFirstApproachORM.Models
{
    internal class UserInteraction
    {
        IClinicService service = new ClinicService();

        public void Menu()
        {
            int choice;
            do
            {
                Console.WriteLine("1. Doctor Registration");
                Console.WriteLine("2. Patient Registration");
                Console.WriteLine("3. Appointment Booking");
                Console.WriteLine("4. View Appointments");
                Console.WriteLine("0. Exit");
                Console.Write("Choose: ");
                choice = Convert.ToInt16(Console.ReadLine());
                switch (choice)
                {
                    case 1:
                        service.DoctorRegistration();
                        break;
                    case 2:
                        service.PatientRegistration();
                        break;
                    case 3:
                        service.BookAppointment();
                        break;
                    case 4:
                        service.AppointmentList();
                        break;
                    case 0:
                        break;
                    default:
                        Console.WriteLine("Enter Valid Option");
                        break;
                }
            }
            while(choice!= 0);
        }
        
        //public void BookAppointmentByDoctorAndPatient()
        //{
        //    Console.Write("Enter the Doctor Name: ");
        //    string name = Console.ReadLine();
        //    Console.Write("Enter Specialist: ");
        //    string specialist = Console.ReadLine();
        //    Console.Write("Enter the Patient Name: ");
        //    string pname = Console.ReadLine();
        //    Console.Write("Enter Email: ");
        //    string email = Console.ReadLine();
        //    try
        //    {
        //        var doctor = context.Doctors.FirstOrDefault(d => d.Name == name && d.Specialist == specialist);
        //        var patient = context.Patients.FirstOrDefault(p => p.PatientName == name && p.Email == email);
        //        Appointment appointment = new Appointment()
        //        {
        //            DoctorID = doctor.Id,
        //            PatientID = patient.PatientID,
        //            DateOfAppointment = DateTime.Now
        //        };
        //        context.Appointments.Add(appointment);
        //        context.SaveChanges();
        //        Console.WriteLine("Appointment added");
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine(ex.Message);
        //    }

        //}
        //}
    }
}
