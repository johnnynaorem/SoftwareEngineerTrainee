using Microsoft.VisualBasic;

namespace DoctorAssignment
{
    internal class Program
    {
        IDoctorInterface doctorManager;

        public Program()
        {
            doctorManager = new DoctorServices();
        }

        public void printMenu()
        {
            Console.WriteLine("1-Add Doctor");
            Console.WriteLine("2-Print All Doctors");
            Console.WriteLine("0-Exit");
        }

        public void AddDoctor()
        {
            try {
                Console.WriteLine("Enter the details of the doctor:");

                Console.Write("Name: ");
                string name = Console.ReadLine();

                Console.Write("Specialty: ");
                string specialty = Console.ReadLine();

                Console.Write("Years of Experience: ");
                int yearsOfExperience = Convert.ToInt32(Console.ReadLine());

                Doctor doctor = new Doctor(name, specialty, yearsOfExperience);

                doctorManager.AddDoctor(doctor);
            }
            catch(FormatException ex)
            {
                Console.WriteLine("Years of Exprience Input was not in the required type");
            }
            catch (InvalidInputDetailException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        void MainInteraction()
        {
            var choice = 0;
            do
            {
                printMenu();
                choice = Convert.ToInt32(Console.ReadLine());
                switch (choice)
                {
                    case 1:
                        AddDoctor();
                        break;
                    case 2:
                        doctorManager.DisplayDoctor();
                        break;
                    case 0:
                        Console.WriteLine("Exit...");
                        break;
                    default:
                        Console.WriteLine("Invalid option");
                        break;
                }
            } while (choice != 0);
        }

        static void Main(string[] args)
        {
            Program program = new Program();
            program.MainInteraction();
        }
    }
}
