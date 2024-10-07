using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingInterface
{
    internal class NRIbank : IBankInterface
    {
        double balance;
        const double MinBalance = 10000;
        const double TransactionChargePercent = 0.02;

        public NRIbank(double initialBalance)
        {
            if (initialBalance < MinBalance)
            {
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
            Console.WriteLine($"Transaction Failed! Your balance is below the minimum required balance of \"{MinBalance}\".");
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
