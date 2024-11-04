using AbstractClass;
using AbstractClass.Device;

class Program
{
    static void Main()
    {
        //Abstract
        //PaymentMethod creditCard = new CreditCard("1234-5678-9876-5432");
        //creditCard.DisplayCardInfo();
        //creditCard.ProcessPayment(100.00m);

        //PaymentMethod payPal = new PayPal("johnnynaorem7@paypal");
        //payPal.DisplayCardInfo();
        //payPal.ProcessPayment(50.00m);


        //Interface
        IConnectable printer = new Printer();
        printer.Connect();
        printer.Disconnect();

        IConnectable laptop = new Laptop();
        laptop.Connect();
        laptop.Disconnect();
    }
}
