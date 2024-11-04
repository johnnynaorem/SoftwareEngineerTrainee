

namespace AbstractClass
{
    class PayPal : PaymentMethod
    {
        public PayPal(string email) : base(email) { }

        public override void ProcessPayment(decimal amount)
        {
            Console.WriteLine($"Processing PayPal payment of {amount:C}");
        }
    }
}
