using FirstApp;

namespace ProjectOne
{
    class Program
    {
        int inputNumber()
        {
            Console.WriteLine("Enter number: ");
            int num = Convert.ToInt16(Console.ReadLine());
            return num;
        }


        void printLarger ()
        {
            int num1 = inputNumber();
            int num2 = inputNumber();
            Console.WriteLine($"The Larger Number is: {Math.Max(num1, num2)}");
        }

        void ManageEmployee()
        {
            Employee employee = new Employee();//new will allocate memory, Then we can see the call for constructor
            //employee.Id = 1;
            //employee.Name = "John";
            //employee.Designation = "Manager";
            //employee.Salary = 10000;
            //employee.DateOfBirth = new DateTime(1990, 1, 1);
            //employee.TotalLeave = 2;
            //Console.WriteLine($"{employee.Name} needs leave");
            employee.TakeLeave(2);
            //Console.WriteLine($"{employee.Name} needs one more leave - 2");
            employee.TakeLeave(1);
            //Console.WriteLine($"{employee.Name} needs one more leave - 3");
            employee.TakeLeave(1);
        }


        static void Main(string[] args)
        {
            Program program = new Program();
            //program.Calculate();
            program.ManageEmployee();
        }
    }
}
