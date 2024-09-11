using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;

namespace Day6Task
{
    internal class ProductServices : ISupplierServices, ICustomerServices
    {
        private List<Product> products = new List<Product>();
        private List<Supplier> suppliers = new List<Supplier>();
        private List<Customer> customers = new List<Customer>();

        public ProductServices()
        {
            products.Add(new Product { Id = 2001, Name = "MSI Bravo 15 Laptop", Price = 90000, Quantity = 1 });
            suppliers.Add(new Supplier { Id = 9002, Name = "Biraj Singh" });
            customers.Add(new Customer { Id = 6001, Name = "Nandu", Phone = 9000000000 });
        }

        private Supplier GetSupplier(int sid)
        {
            return suppliers.FirstOrDefault(s => s.Id == sid);
        }

        public bool AddProduct(Product product, int sid)
        {
            Supplier supplier = GetSupplier(sid);
            if (supplier == null)
            {
                Console.WriteLine("Supplier not found in Supplier List!!! Unable to Add Product!!!!.");
                return false;
            }

            Product existingProduct = products.FirstOrDefault(p => p.Id == product.Id);
            if (existingProduct != null)
            {
                existingProduct.Quantity += product.Quantity;
            }
            else
            {
                products.Add(product);
                Console.WriteLine($"Product of Product ID \"{product.Id}\" is Added Successfully by Supplier ID \"{sid}\"");
            }
            
            return true;
        }

        public void GetAllProducts()
        {
            foreach (var item in products)
            {
                Console.WriteLine(item);
            }
        }

        public void GetAllSuppliers()
        {
            foreach (var item in suppliers)
            {
                Console.WriteLine(item);
            }
        }

        public void GetAllCustomers()
        {
            foreach (var item in customers)
            {
                Console.WriteLine(item);
            }
        }

        private Product GetProduct(int pid)
        {
            return products.FirstOrDefault(p => p.Id == pid);
        }

        

        public bool BuyProducts(int pid)
        {
            Product product = GetProduct(pid);
            if (product == null)
            {
                Console.WriteLine("Product not found. Please try another product.");
                return false;
            }
            if (product.Quantity > 0)
            {
                product.Quantity--;
                Console.WriteLine($"Product bought by Customer ID \"2001\" successfully. Remaining quantity: \"{product.Quantity}\"");
                return true;
            }
            else
            {
                Console.WriteLine("Product is out of stock.");
                return false;
            }
        }

        public bool UpdateProduct(int pid, int sid)
        {
            {
                Supplier supplier = GetSupplier(sid);
                if (supplier == null)
                {
                    Console.WriteLine("Supplier not found in Supplier List!!! Unable to Add Product!!!!.");
                    return false;
                }
                Product existingProduct = products.FirstOrDefault(p => p.Id == pid);
                if (existingProduct != null)
                {
                    existingProduct.Quantity += 1;
                    Console.WriteLine($"Product Quantity of Product ID \"{pid}\" is Updated by Supplier ID \"{sid}\"");
                }
                else {
                    Console.WriteLine($"Product ID \"{pid}\" is Not in Product List");
                }
                
                return true;
            }
        }
    }
}
