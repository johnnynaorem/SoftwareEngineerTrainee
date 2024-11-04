using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbstractClass.Device
{
    class Printer : IConnectable
    {
        public void Connect()
        {
            Console.WriteLine("Printer is connected to the network.");
        }

        public void Disconnect()
        {
            Console.WriteLine("Printer is disconnected from the network.");
        }
    }
}
