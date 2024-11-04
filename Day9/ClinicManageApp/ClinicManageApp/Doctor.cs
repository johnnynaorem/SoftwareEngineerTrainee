using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace ClinicManageApp
{
    public class Doctor
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Speciality { get; set; }

        public string Gender { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }

        //List<Appointment> Appointments { get; set; } = new List<Appointment>();


        public Doctor() { }
        public Doctor(int id, string name, string speciality, string password, string gender, string email)
        {
            Id = id;
            Name = name;
            Speciality = speciality;
            Password = password;
            Gender = gender;
            Email = email;
        }

        public override string ToString()
        {
            return "ID: " + Id + " " + "Name: " + Name +"\n" + "Email: " + Email +"\n" + "Speciality: " + Speciality + "\n" + "Gender: " + Gender;
        }
    }
}
