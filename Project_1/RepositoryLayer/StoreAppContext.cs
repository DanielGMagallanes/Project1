using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using ModelLayer;

namespace RepositoryLayer
{
    public class StoreAppContext : DbContext
    {
        public DbSet<Product> products { get; set; }
        public DbSet<Customer> customers { get; set; }
        public DbSet<Orders> orders { get; set; }
        public DbSet<Item> ItemsAtStore { get; set; }
        public DbSet<OrderedItem> orderedItems { get; set; }
        public DbSet<Store> stores { get; set; }
        public DbSet<Cart> cart { get; set; }

        public StoreAppContext() { }
        public StoreAppContext(DbContextOptions options) : base(options) { }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            if(!options.IsConfigured)
            {
                options.UseSqlServer("Server=LocalHost\\SQLEXPRESS01;Database=Project_1_StoreApp;Trusted_Connection=True;");
            }
        }

    }
}
