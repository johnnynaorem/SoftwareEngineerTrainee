using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinicManageApp
{
    public interface IDoctorInterface
    {
        public void DoctorRegister(int id, string name, string speciality, string password, string gender, string email);
        public void DoctorLogin(int id, string password);
    }
}
