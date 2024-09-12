using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day7Task
{
    public class PremiumCustomer : Customer
    {
        public string Membership { get; set; }
        public PremiumCustomer()
        {
            Membership = "Gold";
        }
        public override void TakeCustomerDetailsFromConsole()
        {
            try
            {
                base.TakeCustomerDetailsFromConsole();//call the base class method
                Console.WriteLine("Enter the membership type:");
                Membership = Console.ReadLine() ?? "Gold";
            }
            catch (Exception ex)
            {
                throw new InvalidInputDetailException();
            }
        }
    }
}
