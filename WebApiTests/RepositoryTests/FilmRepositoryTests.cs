using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApi.Database.Entities;
using WebApi.Repositories;
using Xunit;

namespace WebApiTests
{
    public class FilmRepositoryTests
    {
        private DbContextOptions<WebApiContext> _options;
        private readonly WebApiContext _context;
        private readonly FilmRepository _sut;

        public FilmRepositoryTests()
        {
            _options = new DbContextOptionsBuilder<WebApiContext>()
                .UseInMemoryDatabase(databaseName: "WebApi")
                .Options;

            _context = new WebApiContext(_options);

            _sut = new FilmRepository(_context);
        }

        [Fact]
        public async Task GetAll_ShouldReturnListOfFilms_WhenFilmsExists()
        {
            // Arrange
            await _context.Database.EnsureDeletedAsync();

            _context.Film.Add(new Film
            {
                FilmId = 1,
                FilmName = "The lord of the rings",
                ReleaseDate = "16-09-2001",
                RuntimeInMin = 123,
                Description = "This movie is about a ring",
                Price = 79.99M,
                Stock = 50,
                Image = "C:\\Users\\Tec\\Pictures\\1.jpg"
            });

            _context.Film.Add(new Film
            {
                FilmId = 2,
                FilmName = "Harry potter",
                ReleaseDate = "16-09-2001",
                RuntimeInMin = 123,
                Description = "This movie is about the wizard world",
                Price = 79.99M,
                Stock = 50,
                Image = "C:\\Users\\Tec\\Pictures\\2.jpg"
            });

            await _context.SaveChangesAsync();

            // Act
            var result = await _sut.GetAll();

            // Assert
            Assert.NotNull(result);
            Assert.Equal(2, result.Count);
            Assert.IsType<List<Film>>(result);
        }

        [Fact]
        public async Task GetAll_ShouldReturnEmptyListOfFilms_WhenNoFilmExists()
        {
            // Arrange
            await _context.Database.EnsureDeletedAsync();

            // Act
            var result = await _sut.GetAll();

            // Assert
            Assert.NotNull(result);
            Assert.Empty(result);
            Assert.IsType<List<Film>>(result);
        }
    }
}
