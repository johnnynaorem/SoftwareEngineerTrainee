using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinicManageApp
{
    public interface IPatientInterface
    {
        void PatientRegistor(int id, string name, string password, string address, string email, double phone, int age, string gender);
        void PatientLogin(int id, string password);
    }
}
