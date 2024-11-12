using System.Collections.Generic;
using testCLVD.DBContext;
using testCLVD.DB_Models;
using Microsoft.EntityFrameworkCore;

namespace testCLVD.DBContext 
{
    public class MyContext : DbContext
    {
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }

    }
}
