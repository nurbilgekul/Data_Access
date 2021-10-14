using ECommercialDb.Models.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommercialDb.Infrastructer.Context
{
    public class ApplicationDbContext:DbContext
    {

        public ApplicationDbContext()
        {
            Database.Connection.ConnectionString = @"Server=.;Database=ECommercialDb;Integrated Security=True;";
        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Shipper> Shippers { get; set; }
        public DbSet<Supplier> Suppliers { get; set; }
        public DbSet<Customer> Customers  { get; set; }


    }

}
