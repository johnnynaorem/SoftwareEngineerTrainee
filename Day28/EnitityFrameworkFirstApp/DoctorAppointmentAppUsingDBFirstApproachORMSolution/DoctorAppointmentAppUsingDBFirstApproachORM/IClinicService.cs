using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoctorAppointmentAppUsingDBFirstApproachORM
{
    public interface IClinicService
    {
        void DoctorRegistration();
        void PatientRegistration();
        void BookAppointment();
        void AppointmentList();
    }
}
