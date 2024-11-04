using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbstractClass.Device
{
    class Laptop : IConnectable
    {
        public void Connect()
        {
            Console.WriteLine("Laptop is connected to the network.");
        }

        public void Disconnect()
        {
            Console.WriteLine("Laptop is disconnected from the network.");
        }
    }
}
