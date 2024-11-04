namespace DeligateUnderstanding
{
    internal class Program
    {
        public delegate int DeligateCalulation(int x, int y);
        public int Add(int x, int y)
        {
            return x + y;
        }

        public int Subtract(int x, int y)
        {
            return x - y;
        }

        public int Multiply(int x, int y)
        {
            return x * y;
        }

        public int Division(int x, int y)
        {
            return (x / y);
        }

        public void Calculate(Func<int, int, int> myDeligate, int n1, int n2) { 
            int result = myDeligate(n1, n2);
            Console.WriteLine($"The sum is: {result}");
        }
        public Program() {
            DeligateCalulation deligateCalculation = new DeligateCalulation(Add);
            Console.WriteLine(deligateCalculation(10, 20));
            //Func<int, int, int> deligateCalculation = Add;
            //deligateCalculation += Multiply;
            //Calculate(deligateCalculation, 10, 20);


        }

        //static void Main(string[] args)
        //{
        //    Program program = new Program();
        //}
    }
}
