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
        public WebApiContext() { }
        public WebApiContext(DbContextOptions<WebApiContext> options) : base(options) { }
        public DbSet<Address> address { get; set; }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
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




        }
    }
}
