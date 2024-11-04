using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbstractClass
{
    class CreditCard : PaymentMethod
    {
        public CreditCard(string cardNumber) : base(cardNumber) { }

        public override void ProcessPayment(decimal amount)
        {
            Console.WriteLine($"Processing credit card payment of {amount:C}");
        }
    }
}
