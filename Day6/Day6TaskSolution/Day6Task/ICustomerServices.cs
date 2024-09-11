using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day6Task
{
    internal interface ICustomerServices
    {
        public bool BuyProducts(int pid);
        public void GetAllProducts();
    }
}
