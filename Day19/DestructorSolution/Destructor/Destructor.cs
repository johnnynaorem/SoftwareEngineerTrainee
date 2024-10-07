using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Destructor
{
    public class Destructor:IDisposable
    {
        public int Id { set; get; }
        public Destructor() {
            Id = 0;
            Console.WriteLine("Constructor");
        }

        public void SetValue(int value)
        {
            Id = value;
        }

        public void Display()
        {
            Console.WriteLine(Id);
        }

        public void Dispose()
        {
            Console.WriteLine("Dispose");
        }

        ~Destructor() {
            Console.WriteLine("Destructor");
        }
    }
}
