using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinicManageApp
{
    internal class UserInteraction
    {
        IDoctorInterface doctor;
        IPatientInterface patient;


        public UserInteraction()
        {
            doctor = new ClinicManager();
            patient = (IPatientInterface) doctor;
        }

        private void ConsoleColorController(string message, System.ConsoleColor color)
        {
            Console.ForegroundColor = color;
            Console.Write(message);
            Console.ResetColor();
        }

        private void DoctorRegistration()
        {
            try
            {
                Console.WriteLine("Doctor Registration");
                ConsoleColorController("Enter Doctor ID: ", ConsoleColor.Cyan);
                int id = Convert.ToInt32(Console.ReadLine());
                ConsoleColorController("Enter Doctor Name: ", ConsoleColor.Cyan);
                string name = Console.ReadLine();
                ConsoleColorController("Enter Doctor Gender: ", ConsoleColor.Cyan);
                string gender = Console.ReadLine();
                ConsoleColorController("Enter Email: ", ConsoleColor.Cyan);
                string email = Console.ReadLine();
                ConsoleColorController("Enter Specialty: ", ConsoleColor.Cyan);
                string specialty = Console.ReadLine();
                ConsoleColorController("Create Password: ", ConsoleColor.Cyan);
                string password = Console.ReadLine();
                doctor.DoctorRegister(id, name, specialty, password, gender, email);
            }
            catch(FormatException ex)
            {
                ConsoleColorForException(ex.Message);
            }
        }
        private void ConsoleColorForException(string message)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(message);
            Console.ResetColor();
        }

        private void PateintRegistration()
        {
            try
            {
                Console.WriteLine("Patient Registration");
                ConsoleColorController("Enter Patient ID: ", ConsoleColor.Cyan);
                int id = Convert.ToInt32(Console.ReadLine());
                ConsoleColorController("Enter Patient Name: ", ConsoleColor.Cyan);
                string name = Console.ReadLine();
                ConsoleColorController("Enter Patient Age: ", ConsoleColor.Cyan);
                int age = Convert.ToInt32(Console.ReadLine());
                ConsoleColorController("Enter Email: ", ConsoleColor.Cyan);
                string email = Console.ReadLine();
                ConsoleColorController("Enter Phone Number: ", ConsoleColor.Cyan);
                double phone = Convert.ToDouble(Console.ReadLine());
                ConsoleColorController("Enter Address: ", ConsoleColor.Cyan );
                string address = Console.ReadLine();
                ConsoleColorController("Create password: ", ConsoleColor.Cyan);
                string password = Console.ReadLine();
                ConsoleColorController("Gender: ", ConsoleColor.Cyan);
                string gender = Console.ReadLine();
                patient.PatientRegistor(id, name, password, address, email, phone, age, gender);
            }
            catch (FormatException ex)
            {

                ConsoleColorForException(ex.Message );
            }
        }

        public void MainInteraction()
        {

            while (true)
            {
                Console.WriteLine("1. Doctor");
                Console.WriteLine("2. Patient");
                ConsoleColorController("Select an option: ", ConsoleColor.Magenta);
                var choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                       DoctorMenu();
                        break;
                    case "2":
                        PatientMenu();
                        break;
                    default:
                        ConsoleColorForException("Invalid option.");
                        break;
                }
            }
        }


        public void DoctorLogin()
        {
            try
            {
                ConsoleColorController("Enter Doctor ID: ", ConsoleColor.Cyan);
                int id = Convert.ToInt32(Console.ReadLine());
                ConsoleColorController("Enter password: ", ConsoleColor.Cyan);
                string password = Console.ReadLine();
                doctor.DoctorLogin(id, password);
            }
            catch(FormatException ex)
            {
                ConsoleColorForException(ex.Message);
            }
           

        }

        public void PatientLogin()
        {
            ConsoleColorController("Enter Patient ID: ", ConsoleColor.Cyan);
            int id = Convert.ToInt32(Console.ReadLine());
            ConsoleColorController("Enter Password: ", ConsoleColor.Cyan);
            string password = Console.ReadLine();
            patient.PatientLogin(id, password);
        }
        public void DoctorMenu()
        {
            while (true)
            {
                Console.WriteLine("Doctor Menu:");
                Console.WriteLine("1. Login");
                Console.WriteLine("2. Registration");
                Console.WriteLine("3. Back");
                ConsoleColorController("Select an option: ", ConsoleColor.Magenta);
                var choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        DoctorLogin();
                        break;
                    case "2":
                        DoctorRegistration();
                        break;
                    case "3":
                        return;
                    default:
                        ConsoleColorForException("Invalid option.");
                        break;
                }
            }
        }
        public void PatientMenu()
        {
            while (true)
            {
                Console.WriteLine("Patient Menu:");
                Console.WriteLine("1. Registration");
                Console.WriteLine("2. Login");
                Console.WriteLine("0. Back");
                ConsoleColorController("Select an option: ", ConsoleColor.Magenta);
                var choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        PateintRegistration();
                        break;
                    case "2":
                        PatientLogin();
                        break;
                    case "0":
                        return;
                    default:
                        ConsoleColorForException("Invalid option.");
                        break;
                }
            }
        }
    }
}
