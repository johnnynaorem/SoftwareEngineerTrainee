using System.Globalization;

namespace DoctorListWebApplication.Models
{
	public class Doctor
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public long Phone { get; set; }
		public string Email { get; set; }
		public string Speciality { get; set; }

		public string Image {  get; set; }
        public List<IFormFile> PostedFiles { get; set; } = new List<IFormFile>();


    }

}
