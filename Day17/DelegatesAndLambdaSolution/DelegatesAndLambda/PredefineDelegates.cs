using System;

namespace DelegatesAndLambda
{
    public class PredefinedDelegates
    {
        // Predefined delegates
        Action<string> printMessage = message => Console.WriteLine(message);
        Func<int, int, int> addFunc = (x, y) => x + y;
        Predicate<int> isEven = number => number % 2 == 0;

        public void Execute()
        {
            
            printMessage("Hello, this is a message!");

            
            int sum = addFunc(5, 7);
            Console.WriteLine($"Sum: {sum}"); 

            
            bool result = isEven(4);
            Console.WriteLine($"Is 4 even? {result}"); 
        }
    }
}
