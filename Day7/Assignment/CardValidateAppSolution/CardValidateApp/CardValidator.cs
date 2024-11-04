using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardValidateApp
{
    public class CardValidator : ICardInterface
    {
        public bool IsValidCardNumber(string cardNumber)
        {
            try
            {
                if (!HasValidFormat(cardNumber))
                {
                    Console.WriteLine("Error: Card number must be in the format 'XXXX XXXX XXXX XXXX'.");
                    return false;
                }

                string cleanCardNumber = CleanCardNumber(cardNumber);

                if (!HasValidLength(cleanCardNumber))
                {
                    Console.WriteLine("Error: Card number must be exactly 16 digits.");
                    return false;
                }

                if (!ContainsOnlyDigits(cleanCardNumber))
                {
                    Console.WriteLine("Error: Card number must contain only digits.");
                    return false;
                }

                if (!IsLuhnValid(cleanCardNumber))
                {
                    Console.WriteLine("Error: Card number is not valid according to the Luhn algorithm.");
                    return false;
                }

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An unexpected error occurred during validation: {ex.Message}");
                return false;
            }
        }

        private bool HasValidFormat(string cardNumber)
        {
            string[] parts = cardNumber.Split(' ');

            if (parts.Length != 4)
                return false;

            return parts.All(part => part.Length == 4 && part.All(char.IsDigit));
        }

        private string CleanCardNumber(string cardNumber) =>
            cardNumber.Replace(" ", "").Replace("-", "");

        private bool HasValidLength(string cardNumber) =>
            cardNumber.Length == 16;

        private bool ContainsOnlyDigits(string cardNumber) =>
            cardNumber.All(char.IsDigit);

        private bool IsLuhnValid(string cardNumber)
        {
            int totalSum = CalculateLuhnSum(cardNumber);
            return totalSum % 10 == 0;
        }

        private int CalculateLuhnSum(string cardNumber)
        {
            return cardNumber
                .Reverse()
                .Select((digit, index) => (index % 2 == 1 ? MultiplyByTwo(digit) : (int)char.GetNumericValue(digit)))
                .Sum();
        }

        private int MultiplyByTwo(char digit)
        {
            int doubled = (int)char.GetNumericValue(digit) * 2;
            return doubled > 9 ? doubled - 9 : doubled;
        }
    }
}
