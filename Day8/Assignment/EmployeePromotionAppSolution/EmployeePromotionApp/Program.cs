namespace EmployeePromotionApp
{
    internal class Program
    {
        //Employee employee;
        EmployeePromotion employeePromotion;

        public Program()
        {
            //employee = new Employee();
            employeePromotion = new EmployeePromotion();
        }
        static void Main(string[] args)
        {
            Program program = new Program();
            program.employeePromotion.AddEmployeeNames();
            //program.employeePromotion.TestCapacity();
            //program.employeePromotion.DisplayEmployees();
            program.employeePromotion.FindPosition();
            program.employeePromotion.CapacityOfList();
            program.employeePromotion.PromotedEmployee();
        }
    }
}
