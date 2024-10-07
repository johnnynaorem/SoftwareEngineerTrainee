namespace InterfaceAmbi
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Sum sum = new Sum();
            ((Interface1)sum).Display();
            ((Interface2)sum).Display();
            //((Interface2)sum).Print();

            Interface1 iter1 = sum;
            //Interface2 iter2 = sum;

            iter1.Print();
        }
    }
}
