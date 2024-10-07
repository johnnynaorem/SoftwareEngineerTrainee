using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingInterface
{
    internal interface IBankInterface
    {
        public bool MoneyTransaction(double amount);
        public bool hasMinimumBalance();
        public double getBalance();
    }
}
