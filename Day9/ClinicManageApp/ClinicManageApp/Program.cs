namespace ClinicManageApp
{
    internal class Program
    {
        UserInteraction userInteraction;
        public Program()
        {
            userInteraction = new UserInteraction();
        }
        public void UserDirectInteraction()
        {
            userInteraction.MainInteraction();
        }
        
        static void Main(string[] args)
        {
            Program program = new Program();
            program.UserDirectInteraction();
        }
    }
}
