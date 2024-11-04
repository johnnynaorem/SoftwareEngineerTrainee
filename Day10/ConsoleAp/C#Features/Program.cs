namespace C_Features
{
    internal class Program
    {
        public void UnderstandingLimits()
        {
            int num1 = int.MaxValue;
            Console.WriteLine($"The value of num1 is: {num1}");
            checked
            {
                try
                {
                    num1++;
                    Console.WriteLine($"The value of num1 after increment is: {num1}");
                }
                catch (OverflowException ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }

        void UnderstandingErrorHandling()
        {
            int num = 0;
            Console.Write("Enter number: ");
            while (int.TryParse(Console.ReadLine(), out num) == false)
            {
                Console.Write("Invalid Type!!! Please Enter valid type: ");
            }
                Console.WriteLine("The number is: " + num);
            
        }

        void UnderstandingThread()
        {
            lock (this)
            {
                for (int i = 0; i < 10; i++)
                {
                    //if(i == 5)
                    //{
                    //    Thread.Sleep(4000);
                    //}
                    Thread.Sleep(1000);
                    Console.WriteLine($"The thread {Thread.CurrentThread.Name} is printing {i}");
                }
            }
        }
        void UnderstandingUsageOfWaitTime()
        {
            for (int i = 10; i <= 100; i = i + 10)
            {
                if (i == 50)
                {
                    Thread.Sleep(6000);
                }
                Console.WriteLine(i);
            }
        }


        void UnderstandingTask()
        {
            Task task = new Task(() =>
            {
                for (int i = 0; i < 10; i++)
                {
                    Thread.Sleep(1000);
                    Console.WriteLine($"The task is printing {i}");
                }
            });
            task.Start();
            //task.Wait();
            Console.WriteLine("Task Completed");
        }

        async Task UnderstandingAsyncAwait()
        {
            Console.WriteLine("First line in task");
            Thread.Sleep(5000);
            Console.WriteLine("Last line in task");
        }

        async Task CallTheAsyncMehod()
        {
            //Console.WriteLine("Calling the async method.");
            //Task task = UnderstandingAsyncAwait();
            //Console.WriteLine("Call is done");
            //task.Wait();

            Console.WriteLine("Calling the async method.");
            await UnderstandingAsyncAwait();
            Console.WriteLine("Call is done");
        }

       
        static async Task Main(string[] args)
        {
            Program program = new Program();
            //program.UnderstandingLimits();
            //program.UnderstandingErrorHandling();
            //program.UnderstandingThread();
            //program.UnderstandingUsageOfWaitTime();

            //Thread t1, t2;
            //t1 = new Thread(program.UnderstandingThread);
            //t2 = new Thread(program.UnderstandingUsageOfWaitTime);
            //t1 = new Thread(program.UnderstandingThread);
            //t2 = new Thread(program.UnderstandingThread);
            //t1.Name = "Thread 1";
            //t2.Name = "Thread 2";
            //t1.Start();
            //t2.Start();
            //program.UnderstandingTask();
            await program.CallTheAsyncMehod();
        }
    }
}
