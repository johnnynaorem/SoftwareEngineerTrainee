using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LongestShortestProject
{
    internal class Word
    {
        public string input {  get; set; }

        public void TakeInputFromConsole()
        {
            bool flag = true;
            while (flag)
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
                    
                    flag = false;
                }
                catch (ArgumentException ex)
                {
                    Console.WriteLine($"Error: {ex.Message}");
                }
            }
        }
    }
}
