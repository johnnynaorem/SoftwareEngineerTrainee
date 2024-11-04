using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace College
{
    internal class Student
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public DateTime DateOfBirth { get; set; }
        public double MobileNumber { get; set; }
        public string Email { get; set; } = string.Empty;

        public Student()
        {
            Id = 07;
            Name = "Johnny Naorem";
            DateOfBirth = new DateTime(2002, 02, 28);
            MobileNumber = 8787470933;
            Email = "johnnynaorem7@gmail.com";
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
