using DoctorAppointmentAppUsingDBFirstApproachORM.Interfaces;
using DoctorAppointmentAppUsingDBFirstApproachORM.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoctorAppointmentAppUsingDBFirstApproachORM.Models
{
    internal class UserInteraction
    {
        //IClinicService service = new ClinicService();
        IDoctorService doctorService = new DoctorService();
        IPatientService patientService = new PatientService();
        IAppointmentService appointmentService = new AppointmentService();

        public void MainMenu()
        {
            int choice;
            do
            {
                Console.WriteLine("1. Doctor");
                Console.WriteLine("2. Patient");
                Console.WriteLine("0. Exit");
                Console.Write("Choose: ");
                choice = Convert.ToInt16(Console.ReadLine());
                switch (choice)
                {
                    case 1:
                        DoctorMenu();
                        break;
                    case 2:
                        PaitentMenu();
                        break;
                    case 0:
                        break;
                    default:
                        Console.WriteLine("Enter Valid Option");
                        break;
                }
            }
            while (choice != 0);
        }

        private void DoctorMenu()
        {
            int choice;
            do
            {
                Console.WriteLine("1. Doctor Registration");
                Console.WriteLine("2. View Appointments");
                Console.WriteLine("0. Back");
                Console.Write("Choose: ");
                choice = Convert.ToInt16(Console.ReadLine());
                switch (choice)
                {
                    case 1:
                        DoctorRegistration();
                        break;
                    case 2:
                        ViewAppointment();
                        break;
                    case 0:
                        break;
                    default:
                        Console.WriteLine("Enter Valid Option");
                        break;
                }
            }
            while (choice != 0);
        }

        private void PaitentMenu()
        {
            int choice;
            do
            {
                Console.WriteLine("1. Patient Registration");
                Console.WriteLine("2. View Doctors");
                Console.WriteLine("3. Book Appointment");
                Console.WriteLine("4. View Appointment");
                Console.WriteLine("0. Back");
                Console.Write("Choose: ");
                choice = Convert.ToInt16(Console.ReadLine());
                switch (choice)
                {
                    case 1:
                        PatientRegistration();
                        break;
                    case 2:
                        ViewDoctor();
                        break;
                    case 3:
                        BookAppointment();
                        break;
                    case 4:
                        ViewAppointment();
                        break;
                    case 0:
                        break;
                    default:
                        Console.WriteLine("Enter Valid Option");
                        break;
                }
            }
            while (choice != 0);
        }

        private void BookAppointment()
        {
            Console.Write("Enter the DoctorID: ");
            int id = Convert.ToInt16(Console.ReadLine());
            Console.Write("Enter the PatientID: ");
            int pId = Convert.ToInt16(Console.ReadLine());
            Console.Write("Enter Time (eg. 2024-10-15 14:30): ");
            DateTime startTime = Convert.ToDateTime(Console.ReadLine());

            try
            {
                appointmentService.BookAppointment(id, pId, startTime);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private void PatientRegistration()
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
                patientService.PatientRegistration(name, address, gender, email, phone);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private void ViewAppointment()
        {
            try
            {
                var appointments = appointmentService.GetAllAppointment();
                foreach (var appointment in appointments)
                {
                    Console.WriteLine($"Id:{appointment.AppointmentID}\nDoctorID:{appointment.DoctorID}\nPatientID:{appointment.PatientID}\nDate Of Appointment:{appointment.DateOfAppointment}\nStart Time: {appointment.SartTime}\nEnd Time: {appointment.EndTime}");
                    var doctors = doctorService.GetAllDoctors();
                    var doctor = doctors.FirstOrDefault(d => d.Id == appointment.DoctorID);
                    Console.WriteLine("---------Doctor Details");
                    Console.WriteLine($"Name: {doctor.Name}\t Specialist: {doctor.Specialist}\nEmail: {doctor.Email} \t Gender: {doctor.Gender}\t Phone: {doctor.Phone}");

                    var patients = patientService.GetAllPatient();
                    var patient = patients.FirstOrDefault(p => p.PatientID == appointment.PatientID);
                    Console.WriteLine("---------Patient Details: ");
                    Console.WriteLine($"Name: {patient.PatientName}\t Address: {patient.Address}\nEmail: {patient.Email} \t Gender: {doctor.Gender}\t Phone: {patient.Phone}");
                    Console.WriteLine("--------------------------------------------------------------");

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private void ViewDoctor()
        {
            try
            {
                var doctors = doctorService.GetAllDoctors();
                foreach (var doctor in doctors)
                {
                    Console.WriteLine($"Name: {doctor.Name}\t Specialist: {doctor.Specialist}\nEmail: {doctor.Email} \t Gender: {doctor.Gender}\t Phone: {doctor.Phone}");
                    Console.WriteLine("--------------------------------------");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private void DoctorRegistration()
        {
            Console.Write("Enter Doctor Name: ");
            string name = Console.ReadLine();
            Console.Write("Enter Specialist: ");
            string specialist = Console.ReadLine();
            Console.Write("Enter Gender: ");
            string gender = Console.ReadLine();
            Console.Write("Enter Email: ");
            string email = Console.ReadLine();
            Console.Write("Enter Phone: ");
            string phone = Console.ReadLine();

            try
            {
                doctorService.DoctorRegistration(name, specialist, gender, email, phone);

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        //public void Menu()
        //{
        //    int choice;
        //    do
        //    {
        //        Console.WriteLine("1. Doctor Registration");
        //        Console.WriteLine("2. Patient Registration");
        //        Console.WriteLine("3. Appointment Booking");
        //        Console.WriteLine("4. View Appointments");
        //        Console.WriteLine("0. Exit");
        //        Console.Write("Choose: ");
        //        choice = Convert.ToInt16(Console.ReadLine());
        //        switch (choice)
        //        {
        //            case 1:
        //                service.DoctorRegistration();
        //                break;
        //            case 2:
        //                service.PatientRegistration();
        //                break;
        //            case 3:
        //                service.BookAppointment();
        //                break;
        //            case 4:
        //                service.AppointmentList();
        //                break;
        //            case 0:
        //                break;
        //            default:
        //                Console.WriteLine("Enter Valid Option");
        //                break;
        //        }
        //    }
        //    while(choice!= 0);
        //}
    }
}
