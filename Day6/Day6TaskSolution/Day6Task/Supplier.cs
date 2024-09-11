using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Day6Task
{
    internal class Supplier
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;

        public Supplier() {
            //Id = 1901;
            //Name = "Manij Pande";
        }
        public override string ToString()
        {
            return $"Id: {Id}, Name: {Name}";
        }

    }

}
