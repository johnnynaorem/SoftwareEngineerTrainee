using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OperatorOverloading
{
    internal class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Price { get; set; }

        public static Product operator +(Product a, Product b)
        {
            Product product = new Product();
            product.Id = a.Id;
            product.Name = a.Name + " " + b.Name;
            product.Price = a.Price + b.Price;
            return product;
        }

        public override string ToString() { 
            return "Id: " + Id + "Name: " + Name + "Price: " + Price ;
        }
    }
}
