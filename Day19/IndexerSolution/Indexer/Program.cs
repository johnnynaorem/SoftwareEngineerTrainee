namespace Indexer
{
    internal class Program
    {
        public void Test()
        {
            Indexer index = new Indexer();
            Console.WriteLine(index.Name);
            Console.WriteLine(index[0]);

            Console.WriteLine("The position of Java is "+ index["Java"]);
            Console.WriteLine("The position of React is "+ index["React"]);
        }
        static void Main(string[] args)
        {
            Program program = new Program();
            program.Test();
        }
    }
}
