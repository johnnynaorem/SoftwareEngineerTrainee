namespace EmployeeAppSolution
{
    public class EmployeeServices
    {
        Dictionary<int, Employee> employees = new Dictionary<int, Employee>();
        List<Employee> employeeList = new List<Employee>();
        public void TakeEmployeeDetailsAndStore()
        {
            int choice = 0;
            do
            {
                try
                {
                    Employee employee = new Employee();
                    employee.GetEmployeeDetailsFromConsole();
                    employee.Id = employees.Count + 100;
                    employees.Add(employee.Id, employee);
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("Employee added successfully.");
                    Console.ResetColor();
                }
                catch (Exception ex)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"Error: {ex.Message}");
                    Console.ResetColor();
                }

                Console.WriteLine("Do you want to continue? Enter any number other than 0 to continue, or 0 to stop.");
                try
                {
                    choice = Convert.ToInt32(Console.ReadLine());
                }
                catch (FormatException)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Invalid input. Please enter a number.");
                    Console.ResetColor();
                    choice = -1;
                }
            } while (choice != 0);
        }

        public void FindAndPrintEmployeeById()
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write("Enter Employee ID to Find: ");
            Console.ResetColor();
            int id;
            try
            {
                id = Convert.ToInt32(Console.ReadLine());
                if (employees.ContainsKey(id))
                {
                    Employee employee = employees[id];
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("Employee found:");
                    Console.WriteLine(employee);
                    Console.ResetColor();
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Employee with ID {0} not found.", id);
                    Console.ResetColor();
                }
            }
            catch (FormatException)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Invalid ID format. Please enter a numeric ID.");
                Console.ResetColor();
            }
        }

        public void FindEmployeeByName()
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write("Enter Employee Name to Search: ");
            Console.ResetColor();
            string name = Console.ReadLine().Trim();

            bool found = false;

            try
            {
                foreach (var employee in employees.Values)
                {
                    if (employee.Name == name)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("------------------------------------------");
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine(employee);
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("------------------------------------------");
                        Console.ResetColor();
                        found = true;
                    }
                }

                if (!found)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"No employees found with the name {name}");
                    Console.ResetColor();
                }
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"An error occurred while searching: {ex.Message}");
                Console.ResetColor();
            }
        }

        public void FindEmployeeByNameAndPrintOlder()
        {
            if (employees.Count != 0)
            {
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.Write("Enter Employee Name to Search: ");
                Console.ResetColor();
                string name = Console.ReadLine().Trim();

                bool found = false;

                try
                {
                    //    foreach (var employee in employees.Values)
                    //    {
                    //        if (employee.Name == name)
                    //        {
                    //            targetEmployee = employee;
                    //            found = true;
                    //        }
                    //    }
                    var employee = employeeList.FirstOrDefault(e => e.Name == name);
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.WriteLine($"Employee List Older than {name}");
                    //foreach (var employee in employees.Values)
                    //{
                    //    if (targetEmployee.Age < employee.Age)
                    //    {
                    //        Console.ForegroundColor = ConsoleColor.Red;
                    //        Console.WriteLine("------------------------------------------");
                    //        Console.ForegroundColor = ConsoleColor.Green;
                    //        Console.WriteLine(employee);
                    //        Console.ResetColor();
                    //        Console.ForegroundColor = ConsoleColor.Red;
                    //        Console.WriteLine("------------------------------------------");
                    //        Console.ResetColor();
                    //    }
                    //}

                    var targetEmployee = employeeList.Where(e => e.Age > employee.Age);
                    if (targetEmployee != null) { 
                        found = true;
                    }

                    if (!found)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine($"No employees found with the name {name}");
                        Console.ResetColor();
                    }
                }
                catch (Exception ex)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"An error occurred while searching: {ex.Message}");
                    Console.ResetColor();
                }
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Empty!!!");
            }
            
            
        }

        public void PrintEmployee()
        {
            if (employees.Count == 0)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("No employees to display.");
                Console.ResetColor();
                return;
            }
            Console.WriteLine("Employees List.");
            Console.ForegroundColor = ConsoleColor.Cyan;
            foreach (var item in employees.Values)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("------------------------------------------");
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine(item);
                Console.ResetColor();
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("------------------------------------------");
                Console.ResetColor();
            }
            Console.ResetColor();
        }

        public void DeleteEmployee()
        {
            try
            {
                if (employees.Count == 0)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Empty!!!!!");
                    Console.ResetColor();
                    return;
                }
                Console.WriteLine("Enter the Id of Employee which You want to delete: ");
                int id = Convert.ToInt16(Console.ReadLine());
                bool found = false;
                foreach (var item in employees)
                {
                    if (id == item.Value.Id)
                    {
                        employees.Remove(id);
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine($"Employee with ID {id} is Deleted Successfully");
                        found = true;
                    }
                }
                if (!found)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Employee Not Found");
                }
            }
            catch (FormatException ex) {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Invalid ID format. Please enter a numeric ID.");
                Console.ResetColor();
            }
        }

        public void ModifyEmployeeDetails()
        {

            if (employees.Count > 0)
            {
                
                try
                {
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.Write("Enter Employee ID to Modify: ");
                    Console.ResetColor();
                    int id;
                    id = Convert.ToInt32(Console.ReadLine());
                    if (employees.ContainsKey(id))
                    {
                        Employee employee = employees[id];
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.WriteLine("Employee found:");
                        Console.WriteLine(employee);
                        Console.ResetColor();

                        Console.WriteLine("Which field do you want to modify?");
                        Console.WriteLine("1. Name");
                        Console.WriteLine("2. Salary");
                        Console.WriteLine("3. Age");
                        Console.Write("Enter your choice (1-3): ");

                        int fieldChoice;
                        try
                        {
                            fieldChoice = Convert.ToInt32(Console.ReadLine());
                            switch (fieldChoice)
                            {
                                case 1:
                                    Console.Write("Enter new Name: ");
                                    employee.Name = Console.ReadLine().Trim();
                                    break;
                                case 2:
                                    Console.Write("Enter new Salary: ");
                                    try
                                    {
                                        employee.Salary = Convert.ToDecimal(Console.ReadLine());
                                    }
                                    catch (FormatException)
                                    {
                                        Console.ForegroundColor = ConsoleColor.Red;
                                        Console.WriteLine("Invalid salary format. Please enter a numeric value.");
                                        Console.ResetColor();
                                    }
                                    break;
                                case 3:
                                    Console.Write("Enter new Age: ");
                                    try
                                    {
                                        employee.Age = Convert.ToInt32(Console.ReadLine());
                                    }
                                    catch (FormatException)
                                    {
                                        Console.ForegroundColor = ConsoleColor.Red;
                                        Console.WriteLine("Invalid age format. Please enter a numeric value.");
                                        Console.ResetColor();
                                    }
                                    break;
                                default:
                                    Console.ForegroundColor = ConsoleColor.Red;
                                    Console.WriteLine("Invalid choice. No modifications were made.");
                                    Console.ResetColor();
                                    return;
                            }
                            employees[id] = employee;
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.WriteLine("Employee details updated successfully.");
                            Console.ResetColor();
                        }
                        catch (FormatException)
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("Invalid input. Please enter a valid number.");
                            Console.ResetColor();
                        }
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Employee with ID {0} not found.", id);
                        Console.ResetColor();
                    }
                }
                catch (FormatException)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Invalid ID format. Please enter a numeric ID.");
                    Console.ResetColor();
                }
            }
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Empty!!!");
            Console.ResetColor();
        }
        

        public void SortingEmployeeList()
        {
            try
            {
                employeeList = employees.Values.ToList();
                employeeList.Sort();

                if (employeeList.Count == 0)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("No employees to sort.");
                    Console.ResetColor();
                    return;
                }

                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("Sorted Employees by Salary (Ascending):");
                Console.ResetColor();
                foreach (var item in employeeList)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("------------------------------------------");
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine(item);
                    Console.ResetColor();
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("------------------------------------------");
                }
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"An error occurred while sorting: {ex.Message}");
                Console.ResetColor();
            }
        }


    }
}
