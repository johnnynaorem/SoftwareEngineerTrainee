using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace DoctorAssignment
{
    internal class DoctorServices : IDoctorInterface
    {
        List<Doctor> doctors = new List<Doctor>();

        public void AddDoctor(Doctor doctor)
        {
            try {
                doctors.Add(doctor);
            }catch (Exception ex) {
                throw new InvalidInputDetailException();
            }
        }
        
        public void DisplayDoctor()
        {
            if (doctors.Count == 0) {
                Console.WriteLine("Empty");
                return;
            }
            foreach (Doctor doctor in doctors)
            {
                doctor.PrintDetails();
            }
        }
    }
}
