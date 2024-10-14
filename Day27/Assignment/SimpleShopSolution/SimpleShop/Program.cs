namespace SimpleShop
{
    internal class Program
    {
        UserInteraction userInteraction;
        public Program() {
            userInteraction = new UserInteraction();
        }

        void Start()
        {
            userInteraction.UserInteractions();
        }
        static void Main(string[] args)
        {
            Program program = new Program();
            program.Start();
        }
    }
}
