using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeligateUnderstanding
{
    public class Employee
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public int Salary { get; set; }
        public override string ToString()
        {
            return "Id = " + ID + ", Name = " + Name + ", Salary = " + Salary;
        }
    }
    internal class LINQ
    {
        public void UnderstandingLIQN() { 
            List<Employee> employees = new List<Employee>
            {
                new Employee { ID = 101, Name = "Mark", Salary = 5000 },
                new Employee { ID = 102, Name = "John", Salary = 10000 },
                new Employee { ID = 103, Name = "Smith", Salary = 14000 },
                new Employee { ID = 104, Name = "Watson", Salary = 15000 }
            };

            var HigherPaidEmployees = employees.Where(e => e.Name.Contains("oh")).OrderByDescending(e=> e.Salary);
            foreach (var employee in HigherPaidEmployees)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine(employee);
                Console.ResetColor();
            }
        }

        public LINQ()
        {
            UnderstandingLIQN();
        }

        static void Main()
        {
            new LINQ();
        }
    }
}
