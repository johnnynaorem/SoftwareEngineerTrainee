using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day7Task
{
    public class Customer
    {
        public string? Name { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string? Gender { get; set; }


        public virtual void TakeCustomerDetailsFromConsole()
        {
            Console.WriteLine("Enter Customer Name: ");
            Name = Console.ReadLine();
            Console.WriteLine("Enter Customer DOB: ");
            DateOfBirth = Convert.ToDateTime(Console.ReadLine());
            Console.WriteLine("Enter Customer Gender: ");
            Gender = Console.ReadLine();
        }
    }
}
