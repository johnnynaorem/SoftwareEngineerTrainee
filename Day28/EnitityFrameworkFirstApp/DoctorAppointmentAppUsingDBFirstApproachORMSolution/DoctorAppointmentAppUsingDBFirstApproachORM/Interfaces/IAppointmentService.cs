using DoctorAppointmentAppUsingDBFirstApproachORM.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoctorAppointmentAppUsingDBFirstApproachORM.Interfaces
{
    internal interface IAppointmentService
    {
        void BookAppointment(int doctorId, int patientId, DateTime startTime);
        IEnumerable<Appointment> GetAllAppointment();
    }
}
