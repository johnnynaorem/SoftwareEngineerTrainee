using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day7Class
{
    public class Calculator : ICalculatorInterface
    {
        public Numbers Numbers { get; set; }
        public Calculator()
        {
            Numbers = new Numbers();
        }
        void PrintResult(string opetration, double result)
        {
            Console.WriteLine($"The result of {opetration} on {Numbers} is {result}");
        }
        public void Add()
        {
            Numbers.TakeNumbersFromConsole();
            double result = Numbers.Number1 + Numbers.Number2;
            PrintResult("Addition", result);
        }

          
        public void Subtract()
        {
            Numbers.TakeNumbersFromConsole();
            double result = Numbers.Number1 - Numbers.Number2;
            PrintResult("Subtraction", result);
        }

        public void Division()
        {
            Numbers.TakeNumbersFromConsole();
            double result = Numbers.Number1 / Numbers.Number2;
            PrintResult("Division", result);
        }

        public void Multiplication()
        {
            Numbers.TakeNumbersFromConsole();
            double result = Numbers.Number1 * Numbers.Number2;
            PrintResult("Multiplication", result);
        }
    }
}
