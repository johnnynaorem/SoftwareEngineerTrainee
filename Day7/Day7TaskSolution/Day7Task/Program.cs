using System.IO;

namespace Day7Task
{
    internal class Program
    {
        //ICustomerInterface _customer;

        //public Program() {
        //    _customer = new CustomerValidate();
        //}
        //static void Main(string[] args)
        //{
        //    var program = new Program();
        //    program._customer.Validate();
        //}

        Customer customer;
        ICustomerInterface customerValidate;
        public Program()
        {
            customer = new PremiumCustomer();
            customerValidate = new CustomerValidate();
        }
        void CheckCustomer()
        {
            try
            {
                customer.TakeCustomerDetailsFromConsole();
                customerValidate.Validate(customer);
            }
            catch (InvalidInputDetailException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch (InvalidDateException ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally {
                Console.WriteLine("Final Block Executed");
            }
        }
        static void Main(string[] args)
        {
            new Program().CheckCustomer();

        }
    }
}
