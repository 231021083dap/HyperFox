using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Entities;

namespace WebApi.Database.Entities
{
    public class WebApiContext : DbContext
    {
        //Construcktors 
        public WebApiContext() { }
        public WebApiContext(DbContextOptions<WebApiContext> options): base(options){ }
        public DbSet<Item> Item{ get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Item>().HasData(

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
        }
    }
}
