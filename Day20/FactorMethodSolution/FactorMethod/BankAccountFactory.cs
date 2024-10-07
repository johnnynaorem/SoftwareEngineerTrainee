using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FactorMethod
{
    public abstract class BankAccountFactory
    {
        protected abstract BankAcccount MakeProduct();
        
        public BankAcccount CreateProduct()
        {
            //Call the MakeProduct which will create and return the appropriate object 
            BankAcccount bankAcccount = MakeProduct();
            //Return the Object to the Client
            return bankAcccount;
        }
    }
}
