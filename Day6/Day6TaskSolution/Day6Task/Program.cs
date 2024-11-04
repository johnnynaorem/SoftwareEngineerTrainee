using System;

namespace Day6Task
{
    internal class Program
    {
        private ISupplierServices productServices;
        private ICustomerServices customerServices;

        public Program()
        {
            productServices = new ProductServices();
            customerServices = (ICustomerServices)productServices;
        }

        void PrintMainMenu()
        {
            Console.WriteLine("Welcome to the Product Management Service");
            Console.WriteLine("1 - Customer Interaction");
            Console.WriteLine("2 - Supplier Interaction");
            Console.WriteLine("0 - Exit");
        }

        void PrintCustomerMenu()
        {
            Console.WriteLine("Customer Menu:");
            Console.WriteLine("1 - Buy Product");
            Console.WriteLine("2 - View All Products");
            Console.WriteLine("0 - Back to Main Menu");
        }

        void PrintSupplierMenu()
        {
            Console.WriteLine("Supplier Menu:");
            Console.WriteLine("1 - Add Product");
            Console.WriteLine("2 - Update Product");
            Console.WriteLine("3 - View All Products");
            Console.WriteLine("4 - View All Customers");
            Console.WriteLine("5 - View All Suppliers");
            Console.WriteLine("0 - Back to Main Menu");
        }

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
                        CustomerInteraction();
                        break;
                    case 2:
                        SupplierInteraction();
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

        void CustomerInteraction()
        {
            var choice = 0;
            do
            {
                PrintCustomerMenu();
                choice = Convert.ToInt32(Console.ReadLine());
                switch (choice)
                {
                    case 1:
                        BuyProducts();
                        break;
                    case 2:
                        PrintAllProducts();
                        break;
                    case 0:
                        return; 
                    default:
                        Console.WriteLine("Invalid option");
                        break;
                }
            } while (choice != 0);
        }

        void SupplierInteraction()
        {
            var choice = 0;
            do
            {
                PrintSupplierMenu();
                choice = Convert.ToInt32(Console.ReadLine());
                switch (choice)
                {
                    case 1:
                        AddProduct();
                        break;
                    case 2:
                        Update(); 
                        break;
                    case 3:
                        PrintAllProducts();
                        break;
                    case 4:
                        PrintAllCustomers();
                        break;
                    case 5:
                        PrintAllSuppliers();
                        break;
                    case 0:
                        return; 
                    default:
                        Console.WriteLine("Invalid option");
                        break;
                }
            } while (choice != 0);
        }

        private void AddProduct()
        {
            Product product = new Product();
            Console.WriteLine("Enter Product Name");
            product.Name = Console.ReadLine();
            Console.WriteLine("Enter Id");
            product.Id = Convert.ToInt16(Console.ReadLine());
            Console.WriteLine("Enter price");
            product.Price = Convert.ToInt16(Console.ReadLine());
            Console.WriteLine("Enter Quantity");
            product.Quantity = Convert.ToInt16(Console.ReadLine());
            Console.WriteLine("Enter Supplier ID");
            int id = Convert.ToInt16(Console.ReadLine());

            productServices.AddProduct(product, id);
        }

        private void Update()
        {
            Console.WriteLine("Enter Supplier Id: ");
            int sid = Convert.ToInt16(Console.ReadLine());
            Console.WriteLine("Enter Product Id: ");
            int pid = Convert.ToInt16(Console.ReadLine());
            productServices.UpdateProduct(pid, sid);
        }

        private void PrintAllProducts()
        {
            customerServices.GetAllProducts();
        }

        private void PrintAllCustomers()
        {
            productServices.GetAllCustomers();
        }

        private void PrintAllSuppliers()
        {
            productServices.GetAllSuppliers();
        }

        private void BuyProducts()
        {
            Console.WriteLine("Enter Product Id which you want to Buy: ");
            int id = Convert.ToInt32(Console.ReadLine());

            bool success = customerServices.BuyProducts(id);
            if (success)
            {
                Console.WriteLine("Product purchase successful.");
            }
            else
            {
                Console.WriteLine("Product purchase failed.");
            }
        }

        static void Main(string[] args)
        {
            var program = new Program();
            program.MainInteraction();
        }
    }
}
