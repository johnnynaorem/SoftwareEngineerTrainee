namespace Destructor
{
    internal class Program
    {
        public void Test()
        {
            using (Destructor destructor = new Destructor()) {
                Console.WriteLine(destructor.Id)
                destructor.SetValue(1);
                destructor.Display();
            }
        }

        static void Main(string[] args)
        {
            Program program = new Program();
            program.Test();
            
        }
    }
}
