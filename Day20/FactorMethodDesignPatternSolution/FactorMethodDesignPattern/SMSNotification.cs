using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FactorMethodDesignPattern
{
    internal class SMSNotification : INotification
    {
        public void Notify(string message)
        {
            Console.WriteLine($"SMS notification: {message}");
        }
    }
}
