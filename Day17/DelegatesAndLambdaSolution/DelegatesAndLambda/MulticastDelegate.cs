using System;

namespace DelegatesAndLambda
{
    public class MulticastDelegate
    {
        public delegate void NotifyDelegate();

        public void NotifyByEmail()
        {
            Console.WriteLine("Notification sent via Email.");
        }

        public void NotifyBySMS()
        {
            Console.WriteLine("Notification sent via SMS.");
        }

        public void RaiseNotifications()
        {
            
            NotifyDelegate notify = NotifyByEmail;
            notify += NotifyBySMS;
            notify();
        }
    }
}
