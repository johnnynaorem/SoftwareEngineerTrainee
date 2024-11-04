using FactorMethod.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FactorMethod
{
    public class CurrentFactory : BankAccountFactory
    {
        protected override BankAcccount MakeProduct()
        {
            BankAcccount acc = new Current();
            return acc;
        }
    }
}