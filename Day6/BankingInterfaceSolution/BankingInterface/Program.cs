using System.Security.Principal;
using System.Threading.Channels;

namespace BankingInterface
{
    internal class Program
    {
        void PrintMainMenu()
        {
            Console.WriteLine("Welcome to the Bank Management Service");
            Console.WriteLine("Enter 1 to Create Normal Account");
            Console.WriteLine("Enter 2 to Create NRI Account");
            Console.WriteLine("Enter 3 to Create Salary Account");
        }

        //void MainInteraction()
        //{
        //    var choice = 0;
        //    do
        //    {
        //        PrintMainMenu();
        //        choice = Convert.ToInt32(Console.ReadLine());
        //        switch (choice)
        //        {
        //            case 1:
        //                acctTypeInteraction("Normal");
        //                break;
        //            case 2:
        //                acctTypeInteraction("NRI");
        //                break;
        //            case 3:
        //                acctTypeInteraction("Salary");
        //                break;
        //            case 0:
        //                Console.WriteLine("Exiting...");
        //                break;
        //            default:
        //                Console.WriteLine("Invalid option");
        //                break;
        //        }
        //    } while (choice != 0);
        //}

        void MainInteraction()
        {
            var choice = 0;
            do
            {
                PrintMainMenu();
                choice = Convert.ToInt32(Console.ReadLine());
                switch (choice)
                {
                    case 1:
                        normalAcct();
                        break;
                    case 2:
                        nriAcct();
                        break;
                    case 3:
                        salaryAcct();
                        break;
                    case 0:
                        Console.WriteLine("Exiting...");
                        break;
                    default:
                        Console.WriteLine("Invalid option");
                        break;
                }
            } while (choice != 0);
        }
        //void PrintAcctMenu(string acctType)
        //{
        //    Console.WriteLine($"{acctType} Account Menu:");
        //    Console.WriteLine("1 Show Balance");
        //    Console.WriteLine("2 Show Minimum Balance");
        //    Console.WriteLine("3 Money Transaction");
        //    Console.WriteLine("0 Back to Main Menu");
        //}

        //void acctTypeInteraction(string type)
        //{
        //    var choice = 0;
        //    do
        //    {
        //        PrintAcctMenu(type);
        //        choice = Convert.ToInt32(Console.ReadLine());
        //        switch (choice)
        //        {
        //            case 1:
        //                showBalance();
        //                break;
        //            case 0:
        //                return;
        //            default:
        //                Console.WriteLine("Invalid option");
        //                break;
        //        }
        //    } while (choice != 0);
        //}

        //public void showBalance() {
        //    bankServices.getBalance();
        //}

        private void normalAcct()
        {
            Console.WriteLine("Creating Normal Account");
            Console.WriteLine("Enter Amount to Deposit (minimum: 5000)");
            double balance = Convert.ToDouble(Console.ReadLine());
            IBankInterface normalAccount = new NormalBank(balance);
            Console.WriteLine("Normal Account:");
            Console.WriteLine($"Initial Balance: {normalAccount.getBalance()}");
            Console.WriteLine("Enter Amount for Transaction: ");
            double transactBalance = Convert.ToDouble(Console.ReadLine());
            bool normalTransactionSuccess = normalAccount.MoneyTransaction(transactBalance);
            Console.WriteLine($"Transaction Success: {normalTransactionSuccess}");
            if (normalTransactionSuccess)
            {
                Console.WriteLine($"New Balance after transacting {transactBalance}: {normalAccount.getBalance()}");
            }
        }
        private void nriAcct()
        {
            Console.WriteLine("Creating NRI Account");
            Console.WriteLine("Enter Amount to Deposit (minimum 10000)");
            double balance = Convert.ToDouble(Console.ReadLine());
            IBankInterface nriAccount = new NRIbank(balance);
            Console.WriteLine("NRI Account:");
            Console.WriteLine($"Initial Balance: {nriAccount.getBalance()}");
            Console.WriteLine("Enter Amount for Transaction: ");
            double transactBalance = Convert.ToDouble(Console.ReadLine());
            bool normalTransactionSuccess = nriAccount.MoneyTransaction(transactBalance);
            Console.WriteLine($"Transaction Success: {normalTransactionSuccess}");
            if (normalTransactionSuccess)
            {
                Console.WriteLine($"New Balance after transacting {transactBalance}: {nriAccount.getBalance()}");
            }
        }
        private void salaryAcct()
        {
            Console.WriteLine("Creating Salary Account");
            Console.WriteLine("Enter Amount to Deposit: ");
            double balance = Convert.ToDouble(Console.ReadLine());
            IBankInterface salaryAccount = new SalaryBank(balance);
            Console.WriteLine("Salary Account:");
            Console.WriteLine($"Initial Balance: {salaryAccount.getBalance()}");
            Console.WriteLine("Enter Amount for Transaction: ");
            double transactBalance = Convert.ToDouble(Console.ReadLine());
            bool normalTransactionSuccess = salaryAccount.MoneyTransaction(transactBalance);
            Console.WriteLine($"Transaction Success: {normalTransactionSuccess}");
            if (normalTransactionSuccess)
            {
                Console.WriteLine($"New Balance after transacting {transactBalance}: {salaryAccount.getBalance()}");
            }
        }

        public void BuyProducts() { }
        static void Main(string[] args)
        {
            var program = new Program();
            program.MainInteraction();
        }
    }
}
