namespace StaticConstructor
{
    internal class Program
    {
        public void HandleCompany()
        {
            Console.WriteLine(Company.NumberOfEmployee);
        }
        static void Main(string[] args)
        {
            Program program = new Program();
            program.HandleCompany();
        }
    }
}
