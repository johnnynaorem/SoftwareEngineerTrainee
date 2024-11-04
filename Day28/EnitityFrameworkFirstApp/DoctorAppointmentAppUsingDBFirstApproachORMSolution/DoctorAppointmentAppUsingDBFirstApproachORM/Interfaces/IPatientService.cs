using DoctorAppointmentAppUsingDBFirstApproachORM.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoctorAppointmentAppUsingDBFirstApproachORM.Interfaces
{
    internal interface IPatientService
    {
        void PatientRegistration(string name, string address, string gender, string email, string phone);
        IEnumerable<Patient> GetAllPatient();
    }
}
