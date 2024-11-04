using System;
using System.Collections.Generic;

namespace WordyAppConsole
{
    public class Program
    {
        UserInteraction userInteraction;
        public Program() { 
            userInteraction = new UserInteraction();
        }

        static void Main(string[] args)
        {
            var program = new Program();
            program.userInteraction.MainInteraction();
        }
    }
}
