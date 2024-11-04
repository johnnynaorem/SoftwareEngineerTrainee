using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnitityFrameworkFirstApp.Model
{
    internal class Category
    {
        public int CategoryID { get; set; }
        public string CategoryName { get; set; } = string.Empty;
        public string CategoryDescription { get; set; } = string.Empty;
        public IEnumerable<Product> Products { get; set; } //Navigation property will be included in the database
    }
}
