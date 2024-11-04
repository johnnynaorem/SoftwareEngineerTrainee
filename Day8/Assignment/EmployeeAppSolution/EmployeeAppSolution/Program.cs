namespace EmployeeAppSolution
{
    internal class Program
    {
        UserInteraction userInteraction;
        public Program() {
            userInteraction = new UserInteraction();
        }

        static void Main(string[] args)
        {
            Program program = new Program();
            program.userInteraction.ShowMenu();
        }
    }
}
