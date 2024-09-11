using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day6Task
{
    internal class Product
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public int Price { get; set; }
        public int Quantity { get; set; }


        public Product()
        {
            //Id = 07;
            //Name = "MSI Bravo 15 Gaming Laptop";
            //Price = 80000;
            //Quantity = 10;

        }

        public override string ToString()
        {
            return $"Id: {Id}, Name: {Name}, Price: {Price}, Quantity: {Quantity}";
        }
    }
}
