using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DelegatesAndLambda
{
    public class Lambda
    {
        List<int> numbers = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };

        public void GetEvenNumber() {
            //List<int> evenNumbers = new List<int>();
            //foreach(int number in numbers)
            //{
            //    if(number % 2 == 0)
            //    {
            //        evenNumbers.Add(number);
            //    }
            //}
            var evenNumbers = numbers.Where(n => n % 2 == 0).ToList();
            Console.WriteLine("Even numbers:");
            foreach (var number in evenNumbers)
            {
                Console.Write(number + " ");
            }
        }      
    }
}
