namespace DelegatesAndLambda
{
    public class Program
    {
        // Step 1: Define a delegate
        public delegate string NumberToString(int number);

        // Step 2: Create a method that matches the delegate signature
        public static string ConvertToString(int number)
        {
            return number.ToString();
        }

        public delegate int MathOperation(int x, int y);

        // Usage with an anonymous method
        MathOperation add = delegate (int x, int y)
        {
            return x + y;
        };

        static void Main()
        {
            // Step 3: Create an instance of the delegate
            //NumberToString converter = ConvertToString;

            //// Call the method through the delegate
            //string result = converter(42);

            //Console.WriteLine(result.GetType());
            //Console.WriteLine(result);

            //MulticastDelegate multicastDelegate = new MulticastDelegate();
            //multicastDelegate.RaiseNotifications();

            //PredefinedDelegates predefinedDelegates = new PredefinedDelegates();
            //predefinedDelegates.Execute();

            Lambda lambda = new Lambda();
            lambda.GetEvenNumber();
        }
    }
}
