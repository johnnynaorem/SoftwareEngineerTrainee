namespace PrivateConstructor
{
    internal class Program
    {

        public void HandleConnection()
        {
            Connection connection1 = Connection.GetConnection();
            connection1.ConnectionName = "Test1";
            Console.WriteLine(connection1.ConnectionName);
            Connection connection2 = Connection.GetConnection();
            connection2.ConnectionName = "Test2";
            Console.WriteLine(connection2.ConnectionName);
            Console.WriteLine(connection1.ConnectionName);
        }
        static void Main(string[] args)
        {
            Program program = new Program();
            program.HandleConnection();
        }
    }
}
