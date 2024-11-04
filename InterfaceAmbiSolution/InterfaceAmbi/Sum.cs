using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterfaceAmbi
{
    public class Sum : Interface1, Interface2
    {
        //public Sum()
        //{
        //    Interface1.Print();
        //}
        void Interface1.Display()
        {
            Console.WriteLine("InterFace1");
        }

        void Interface2.Display()
        {
            Console.WriteLine("InterFace2");
        }

        int Interface2.sum(int x, int y)
        {
            int z = x + y;
            Console.WriteLine(z);
            return z;
        }

        int Interface1.sum(int x, int y)
        {
            int z = x + y;
            Console.WriteLine(z);
            return z;
        }
    }
}
