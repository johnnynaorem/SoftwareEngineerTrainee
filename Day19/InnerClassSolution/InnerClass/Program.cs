namespace InnerClass
{
    internal class Program
    {
        public void TestEmployee()
        {
            Employee employee = new Employee();
            Console.WriteLine(employee.Name);
            if (employee.AvailStickLeave(8))
            {
                Console.WriteLine("Leave Approved");
            }
            else
            {
                Console.WriteLine("Leave Rejected");
            }

        }
        static void Main(string[] args)
        {
            Program program = new Program();
            program.TestEmployee(); 
        }
    }
}
