using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Database.Entities;
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

                },
                new User
                {
                    UserId = 2,
                    UserName = "Test2",
                    Email = "Test2",
                    Password = "Test2",
                    Admin = "User"

                });

            modelBuilder.Entity<Address>().HasData(

                new Address
                {
                    AddressId = 1,
                    StreetName = "Tec Ballerup",
                    Postal = 2700,
                    City = "Ballerup",
                    UserId = 1


                },
                new Address
                {
                    AddressId = 2,
                    StreetName = "Havet",
                    Postal = 2700,
                    City = "Kattegat",
                    UserId = 2
                });

            modelBuilder.Entity<Order>().HasData(

                new Order
                {
                    OrderId = 1,
                    DateTime = DateTime.Parse("2001-08-21 04:45:21"),
                    UserId = 1

                },

                 new Order
                 {
                     OrderId = 2,
                     DateTime = DateTime.Parse("2001-08-21 04:45:41"),
                     UserId = 1

                 });

            modelBuilder.Entity<Item>().HasData(
                    //Creating new Item in the Database.

                    new Item
                    {
                        ItemId = 1,
                        FilmId = 1,
                        OrderId = 1,
                        Quantity = 1,
                        Price = 1
                    },

                    new Item
                    {
                        ItemId = 2,
                        FilmId = 2,
                        OrderId = 2,
                        Quantity = 2,
                        Price = 2
                    });

            modelBuilder.Entity<Genre>().HasData(
                new Genre
                {
                    GenreId = 1,
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
                    Image = "https://www.information.dk/sites/information.dk/files/styles/open_graph/public/media/2011/05/27/20110527-144201-124983_0.jpg?itok=dpVGfrig",
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
                    Image = "https://www.bog-ide.dk/scommerce/images/klods-hans-592756.jpg?i=592756",
                    GenreId = 1
                });
        }

    }
}
