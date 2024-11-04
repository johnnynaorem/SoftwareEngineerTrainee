

namespace AbstractClass
{
    abstract class PaymentMethod
    {
        public string CardNumber { get; set; }

        protected PaymentMethod(string cardNumber)
        {
            CardNumber = cardNumber;
        }

        // Abstract method
        public abstract void ProcessPayment(decimal amount);

        // Concrete method
        public void DisplayCardInfo()
        {
            Console.WriteLine($"Processing payment from card: {CardNumber}");
        }
    }
}
