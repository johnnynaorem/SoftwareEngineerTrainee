using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day7Task
{
    internal class CustomerValidate : ICustomerInterface
    {
        //public Customer Customer { get; set; }

        //public CustomerValidate()
        //{
        //    Customer = new Customer();
        //}
        //public void Validate()
        //{
        //    try
        //    {
        //        Customer.TakeCustomerDetailsFromConsole();
        //        Console.WriteLine(Customer.DateOfBirth);
        //        //age = DateTime.Now.Year - age;
        //        //if (age >= 18)
        //        //{
        //        //    Console.WriteLine($"Hey {Customer.Name}, yours age is {age}, so you are eligible for vote....");
        //        //    return;
        //        //}
        //        //Console.WriteLine($"Hey {Customer.Name}, yours age is {age}, so you are not eligible for vote....");
        //    }
        //    catch (Exception ex) {
        //        Console.WriteLine("Error");
        //    }
        //}
        int CalculateAge(DateTime dateOfBirth)
        {
            var diff = DateTime.Now - dateOfBirth;
            return (diff.Days / 365);
        }


        private void PrintResult(Customer customer, int age)
        {
            var salutation = customer.Gender == "M" ? "Mr" : "Ms";
            if (age < 18)
            {
                Console.WriteLine($"Dear {salutation}. {customer.Name} you are {age} years old and so not eligible to vote.");
            }
            else
            {
                Console.WriteLine($"Dear {salutation}. {customer.Name} you are {age} years old and so you are eligible to vote.");
            }
        }

        public void Validate(Customer customer)
        {
            var age = CalculateAge(customer.DateOfBirth);
            PrintResult(customer, age);
        }
    }
}
