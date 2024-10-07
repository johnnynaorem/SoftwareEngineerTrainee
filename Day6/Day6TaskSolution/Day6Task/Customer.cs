using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Day6Task
{
    internal class Customer
    {
        public int Id { get; set; }
        public string Name { get; set; } = String.Empty;
        public long Phone {  get; set; }

        public Customer()
        {
            //Id = 1001;
            //Name = "Johnny Naorem";
            //Phone = 8787470933;
        }

        public override string ToString()
        {
            return $"Id: {Id}, Name: {Name}, Phone: {Phone}";
        }
    }
}
