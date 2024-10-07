namespace DeligateUnderstanding
{
    //public class Employee
    //{
    //    public int ID { get; set; }
    //    public string Name { get; set; }
    //    public int Salary { get; set; }
    //    public override string ToString()
    //    {
    //        return "Id = " + ID + ", Name = " + Name + ", Salary = " + Salary;
    //    }
    //}
    public class Predicate
    {
        public Predicate() {
            UnderstandingDeligate();
        }
        void UnderstandingDeligate()
        {
            List<Employee> employees = new List<Employee>()
            {
                new Employee { ID = 101, Name = "Mark", Salary = 5000 },
                new Employee { ID = 102, Name = "John", Salary = 10000 },
                new Employee { ID = 103, Name = "Smith", Salary = 14000 },
                new Employee { ID = 104, Name = "Watson", Salary = 15000 }
            };
            Console.WriteLine("Enter the name of employee to check");
            string name = Console.ReadLine();

            //Predicate<Employee> employeePredicate = new Predicate<Employee>(e=> e.Name == name);
            ////Employee employee = employees.Find(new Predicate<Employee>(e => e.Name == name));
            //Employee employee = employees.Find(employeePredicate);
            var employee = employees.FirstOrDefault(e=> e.Name == name);
            if (employee != null)
                Console.WriteLine(employee);
            else
                Console.WriteLine("employee not found");
        }
        //public bool FindEmployee(Employee employee)
        //{
        //    return employee.Name == "John";
        //}
        //static void Main(string[] args)
        //{
        //    new Predicate();

        //}
    }
}
