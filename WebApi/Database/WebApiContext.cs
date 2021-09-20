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
        public DbSet<Genre> Genre { get; set; }
        public DbSet<Film> Film { get; set; }


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

            modelBuilder.Entity<Genre>().HasData(
               GenreId = 1,
                    GenreName = "Action"
                },
                new Genre
                {
                    GenreId = 2,
                    GenreName = "Comedy"
                });

            modelBuilder.Entity<Film>().HasData(
                new Film
                {
                    FilmId = 1,
                    FilmName = "The lord of the rings",
                    ReleaseDate = "16-09-2001",
                    RuntimeInMin = 123,
                    Description = "This movie is about a ring",
                    Price = 79.99M,
                    Stock = 50,
                    Image = "C:\\Users\\Tec\\Pictures\\1.jpg",
                    GenreId = 1
                },
                new Film
                {
                    FilmId = 2,
                    FilmName = "Harry potter",
                    ReleaseDate = "16-09-2001",
                    RuntimeInMin = 123,
                    Description = "This movie is about the wizard world",
                    Price = 79.99M,
                    Stock = 50,
                    Image = "C:\\Users\\Tec\\Pictures\\2.jpg",
                    GenreId = 2
                });

    }
}
