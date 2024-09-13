using System.Collections;
namespace Collections
{
    internal class Program
    {

        public void UnderstandingCollectionArrayList()
        {
            ArrayList array = new ArrayList();
            array.Add(0);
            array.Add(1);

            array.Add(new Random().Next(10, 50));
            array.Add(new Random().Next(10, 50));
            array.Add(new Random().Next(10, 50));

            foreach (var item in array)
            {
                Console.WriteLine(item);
            }
        }

        public void UnderstandingCollectionList()
        {
            List<int> array = new List<int>();
            for (int i = 0; i < 10; i++)
            {
                array.Add(new Random().Next(10, 40));
            }
            foreach (var item in array)
            {
                Console.WriteLine(item);
            }
        }

        public void Print(List<Customer> customer)
        {
            foreach (var item in customer)
            {
                Console.WriteLine(item);
            }
        }

        public void UnderStandingMoreList()
        {
            List<Customer> customers = new List<Customer>()
            {
                new Customer(100, "Johnny", 8787470933),
                new Customer(101, "Bimu", 5544332211),
                new Customer(102, "Somu", 9876543210)
            };
            int choice = 0;
            //do
            //{
            //    Customer customer = new Customer();
            //    customer.GetCustomerDetails();
            //    customer.Id = customers.Count + 100;
            //    customers.Add(customer);
            //    Console.WriteLine("Do you want to continue? Then enter any number other than 0.");
            //    choice = Convert.ToInt32(Console.ReadLine());
            //} while (choice != 0);
            Console.WriteLine("----------------------------------------");
            Print(customers);
            bool isCustomerFound = customers.Contains(new Customer(100, "Johnny", 8787470933));
            Console.WriteLine("Is Johnny present " + isCustomerFound);
            //var index = customers.IndexOf(new Customer(100, "Johnny", 8787470933));
            //Console.WriteLine(index);
            //customers.RemoveAt(index);
            //customers.Remove(new Customer(100, "Johnny", 8787470933));
            customers.Sort();
            Print(customers);
        }

        public void UnderstandingDictionary()
        {
            Dictionary<int, Customer> customers = new Dictionary<int, Customer>();
            try
            {
                customers.Add(100, new Customer(100, "Johnny", 8787470933));
                customers.Add(101, new Customer(101, "Rohit", 8787470943));
                customers.Add(102, new Customer(102, "Venker", 8787470993));

                foreach (var item in customers.Keys)
                {
                    Console.WriteLine(customers[item]);
                }
            }
            catch (Exception ex) {
                Console.WriteLine(ex.Message.ToString());
            }

        }

        static void Main(string[] args)
        {
            Program program = new Program();
            //program.UnderstandingCollectionArrayList();
            //program.UnderstandingCollectionList();
            //program.UnderStandingMoreList();
            program.UnderstandingDictionary();
        }
    }
}
