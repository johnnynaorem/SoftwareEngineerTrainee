namespace PrimeNumber
{
    internal class Program
    {
        static void Main(string[] args)
        {
            List<int> primeNumbers = new List<int>();
            int number;

            Console.WriteLine("Enter numbers one by one. Enter 0 to stop:");

            while (true)
            {
                try
                {
                    string input = Console.ReadLine();

                    if (!int.TryParse(input, out number))
                    {
                        throw new FormatException("Input is not a valid integer.");
                    }

                    if (number == 0)
                    {
                        break;
                    }

                    if (IsPrime(number))
                    {
                        primeNumbers.Add(number);
                    }
                }
                catch (FormatException ex)
                {
                    Console.WriteLine($"Error: {ex.Message}");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Unexpected error: {ex.Message}");
                }
            }

            Console.WriteLine("Prime numbers entered:");
            foreach (var prime in primeNumbers)
            {
                Console.WriteLine(prime);
            }
        }

        static bool IsPrime(int number)
        {
            if (number <= 1) return false;
            if (number == 2) return true;
            if (number % 2 == 0) return false;

            int maxDivisor = (int)Math.Sqrt(number);

            for (int i = 3; i <= maxDivisor; i += 2)
            {
                if (number % i == 0)
                {
                    return false;
                }
            }

            return true;
        }
    }
}
