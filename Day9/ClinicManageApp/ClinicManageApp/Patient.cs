using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinicManageApp
{
    internal class Patient
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }

        public double Phone { get; set; }
        public string Email { get; set; }
        public int Age {  get; set; }

        public string Gender { get; set; }

        public string Address { get; set; }

        //List<Appointment> Appointments { get; set; } = new List<Appointment>();

        public Patient() { }
        public Patient(int id, string name, string password, string address, string email, double phone, int age, string gender)
        {
            Id = id;
            Name = name;
            Password = password;
            Address = address;
            Email = email;
            Phone = phone;
            Age = age;
            Gender = gender;
        }

        public override string ToString() {
            return "ID: " + Id + " " + "Name: " + Name + "\n" + "Email: " + Email  + "\n" + "Phone: " + Phone + "\n" + "Address: " + Address + "\n" + "Age: " + Age + " " + "Gender " + Gender;
        }
    }
}
