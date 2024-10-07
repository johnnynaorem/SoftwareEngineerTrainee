using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterfaceAmbi
{
    internal interface Interface1
    {
        public int sum(int x, int y);
        public void Display();

        public void Print()
        {
            Console.WriteLine("Print from Interface1");
        }
    }
}
