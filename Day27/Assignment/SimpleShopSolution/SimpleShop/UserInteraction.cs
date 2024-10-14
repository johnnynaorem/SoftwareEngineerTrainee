using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleShop
{
    public class UserInteraction
    {
        IService service;
        public UserInteraction() {
            service = new Service();
        }

        public void UserInteractions()
        {
            int choice;
            do
            {
                Console.WriteLine("1. Login");
                Console.WriteLine("2. Create Account");
                Console.WriteLine("0. Exit");

                Console.Write("Enter your choice: ");
                choice = Convert.ToInt32(Console.ReadLine());
                switch (choice)
                {
                    case 1:
                        service.UserLogin();
                        break;
                    case 2:
                        service.CreateUser();
                        break;
                    case 0:
                        Console.ForegroundColor = ConsoleColor.Magenta;
                        Console.WriteLine("Exiting...");
                        Console.ResetColor();
                        break;
                    default:
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Invalid choice. Please enter a number between 1 and 6.");
                        Console.ResetColor();
                        break;
                }
            } while (choice != 0);
        }
    }
}
