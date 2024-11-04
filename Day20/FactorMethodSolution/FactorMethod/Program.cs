namespace FactorMethod
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // The client code works with an instance of a concrete creator
            // The CreateProduct will return the actual product instance via the product interface
            
            BankAcccount savingAcccount = new SavingFactory().CreateProduct();
            if (savingAcccount != null)
            {
                savingAcccount.CreateAccount("120", "Jaswant");
                savingAcccount.DepositAccount(6000);
                savingAcccount.ShowBalance();
            }
            else
            {
                Console.Write("Invalid Account Type");
            }
            Console.WriteLine("--------------");


            BankAcccount currentAcccount = new SavingFactory().CreateProduct();
            if (currentAcccount != null)
            {
                currentAcccount.CreateAccount("100", "Johnny");
                currentAcccount.DepositAccount(10000);
                currentAcccount.ShowBalance();
            }
            else
            {
                Console.Write("Invalid Account Type");
            }
            Console.WriteLine("--------------");

            Console.ReadLine();
        }
    }
}
