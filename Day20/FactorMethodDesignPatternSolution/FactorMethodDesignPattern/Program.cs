namespace FactorMethodDesignPattern
{
    public class Program
    {
        static void Main(string[] args)
        {
            NotificationFactory emailFactory = new EmailFactory();
            INotification emailNotification = emailFactory.CreateNotification();
            emailNotification.Notify("This is an Email notification");

            NotificationFactory smsFactory = new SMSFactory();
            INotification smsNotification = smsFactory.CreateNotification();
            smsNotification.Notify("This is a SMS notification");
        }
    }
}
