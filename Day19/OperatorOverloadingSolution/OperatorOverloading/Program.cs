namespace OperatorOverloading
{
    internal class Program
    {

        public void Test()
        {
            Product product1 = new Product() { Id = 1, Name = "Johnny", Price = 1000 };
            Product product2 = new Product() { Id = 2, Name = "Naorem", Price = 2000 };
            Product product = product1 + product2;
            Console.WriteLine(product);
        }
        static void Main(string[] args)
        {
            Program program = new Program();
            program.Test();
        }
    }
}
