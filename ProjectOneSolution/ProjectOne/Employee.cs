using FirstApp;

namespace FirstApp
{
    internal class Employee
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Designation { get; set; } = string.Empty;
        public double Salary { get; set; }
        public DateTime DateOfBirth { get; set; }
        public int TotalLeave { get; set; }

        public Employee() {
            Id = 1;
            Name = "Johnny Naorem"; 
            Designation = "Software Engineer";
            Salary = 30000;
            DateOfBirth = new DateTime(2002, 02, 28);
            TotalLeave = 4;
        }
        public void DoWork()
        {
            Console.WriteLine("Employee does his work");
        }
        public void TakeLeave(int NoOfLeaves)
        {
            Console.WriteLine($"{Name} needs leave {NoOfLeaves} days");
            if (TotalLeave > 0)
            {
                TotalLeave -= NoOfLeaves;
                Console.WriteLine($"Employee takes leave. Remaining {TotalLeave} out of {totalNoOfLeave}");
            }
            else
            {
                Console.WriteLine("Employee has no leave balance");
            }
        }

    }
}

