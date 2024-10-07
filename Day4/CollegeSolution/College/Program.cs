using System.Xml.Linq;

namespace College
{
    internal class Program
    {
        void ManageStudent()
        {
            Student student = new Student();
            student.Id = 10;
            student.Name = "Rohit Laishram";
            student.DateOfBirth = new DateTime(2003, 04, 15);
            student.MobileNumber = 7642833064;
            student.Email = "bonbon@gmail.com";
            student.DisplayEmployee();
        }
        static void Main(string[] args)
        {
           Program program = new Program();
            program.ManageStudent();
        }
    }
}
