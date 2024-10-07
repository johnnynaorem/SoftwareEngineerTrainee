using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FactorMethod.Products
{
    internal class Saving : BankAcccount
    {
        public string AcctHolderName { get; set; }
        public string AcctNumber { get; set; }
        public double Balance { get; set ; }


        public void CreateAccount(string acctNumber, string acctHolderName)
        {
            AcctHolderName = acctHolderName;
            AcctNumber = acctNumber;
        }


        public void DepositAccount(double depositAmount)
        {
            Balance += depositAmount;
        }

        public void ShowBalance()
        {
            Console.WriteLine($"Name: {AcctHolderName} AcctNumber: {AcctNumber} Balance: {Balance}");
        }
    }
}
