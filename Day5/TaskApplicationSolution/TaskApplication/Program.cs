using System.ComponentModel;

namespace TaskApplication
{
    internal class Program
    {
        public void TakeNumbers()
        {
            int[] numbers = new int[5];
            for (int i = 0; i < numbers.Length; i++)
            {
                numbers[i] = Convert.ToInt32(Console.ReadLine());
            }
            Calculate(numbers);
        }


        static void Calculate(int[] a)
        {
            int result = 0;
            int count = 0;
            for (int i = 0; i < a.Length; i++)
            {
                if (a[i] % 7 == 0)
                {
                    count++;
                    result = result + a[i];
                }
            }
            var output = result / count;
            Console.WriteLine(output);
        }

        static void PrimeNumber()
        {
            int start = Convert.ToInt32(Console.ReadLine());
            int end = Convert.ToInt32(Console.ReadLine());

            for (int i = start; i <= end; i++)
            {
                if (i <= 1) {
                    continue;
                }
                bool flag = true;
                for (int j = 2; j < i; j++)
                {
                    if ((i % j) == 0)
                    {
                        flag = false;
                        break;
                    }

                }
                if( flag == true)
                {
                    Console.WriteLine(i);
                }
            }
        }

        static void CollectAndPrintNumbers()
        {
            List<int> numbers = new List<int>();
            int number;
            bool flag = true;
            while (flag) {
                Console.WriteLine("Enter numbers (enter \"-1\" to stop): ");
                number = Convert.ToInt32(Console.ReadLine());
                if(number == -1)
                {
                    flag = false;
                    break;
                }
                if (number % 10 == 3 || number % 3 == 0)
                {
                    numbers.Add(number);
                }
            }
            Console.WriteLine("The numbers that end with 3 or divisible by 3 are: ");
            foreach (var i in numbers)
            {
                Console.WriteLine(i);
            }
        }

            static void Main(string[] args)
            {
                //var program = new Program();
                //program.TakeNumbers();
                //PrimeNumber();
                CollectAndPrintNumbers();
            }
        }
    }
