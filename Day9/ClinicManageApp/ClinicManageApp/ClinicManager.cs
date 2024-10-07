namespace ClinicManageApp
{
    internal class ClinicManager : IDoctorInterface, IPatientInterface
    {
        private List<Doctor> doctors = new List<Doctor>
        {
            new Doctor{Id =100, Name = "Johnny", Speciality = "Ortho", Password = "johnny", Gender="Male", Email="johnnynaorem@gmail.com"},
            new Doctor{Id =100, Name = "Johnny2", Speciality = "Ortho", Password = "johnny2", Gender="Male", Email="johnnynaorem2@gmail.com"},
            new Doctor{Id =101, Name = "Jaswant", Speciality = "Cardiologists", Password = "jaswant", Gender="Male", Email="jaswant@gmail.com"},
            new Doctor{Id =102, Name = "Lanchenba", Speciality = "Dentist", Password = "lanchenba", Gender="Male", Email="lanchenba@gmail.com"},

        };
        private List<Patient> patients = new List<Patient>
        {
            new Patient{Id =200, Name = "Bon", Password = "bonbon", Address = "Bishnupur Ward No 6", Email= "johnnynaorem@gmail.com", Phone=8787470933, Age=22, Gender="Male"},
            new Patient{Id =201, Name = "Venker", Password = "Venker", Address = "Kakching", Email= "venker@gmail.com", Phone=9008237263, Age=21, Gender="Male"},
        };
        private List<Appointment> appointments = new List<Appointment>();
        //{
        //    new Appointment{StartTime = Convert.ToDateTime("2023-10-31 06:30"), Doctor = doctors[1], Patient = patients[1]  },
        //    new Appointment{StartTime = Convert.ToDateTime("2024-09-17 15:20"), Doctor = doctors[0], Patient = patients[0]  },
        //};

        private void ConsoleColorForPrintList()
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("----------------------------");
            Console.ResetColor();
        }
        private void ViewPatients()
        {
            if (patients.Count > 0)
            {
                foreach (var patient in patients)
                {
                    ConsoleColorForPrintList();
                    Console.ForegroundColor = ConsoleColor.Blue;
                    Console.WriteLine(patient);
                    ConsoleColorForPrintList();
                }
            }
            else
            {
                Console.WriteLine("Empty!!");
            }

        }
        private void ConsoleColorController(string message, System.ConsoleColor color)
        {
            Console.ForegroundColor = color;
            Console.WriteLine(message);
            Console.ResetColor();
        }
        private void ViewAppointments(Doctor doctor)
        {
            bool found = false;

            var appointmentFilter = appointments.Where(i => i.Doctor == doctor);
            if (appointmentFilter.Any())
            {
                found = true;
                foreach (var appointment in appointmentFilter)
                {
                    if ((DateTime.Compare(DateTime.Now, appointment.StartTime)) <= 0)
                    {
                        ConsoleColorForPrintList();
                        Console.WriteLine("Upcomming Appointments: ");
                        ConsoleColorController($"Dr. {appointment.Doctor.Name}, You have a appointment with patient {appointment.Patient.Name} at {appointment.StartTime}", ConsoleColor.Green);
                    }
                    else
                    {
                        ConsoleColorForPrintList();
                        Console.WriteLine("Past Appointments: ");
                        ConsoleColorController($"Dr. {appointment.Doctor.Name}, You already met with patient {appointment.Patient.Name} on {appointment.StartTime}", ConsoleColor.Green);
                        ConsoleColorForPrintList();
                    }
                }
            }
            if (!found)
            {
                ConsoleColorController("Doctor, for now you don't have any appointment", ConsoleColor.Red);
            }
        }
        public void DoctorLogin(int id, string password)
        {
            var doctor = doctors.FirstOrDefault(x => x.Id == id && x.Password == password);
            if (doctor == null)
            {
                ConsoleColorController("Invalid Credentials", ConsoleColor.Red);
                return;
            }
            ConsoleColorController($"Doctor {doctor.Name} Login Successfully", ConsoleColor.Green);
            while (true)
            {
                ConsoleColorController("1. View Patients", ConsoleColor.Blue);
                ConsoleColorController("2. View Appointments", ConsoleColor.Blue);
                ConsoleColorController("3. Logout", ConsoleColor.Blue);
                ConsoleColorController("Select an option: ", ConsoleColor.DarkMagenta);
                var choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        ViewPatients();
                        break;
                    case "2":
                        ViewAppointments(doctor);
                        break;
                    case "3":
                        ConsoleColorController("Logout Successfully.", ConsoleColor.Green);
                        return;
                    default:
                        Console.WriteLine("Invalid option.");
                        break;
                }
            }
        }

        private void DoctorSelectionForBooking()
        {
            ConsoleColorController("Booking", ConsoleColor.Blue);
        }
        private void BookAppointment(string speciality, Patient patient)
        {
            int count = 1;
            var specialistDoctors = doctors.Where(d => d.Speciality == speciality);
            if (!specialistDoctors.Any())
            {
                ConsoleColorController($"Please Enter Valid Speciality", ConsoleColor.Blue);
                return;
            }
            ConsoleColorController($"{speciality} Doctor List....", ConsoleColor.Cyan);
            foreach (var doctor in specialistDoctors)
            {
                ConsoleColorController($"{count}. {doctor.Name}", ConsoleColor.Magenta);
                count++;
            }
            while (true)
            {
                Console.Write("Select Doctor (enter name): ");
                var doctorName = Console.ReadLine();
                var selectedDoctor = specialistDoctors.FirstOrDefault(d => d.Name == doctorName);
                if (selectedDoctor != null)
                {
                    ConsoleColorController($"Booking appointment with Dr. {selectedDoctor.Name}", ConsoleColor.Green);
                    ConsoleColorController("Enter Appointment Date and Time (e.g., 2024-10-01 10:30): ", ConsoleColor.Blue);

                    DateTime appointmentTime;
                    if (!DateTime.TryParse(Console.ReadLine(), out appointmentTime))
                    {
                        ConsoleColorController("Invalid date/time format.", ConsoleColor.Red);
                    }
                    var existingAppointment = appointments.Any(a => a.Doctor == selectedDoctor && a.StartTime == appointmentTime);
                    if (existingAppointment)
                    {
                        ConsoleColorController($"Doctor is already booked at this time ({appointmentTime}).", ConsoleColor.DarkRed);
                        return;
                    }

                    var patientAppointment = appointments.Any(a => a.Patient == patient && a.StartTime == appointmentTime);
                    if (patientAppointment)
                    {
                        ConsoleColorController("You already have an appointment at this time.", ConsoleColor.DarkRed);
                        return;
                    }
                    DateTime appointmentEndTime = appointmentTime.AddMinutes(30);
                    var overlappingAppointment = appointments.Any(a => a.Doctor == selectedDoctor && ((a.StartTime < appointmentEndTime && a.EndTime > appointmentTime)));
                    if (overlappingAppointment)
                    {
                        ConsoleColorController($"Doctor is already booked during this time ({appointmentTime} to {appointmentEndTime}).", ConsoleColor.DarkRed);
                        return;
                    }

                    Appointment newAppointment = new Appointment
                    {
                        StartTime = appointmentTime,
                        Doctor = selectedDoctor,
                        Patient = patient,
                    };
                    appointments.Add(newAppointment);
                    ConsoleColorController($"Appointment booked successfully at {appointmentTime}", ConsoleColor.Green);
                    //appointments.Add(new Appointment(DateTime.Now, selectedDoctor, patient));
                    return;
                }
                else
                {
                    ConsoleColorController("Doctor not found. Try again.", ConsoleColor.Yellow);
                }
                //Console.WriteLine("0. Back");
                //Console.Write("Select an option: ");
                //var choice = Console.ReadLine();

                //switch (choice)
                //{
                //    case "1":
                //        DoctorSelectionForBooking();
                //        break;
                //    case "0":
                //        return;
                //    default:
                //        Console.WriteLine("Invalid option.");
                //        break;
                //}
            }
        }

        public void BookAppointmentsMenu(Patient patient)
        {
            ConsoleColorController("Appointment Booking", ConsoleColor.Blue);
            ConsoleColorController("Available Specialist in our Clinic", ConsoleColor.Magenta);
            int count = 1;
            foreach (var speciality in doctors.Select(d => d.Speciality).Distinct())
            {
                ConsoleColorController($"{count}. {speciality}", ConsoleColor.Magenta);
                count++;
            }
            Console.Write("Select a speciality (enter speciality e.g. Ortho): ");
            var specialityChoice = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(specialityChoice))
            {
                ConsoleColorController($"You selected: {specialityChoice}", ConsoleColor.Green);
                BookAppointment(specialityChoice, patient);
            }
            else
            {
                ConsoleColorController("Invalid input. Please enter a valid specialty.", ConsoleColor.Red);
            }
            //int count = 1;
            //while (true)
            //{
            //    foreach (var doctor in doctors)
            //    {
            //        Console.WriteLine($"{count}. {doctor.Speciality}");
            //        count++;
            //    }

            //    Console.WriteLine("0. Back");

            //    Console.Write("Select an option: ");
            //    var choice = Console.ReadLine();

            //    switch (choice)
            //    {
            //        case "1":
            //            BookAppointment("Ortho");
            //            count = 1;
            //            break;
            //        case "2":
            //            BookAppointment("Cardiologists");
            //            count = 1;
            //            break;
            //        case "3":
            //            BookAppointment("Dentist");
            //            count = 1;
            //            break;
            //        case "0":
            //            return;
            //        default:
            //            Console.WriteLine("Invalid option.");
            //            break;
            //    }
            //}
        }

        public void DoctorRegister(int id, string name, string speciality, string password, string gender, string email)
        {
            try
            {
                var isExistingDoctor = doctors.FirstOrDefault(e => e.Id == id);
                if (isExistingDoctor == null)
                {
                    Doctor doctor = new Doctor(id, name, speciality, password, gender, email);
                    doctors.Add(doctor);
                    ConsoleColorController($"Doctor {name} registered successfully.", ConsoleColor.Green);
                }
                else
                {
                    ConsoleColorController($"ID: {id} already exixting please login....", ConsoleColor.Red);
                }

            }
            catch (Exception ex)
            {
                ConsoleColorController("Error Occur" + ex.Message, ConsoleColor.Red);
            }
        }

        public void PatientRegistor(int id, string name, string password, string address, string email, double phone, int age, string gender)
        {
            try
            {
                var isExistingPatient = patients.FirstOrDefault(e => e.Id == id);
                if (isExistingPatient == null)
                {
                    Patient patient = new Patient(id, name, password, address, email, phone, age, gender);
                    patients.Add(patient);
                    ConsoleColorController($"Patient {name} registered successfully.", ConsoleColor.Green);
                }
                else
                {
                    ConsoleColorController($"ID: {id} already exixting please login....", ConsoleColor.Red);
                }

            }
            catch (Exception ex)
            {
                throw new Exception("Not able to register" + ex.Message);
            }
        }

        private void ViewDoctor()
        {
            if (doctors.Count > 0)
            {
                foreach (var doctor in doctors)
                {
                    ConsoleColorForPrintList();
                    ConsoleColorController($"{doctor}", ConsoleColor.Green);
                    ConsoleColorForPrintList();
                }
            }
            else
            {
                ConsoleColorController("Empty!!", ConsoleColor.Red);
            }
        }

        private void ViewAppointments(Patient patient)
        {
            string address = (patient.Gender == "Male") ? "Mr." : "Ma'am";
            bool found = false;
            var appointmentFilter = appointments.Where(a => a.Patient == patient);
            if (appointmentFilter.Any())
            {
                found = true;
                foreach (var appointment in appointmentFilter)
                {
                    if ((DateTime.Compare(DateTime.Now, appointment.StartTime)) <= 0)
                    {
                        ConsoleColorForPrintList();
                        Console.WriteLine("Upcomming Appointments: ");
                        ConsoleColorController($"{address} {patient.Name}, You have appointment with Dr. {appointment.Doctor.Name} at {appointment.StartTime}", ConsoleColor.Blue);
                        ConsoleColorForPrintList();
                    }
                    else
                    {
                        ConsoleColorForPrintList();
                        Console.WriteLine("Past Appointments: ");
                        Console.WriteLine($"{address} {patient.Name}, You already met with Dr. {appointment.Doctor.Name} on {appointment.StartTime}");
                        ConsoleColorForPrintList();
                    }

                }
            }
            if (!found)
            {
                ConsoleColorController($"{address} {patient.Name}, for now you don't have any appointment", ConsoleColor.Red);
            }
        }
        public void PatientLogin(int id, string password)
        {
            try
            {
                var patient = patients.FirstOrDefault(p => p.Id == id && p.Password == password);
                if (patient == null)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Invalid Credentials");
                    Console.ResetColor();
                    return;
                }
                ConsoleColorController($"Patient {patient.Name} Login Successfully", ConsoleColor.Green);
                while (true)
                {
                    ConsoleColorController("1. View Doctors", ConsoleColor.Cyan);
                    ConsoleColorController("2. View Appointments", ConsoleColor.Cyan);
                    ConsoleColorController("3. Book Appointments", ConsoleColor.Cyan);
                    ConsoleColorController("4. Logout", ConsoleColor.Cyan);
                    ConsoleColorController("Select an option: ", ConsoleColor.Blue);
                    var choice = Console.ReadLine();

                    switch (choice)
                    {
                        case "1":
                            ViewDoctor();
                            break;
                        case "2":
                            ViewAppointments(patient);
                            break;
                        case "3":
                            BookAppointmentsMenu(patient);
                            break;
                        case "4":
                            ConsoleColorController("Logout Successfully", ConsoleColor.Green);
                            return;
                        default:
                            Console.WriteLine("Invalid option.");
                            break;
                    }
                }
            }
            catch (Exception ex)
            {

                ConsoleColorController(ex.Message, ConsoleColor.Red);
            }
        }
    }
}
