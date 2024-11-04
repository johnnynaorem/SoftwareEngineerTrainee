namespace DatabaseInteractionApp
{
    internal class Program
    {
        Services ser;

        public Program() { 
            ser = new Services();
        }    
        void Run()
        {
            ser.UserInteraction();
        }
        static void Main(string[] args)
        {
            Program program = new Program();
            program.Run();
        }
    }
}
