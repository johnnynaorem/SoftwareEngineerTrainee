using ClinicManagementWebPage.Interfaces;
using ClinicManagementWebPage.Models;

namespace ClinicManagementWebPage.Services
{
    public class DoctorService : IDoctorService
    {
        private readonly IRepository<Doctor> _doctorRepository;

        public DoctorService(IRepository<Doctor> doctorService)
        {
            _doctorRepository = doctorService;
        }
        public List<Doctor> GetAllDoctor()
        {
            var doctors = _doctorRepository.GetAll();
            return doctors;
        }
    }
}
