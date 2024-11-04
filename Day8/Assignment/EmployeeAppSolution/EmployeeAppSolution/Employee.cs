using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace EmployeeAppSolution
{
    public class Employee : IComparable<Employee>
    {
        public int Id { get; set; }
        public int Age { get; set; }
        public string Name { get; set; }
        public decimal Salary { get; set; }
        public Employee()
        {
        }
        public Employee(int id, int age, string name, decimal salary)
        {
            this.Id = id;
            this.Age = age;
            this.Name = name;
            this.Salary = salary;
        }

        public void GetEmployeeDetailsFromConsole()
        {
            try
            {
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.Write("Enter employee name: ");
                Console.ResetColor();
                Name = Console.ReadLine();

                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.Write("Enter employee age: ");
                Console.ResetColor();
                Age = Convert.ToInt32(Console.ReadLine());

                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.Write("Enter employee salary: ");
                Console.ResetColor();
                Salary = Convert.ToDecimal(Console.ReadLine());
            }
            catch (FormatException)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Invalid format. Please enter a valid number.");
                Console.ResetColor();
                throw;
            }
        }

        public override string ToString() { 
            return "Employee ID : " + Id + "\nName: " + Name + "\nAge: " + Age +"\nSalary: " + Salary;
        }

        public int CompareTo(Employee? other)
        {
          return this.Salary.CompareTo(other.Salary);
        }
    }
}
