using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeAppSolution
{
    public class UserInteraction
    {
        EmployeeServices employeeServices;
        public UserInteraction()
        {
            employeeServices = new EmployeeServices();
        }
        public void ShowMenu()
        {
            int choice;
            do
            {
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine("Employee Manager:");
                Console.WriteLine("1. Add Employee");
                Console.WriteLine("2. Find and Print Employee by ID");
                Console.WriteLine("3. Find and Print Employee by Name");
                Console.WriteLine("4. Find and Print Older than Employee by Name");
                Console.WriteLine("5. Print All Employees");
                Console.WriteLine("6. Sort Employees by Salary");
                Console.WriteLine("7. Delete Employee");
                Console.WriteLine("8. Modify Employee");
                Console.WriteLine("0. Exit");
                Console.ResetColor();
                Console.Write("Enter your choice (1-8): ");

                Console.ForegroundColor = ConsoleColor.Green;
                choice = Convert.ToInt32(Console.ReadLine());
                Console.ResetColor();

                switch (choice)
                {
                    case 1:
                        employeeServices.TakeEmployeeDetailsAndStore();
                        break;
                    case 2:
                        employeeServices.FindAndPrintEmployeeById();
                        break;
                    case 3:
                        employeeServices.FindEmployeeByName();
                        break;
                    case 4:
                        employeeServices.FindEmployeeByNameAndPrintOlder();
                        break;
                    case 5:
                        employeeServices.PrintEmployee();
                        break;
                    case 6:
                        employeeServices.SortingEmployeeList();
                        break;
                    case 7:
                        employeeServices.DeleteEmployee();
                        break;
                    case 8:
                        employeeServices.ModifyEmployeeDetails();
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
                Console.WriteLine();
            } while (choice != 0);
        }
    }
}
