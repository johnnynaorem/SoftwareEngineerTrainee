using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StaticConstructor
{
    internal class Company
    {
         public static int NumberOfEmployee { get; set; }

        static Company()
        {
            NumberOfEmployee = 10;
        }
        public Company() {
            NumberOfEmployee = 10;
        }
    }
}
