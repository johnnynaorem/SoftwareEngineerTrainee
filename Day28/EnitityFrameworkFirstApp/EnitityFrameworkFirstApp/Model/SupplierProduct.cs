using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnitityFrameworkFirstApp.Model
{
    internal class SupplierProduct
    {
        [Key] //Notation for primary key

        public int PurchaseNumber { get; set; }
        public int SupplierID { get; set; }
        [ForeignKey("SupplierID")]
        public Supplier Supplier { get; set; }

        public int ProductID { get; set; }
        [ForeignKey("ProductID")]

        public Product Product { get; set; }
    }
}
