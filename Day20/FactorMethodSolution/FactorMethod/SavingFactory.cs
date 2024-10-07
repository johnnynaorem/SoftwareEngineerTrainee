using FactorMethod.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FactorMethod
{
    public class SavingFactory : BankAccountFactory
    {
        protected override BankAcccount MakeProduct()
        {
            BankAcccount acc = new Saving();
            return acc;
        }
    }
}
