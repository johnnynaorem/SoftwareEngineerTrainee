using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnderstandingAbstactClass
{
    internal class UserInteraction
    {
        public void printMenu()
        {
            Console.WriteLine("Welcome to Banking Service");
            Console.WriteLine("1. Saving");
            Console.WriteLine("2. Current");
            Console.WriteLine("0. Exit");
        }

        public void MainInteraction()
        {
            var choice = 0;
            do
            {
                printMenu();
                choice = Convert.ToInt32(Console.ReadLine());
                switch (choice)
                {
                    case 1:
                        SavingAccount();
                        break;
                    case 2:
                        CurrentAccount();
                        break;
                    case 0:
                        Console.WriteLine("Exit...");
                        break;
                    default:
                        Console.WriteLine("Invalid option");
                        break;
                }
            } while (choice != 0);
        }


        private void SavingAccount()
        {
            SavingsAccount johnsSavings = new SavingsAccount("SA123456", "John Doe");
            johnsSavings.Deposit(1000);
            johnsSavings.Withdraw(200);
            johnsSavings.AddInterest();
            johnsSavings.DisplayBalance();
        }

        private void CurrentAccount()
        {
            CurrentAccount janesCurrent = new CurrentAccount("CA654321", "Jane Smith");
            janesCurrent.Deposit(500);
            janesCurrent.Withdraw(1000);
            janesCurrent.DisplayBalance();
        }
    }
}
