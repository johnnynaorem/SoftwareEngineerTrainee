using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Collections
{
    internal class Customer : IEquatable<Customer>, IComparable<Customer>
    {
        public int Id { get; set; }
        public string Name { get; set; }   
        public long Phone { get; set; }

        public Customer (int id, string name, long phone)
        {
            Id = id;
            Name = name;
            Phone = phone;
        }

        public Customer()
        {

        }

        public void GetCustomerDetails()
        {
            Console.Write("Enter Name: ");
            Name = Console.ReadLine()?? "";
            Console.Write("Phone: ");
            Phone = Convert.ToInt64(Console.ReadLine());
        }

        public override string ToString()
        {
            return "ID: " + Id+";" + " Name: " + Name+";" + " Phone: " + Phone+";";
        }

        public bool Equals(Customer? other)
        {
            Customer c1, c2;
            c1 = this;
            c2 = other;
            if(c1.Id == c2.Id && c1.Name == c2.Name && c1.Phone == c2.Phone)
            {
                return true;
            }
            return false;
        }

        public int CompareTo(Customer? other)
        {
            return this.Phone.CompareTo(other.Phone);
        }
    }
}
