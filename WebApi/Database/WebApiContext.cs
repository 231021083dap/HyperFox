using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Database.Entities
{
    public class WebApiContext : DbContext
    {
        public WebApiContext() { }
        public WebApiContext(DbContextOptions<WebApiContext> options) : base(options) { }

        public DbSet<Genre> Genre { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Genre>().HasData(
                new Genre
                {
                    GenreId = 1,
                    GenreName = "Action"
                },
                new Genre
                {
                    GenreId = 2,
                    GenreName = "Comedy"
                });


        }
    }
}
