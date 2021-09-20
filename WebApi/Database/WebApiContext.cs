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
        public DbSet<Film> Film { get; set; }

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
}
