using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnderstandingAbstactClass
{
    public class CurrentAccount : BankAccount
    {
        private double _overdraftLimit = 1000.0;

        public CurrentAccount(string accountNumber, string accountHolder)
            : base(accountNumber, accountHolder) { }

        public override void Deposit(double amount)
        {
            Balance += amount;
            Console.WriteLine($"Deposited ${amount} into Current Account.");
        }

        public override void Withdraw(double amount)
        {
            if ((Balance - amount) >= -_overdraftLimit)
            {
                Balance -= amount;
                Console.WriteLine($"Withdrew ${amount} from Current Account.");
            }
            else
            {
                Console.WriteLine("Withdrawal exceeds overdraft limit.");
            }
        }
    }
}
