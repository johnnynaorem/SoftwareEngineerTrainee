using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace CreatingApi
{
    internal class Employee
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public DateTime DateOfBirth { get; set; }
        public double MobileNumber { get; set; }
        public string Email { get; set; } = string.Empty;

        public Employee() {
        }

        public Employee(int id, string name, DateTime dateOfBirth, double mobileNumber, string email)
        {
            Id = id;
            Name = name;
            DateOfBirth = dateOfBirth;
            MobileNumber = mobileNumber;
            Email = email;
        }

        public void DisplayEmployee()
        {
            Console.WriteLine($"ID: \"{Id}\"  Name: \"{Name}\"");
            Console.WriteLine($"Date of Birth: \"{DateOfBirth.ToString("d")}\"");
            Console.WriteLine($"Phone: \"{MobileNumber}\"");
            Console.WriteLine($"Email: \"{Email}\"");
        }

    }

}
