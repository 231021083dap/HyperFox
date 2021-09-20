using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Entities;

namespace WebApi.Database
{
    public class WebApiContext : DbContext
    {
        public WebApiContext()    {}
        public WebApiContext(DbContextOptions<WebApiContext> options) : base(options) {}

        public DbSet<User> User { get; set; }
        public DbSet<Address> Address { get; set; }
        public DbSet<Order> Order { get; set; }
        public DbSet<Item> Item { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasData(
                new User
                {
                    UserId = 1,
                    UserName = "TestUserName",
                    Email = "TestMail",
                    Password = "TestPassword",
                    Admin = "Admin"

                });
            modelBuilder.Entity<Address>().HasData(

                new Address
                {
                    AddressId = 1,
                    Add = "Tec Ballerup",
                    Postal = 2700,
                    City = "Ballerup"


                },
                new Address
                {
                    AddressId = 2,
                    Add = "Tec Ballerup",
                    Postal = 2700,
                    City = "Ballerup"

                });

            modelBuilder.Entity<Order>().HasData(

                new Order
                {
                    OrderId = 1,
                    UserId = 1,
                    DateTime = "Friday 13th at 4:00"

                },
                new Order
                {

                    OrderId = 2,
                    UserId = 2,
                    DateTime = "Friday 13th at 4:00"
                });

            modelBuilder.Entity<Item>().HasData(
                //Creating new Item in the Database.
                
                    new Item
                {
                    ItemId = 1,
                    FilmId = 2,
                    OrderId = 2,
                    Quantity = 2,
                    Price = 2
                });
        }

    }
}
