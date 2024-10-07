using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepeatingVowel
{
    internal class WordInput
    {
        public static string GetInputFromConsole()
        {
            string input;
            while (true)
            {
                try
                {
                    Console.WriteLine("Enter words separated by commas:");
                    input = Console.ReadLine();

                    if (string.IsNullOrWhiteSpace(input))
                    {
                        throw new ArgumentException("Input cannot be empty or whitespace.");
                    }

                    if (!input.Contains(","))
                    {
                        throw new ArgumentException("Input must contain commas to separate words.");
                    }

                    return input;
                }
                catch (ArgumentException ex)
                {
                    Console.WriteLine($"Error: {ex.Message}");
                }
            }
        }
    }
}
