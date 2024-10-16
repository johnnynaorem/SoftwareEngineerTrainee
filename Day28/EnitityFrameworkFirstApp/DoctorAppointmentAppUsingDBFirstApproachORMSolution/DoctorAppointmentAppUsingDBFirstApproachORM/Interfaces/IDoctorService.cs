using DoctorAppointmentAppUsingDBFirstApproachORM.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoctorAppointmentAppUsingDBFirstApproachORM.Interfaces
{
    internal interface IDoctorService
    {
        void DoctorRegistration(string name, string specialist, string gender, string email, string phone);
        IEnumerable<Doctor> GetAllDoctors();
    }
}
