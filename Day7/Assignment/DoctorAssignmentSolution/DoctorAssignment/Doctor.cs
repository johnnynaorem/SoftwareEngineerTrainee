using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoctorAssignment
{
    public class Doctor
    {
        public string Name { get; set; }
        public string Specialty { get; set; }
        public int YearsOfExperience { get; set; }

        public Doctor(string name, string specialty, int yearsOfExperience)
        {
            Name = name;
            Specialty = specialty;
            YearsOfExperience = yearsOfExperience;
        }

        public void PrintDetails()
        {
            Console.WriteLine("------Doctors in Our Database------ ");
            Console.WriteLine($"Name: {Name}");
            Console.WriteLine($"Specialty: {Specialty}");
            Console.WriteLine($"Years of Experience: {YearsOfExperience}");
            Console.WriteLine();
        }
    }
}
