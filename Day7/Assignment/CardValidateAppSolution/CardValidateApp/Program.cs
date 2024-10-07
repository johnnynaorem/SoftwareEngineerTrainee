namespace CardValidateApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
                ICardInterface cardValidator = new CardValidator();

                while (true)
                {
                    try
                    {
                        Console.WriteLine("Enter a credit card number to validate (XXXX XXXX XXXX XXXX) (or type 'exit' to quit):");
                        string input = Console.ReadLine();

                        if (input.Equals("exit", StringComparison.OrdinalIgnoreCase))
                        {
                            break;
                        }

                        bool isValid = cardValidator.IsValidCardNumber(input);

                        Console.WriteLine($"Card Number '{input}' is {(isValid ? "Valid" : "Invalid")}");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"An error occurred: {ex.Message}");
                    }
                }
            }
    }
}
