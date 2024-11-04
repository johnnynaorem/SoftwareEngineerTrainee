using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeePromotionApp
{
    internal class EmployeePromotion
    {

       List<string> employees;

        public EmployeePromotion()
        {
            employees = new List<string>();
        }

        public void AddEmployeeNames()
        {
            Console.WriteLine("Please enter the employee names in the order of their eligibility for promotion (Please enter blank to stop):");

            while (true)
            {
                string input = Console.ReadLine();

                if (string.IsNullOrWhiteSpace(input))
                {
                    break;
                }

                employees.Add(input);
            }
        }

        public void FindPosition()
        {
            Console.WriteLine("Please enter the name of the employee to check promotion position");
            string name = Console.ReadLine()?? "";
            var index = employees.IndexOf(name);

            Console.WriteLine($"\"{name}\" is the the position {index+1} for promotion.");
        }

        public void PromotedEmployee()
        {
            Console.WriteLine("----------------------------------------");
            Console.WriteLine("Promoted Employee list: ");
            employees.Sort();
            foreach (var employee in employees)
            {
                Console.WriteLine(employee);
            }
        }

        public void TestCapacity()
        {
            List<int> number = new List<int>();
            for (int i = 0; i < 10; i++) {
                number.Add(i);
                Console.WriteLine($"The current size of the collection for adding {i} is {number.Capacity}");
            }
        }

        public void CapacityOfList()
        {
            Console.WriteLine("----------------------------------------------------------");
            Console.WriteLine($"The current size of the collection is {employees.Capacity}");
            employees.TrimExcess();
            Console.WriteLine($"The size after removing the extra space is {employees.Capacity}");
        }

        public void DisplayEmployees()
        {
            Console.WriteLine("\nThe employees eligible for promotion are: ");

            foreach (var employee in employees)
            {
                Console.WriteLine(employee);
            }
        }
    }
}
