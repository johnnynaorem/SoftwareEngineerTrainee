namespace CreatingApi
{
    internal class Program
    {
        Employee CreateEmployee()
        {
            var employee = new Employee();
            Console.Write("Enter ID: ");
            employee.Id = Convert.ToInt32(Console.ReadLine());

            Console.Write("Enter Name: ");
            employee.Name = Console.ReadLine()??"";

            Console.Write("Enter Date of Birth (yyyy-MM-dd): ");
            employee.DateOfBirth = Convert.ToDateTime(Console.ReadLine());

            Console.Write("Enter Mobile Number: ");
            employee.MobileNumber = Convert.ToInt64(Console.ReadLine());

            Console.Write("Enter Email: ");
            employee.Email = Console.ReadLine()??"";

            return employee;
        }
        static void Main(string[] args)
        {
            //var employee = new Employee();
            //var employee = new Employee(7, "Johnny", new DateTime(2002, 02, 28), 8787470933, "johnnynaorem7@gmail.com");
            //Issue issue1 = new Issue(101, "No Power", "There is no Power in Office", 7);
            //issue1.AssignIssue(2001);
            //issue1.ChangeStatus("Closed");
            Program program = new Program();
            var employee1 = program.CreateEmployee();
            employee1.DisplayEmployee();
            //issue1.PrintIssueDetails();
            //Console.WriteLine(issue1);
        }
    }
}
