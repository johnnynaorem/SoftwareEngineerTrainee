using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day6Task
{
    internal interface ISupplierServices
    {
        public bool AddProduct(Product product, int sid);
        void GetAllSuppliers();
        void GetAllCustomers();
        public bool UpdateProduct(int pid, int sid);
    }
}
