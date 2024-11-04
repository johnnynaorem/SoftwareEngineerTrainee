using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingInterface
{
    internal class NormalBank : IBankInterface
    {
        public double balance;
        public const double MinBalance = 5000;
        public const double TransactionChargePercent = 0.001;


        public NormalBank(double initialBalance)
        {
            if (initialBalance < MinBalance) {
                balance = MinBalance;
                return;
            }
            balance = initialBalance;
        }

        public bool MoneyTransaction(double amount)
        {
            double transactionCharge = amount * TransactionChargePercent;
            double totalDeduction = amount + transactionCharge;

            if (balance - totalDeduction >= MinBalance)
            {
                balance -= totalDeduction;
                return true;
            }
            Console.WriteLine($"Transaction Failed!! Your Balance is less than Minimum Balance {MinBalance}");
            return false;
        }
        public bool hasMinimumBalance()
        {
            return balance >= MinBalance;
        }

        public double getBalance()
        {
            return balance;
        }
    }
}
