using DoctorListWebApplication.Models;
using Microsoft.AspNetCore.Mvc;

namespace DoctorListWebApplication.Controllers
{
	public class DoctorController : Controller
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

        private readonly ILogger<DoctorController> _logger;
        private IWebHostEnvironment Environment;
        public DoctorController(ILogger<DoctorController> logger, IWebHostEnvironment _environment)
        {
            _logger = logger;
            Environment = _environment;
        }


        public IActionResult Index()
		{

			return View(doctors);
		}

		[HttpGet]
		public IActionResult Create(){
			return View(new Doctor());
		}

		[HttpPost]

		[HttpPost]
		public IActionResult Create(Doctor doctor)
		{
			if (doctor != null && doctor.PostedFiles.Count > 0)
			{
				foreach (var file in doctor.PostedFiles)
				{
					if (file.Length > 0)
					{
						var fileName = Path.GetFileName(file.FileName);
						var path = Path.Combine(Environment.WebRootPath, "images", fileName);

						var counter = 1;
						while (System.IO.File.Exists(path))
						{
							var newFileName = $"{Path.GetFileNameWithoutExtension(fileName)}_{counter++}{Path.GetExtension(fileName)}";
							path = Path.Combine(Environment.WebRootPath, "images", newFileName);
						}

						using (var stream = new FileStream(path, FileMode.Create))
						{
							file.CopyTo(stream);
						}
						doctor.Image = $"/images/{fileName}";
					}
				}

				doctor.Id = doctors.Count + 1;
        doctors.Add(doctor);

			}
				return RedirectToAction("Index");
		}


        [HttpGet]
		public IActionResult Details(int did)
		{
			var doctor = doctors.FirstOrDefault(d => d.Id == did);
			return View(doctor);
		}

		[HttpGet]
		public IActionResult Edit(int did){
			var doctor = doctors.FirstOrDefault(d => d.Id == did);
			return View(doctor);
		}

		[HttpPost]
		public IActionResult Edit(int did, Doctor doctor){
			var oldDoctor = doctors.FirstOrDefault(d => d.Id==did);
			oldDoctor.Name = doctor.Name;
			oldDoctor.Speciality = doctor.Speciality;
			oldDoctor.Email = doctor.Email;
            oldDoctor.Phone = doctor.Phone;
			oldDoctor.Image = "/images/" + doctor.Image;
			return RedirectToAction("Index");
		}

		[HttpPost]
		public IActionResult Delete(int did){
		 var doctor = doctors.FirstOrDefault(e=> e.Id == did);
		 doctors.Remove(doctor);
		 return RedirectToAction("Index");
		}
	}
}
