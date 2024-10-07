using System;
namespace UnderstandingAbstactClass
{

    public abstract class BankAccount
    {
        public string AccountNumber { get; private set; }
        public string AccountHolder { get; private set; }
        protected double Balance { get; set; }

        public BankAccount(string accountNumber, string accountHolder)
        {
            AccountNumber = accountNumber;
            AccountHolder = accountHolder;
        }

        public abstract void Deposit(double amount);
        public abstract void Withdraw(double amount);


        public virtual void DisplayBalance()
        {
            Console.WriteLine($"Account Number: {AccountNumber}, Account Holder: {AccountHolder}, Balance: ${Balance}");
        }
    }
}