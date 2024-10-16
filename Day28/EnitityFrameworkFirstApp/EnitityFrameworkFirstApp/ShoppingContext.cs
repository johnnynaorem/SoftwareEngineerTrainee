using EnitityFrameworkFirstApp.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnitityFrameworkFirstApp
{
    internal class ShoppingContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=5CD413DKRZ\\JOHNNY_INSTANCES;Integrated Security=True; Initial Catalog=dbEFCore15Oct;TrustServerCertificate=True");
        }
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Supplier> Suppliers { get; set; }
        public DbSet<SupplierProduct> SupplierProducts { get; set; }    


    }
}
