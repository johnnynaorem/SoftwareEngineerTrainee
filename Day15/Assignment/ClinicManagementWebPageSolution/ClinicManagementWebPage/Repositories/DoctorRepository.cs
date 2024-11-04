using ClinicManagementWebPage.Interfaces;
using ClinicManagementWebPage.Models;

namespace ClinicManagementWebPage.Repositories
{
    public class DoctorRepository : IRepository<Doctor>
    {
        static List<Doctor> doctors = new List<Doctor>
        {
            new Doctor{Id=1, Name="Patey Cruiser", Email="paty@gmail.com", Phone=8787321466, Speciality="Neurosurgeon", Image="/images/Patey.jpg"},
            new Doctor{Id=2, Name="Jacqueliene Fernandez", Email="jacqueline@gmail.com", Phone=8787327523, Speciality="Gynecologist", Image="/images/Jacqueliene.jpg"},
            new Doctor{Id=3, Name="Anna Sthesia", Email="anna@gmail.com", Phone=8787470922, Speciality="Surgeon", Image="/images/Anna.jpg"},
            new Doctor{Id=4, Name="Paul Moliv", Email="paul@gmail.com", Phone=87874231133, Speciality="Dentist", Image="/images/Paul.jpg"},
            new Doctor{Id=5, Name="Anna Maul", Email="annamaul@gmail.com", Phone=87874237613, Speciality="Eye Specialist", Image="/images/Maul.jpg"},
            new Doctor{Id=6, Name="Gail Forcewind", Email="gail@gmail.com", Phone=87874231132, Speciality="Urology", Image="/images/Gail.jpg"},

        };


        public List<Doctor> GetAll()
        {
            return doctors;
        }

        public Doctor Get(string email, string password)
        {
            throw new NotImplementedException();
        }


        public Doctor Get(int id)
        {
            var doctor = doctors.FirstOrDefault(x => x.Id == id);
            return doctor;
        }

        public void Add(Doctor entity)
        {
            throw new NotImplementedException();
        }

        public void Update(Doctor entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }
    }
}
