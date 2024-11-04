using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingInterface
{
    internal class SalaryBank : IBankInterface
    {
        double balance;

        public SalaryBank(double amount)
        {
            balance = amount;
        }

        public bool MoneyTransaction(double amount)
        {
            if (balance - amount >= 0)
            {
                balance -= amount;
                return true;
            }
            return false;
        }

        public bool hasMinimumBalance()
        {
            return true;
        }

        public double getBalance()
        {
            return balance;
        }
    }
}
