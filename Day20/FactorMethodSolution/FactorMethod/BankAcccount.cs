using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FactorMethod
{
    public interface BankAcccount
    {
        void CreateAccount(string acctNumber, string acctHolderName);
        void DepositAccount(double depositAmount);
        void ShowBalance();
        //void AccountType();
    }
}
