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
                .UseInMemoryDatabase(databaseName: "WebApiFilm")
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

        [Fact]
        public async void GetById_ShouldReturnTheFilm_IfFilmExists()
        {
            // Arrange
            await _context.Database.EnsureDeletedAsync();

            int filmId = 1;

            _context.Film.Add(new Film
            {
                FilmId = filmId,
                FilmName = "The lord of the rings",
                ReleaseDate = "16-09-2001",
                RuntimeInMin = 123,
                Description = "This movie is about a ring",
                Price = 79.99M,
                Stock = 50,
                Image = "C:\\Users\\Tec\\Pictures\\1.jpg"
            });

            await _context.SaveChangesAsync();

            // Act
            var result = await _sut.GetById(filmId);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<Film>(result);
            Assert.Equal(filmId, result.FilmId);
        }

        [Fact]
        public async void GetById_ShouldReturnNull_IfFilmDoesNotExists()
        {
            // Arrange
            await _context.Database.EnsureDeletedAsync();

            int filmId = 1;

            // Act
            var result = await _sut.GetById(filmId);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task Create_ShouldAddIdToFilm_WhenSavingToDatabase()
        {
            // Arange
            await _context.Database.EnsureDeletedAsync();

            int expectedId = 1;

            Film film = new Film
            {
                FilmName = "The lord of the rings",
                ReleaseDate = "16-09-2001",
                RuntimeInMin = 123,
                Description = "This movie is about a ring",
                Price = 79.99M,
                Stock = 50,
                Image = "C:\\Users\\Tec\\Pictures\\1.jpg"
            };

            // Act
            var result = await _sut.Create(film);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<Film>(result);
            Assert.Equal(expectedId, result.FilmId);
        }

        [Fact]
        public async Task Create_ShouldFailToAddFilm_WhenAddingFilmWithExistingId()
        {
            // Arrange
            await _context.Database.EnsureDeletedAsync();

            Film film = new Film
            {
                FilmId = 1,
                FilmName = "The lord of the rings",
                ReleaseDate = "16-09-2001",
                RuntimeInMin = 123,
                Description = "This movie is about a ring",
                Price = 79.99M,
                Stock = 50,
                Image = "C:\\Users\\Tec\\Pictures\\1.jpg"
            };

            _context.Film.Add(film);
            await _context.SaveChangesAsync();

            // Act
            Func<Task> action = async () => await _sut.Create(film);

            // Assert
            var ex = await Assert.ThrowsAsync<ArgumentException>(action);
            Assert.Contains("An item with the same key has already been added", ex.Message);
        }

        [Fact]
        public async Task Update_ShouldChangeValuesOnFilm_WhenfilmExists()
        {
            // Arrange
            await _context.Database.EnsureDeletedAsync();

            int filmId = 1;

            Film film = new Film
            {
                FilmId = filmId,
                FilmName = "The lord of the rings",
                ReleaseDate = "16-09-2001",
                RuntimeInMin = 123,
                Description = "This movie is about a ring",
                Price = 79.99M,
                Stock = 50,
                Image = "C:\\Users\\Tec\\Pictures\\1.jpg"
            };

            _context.Film.Add(film);
            await _context.SaveChangesAsync();

            Film updateFilm = new Film
            {
                FilmId = filmId,
                FilmName = "The lord of the rings 2",
                ReleaseDate = "10-09-2002",
                RuntimeInMin = 150,
                Description = "This is part 2 of a movie about a ring",
                Price = 89.99M,
                Stock = 60,
                Image = "C:\\Users\\Tec\\Pictures\\1.jpg"
            };

            // Act
            var result = await _sut.Update(filmId, updateFilm);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<Film>(result);
            Assert.Equal(filmId, result.FilmId);
            Assert.Equal(updateFilm.FilmName, result.FilmName);
            Assert.Equal(updateFilm.ReleaseDate, result.ReleaseDate);
            Assert.Equal(updateFilm.RuntimeInMin, result.RuntimeInMin);
            Assert.Equal(updateFilm.Description, result.Description);
            Assert.Equal(updateFilm.Price, result.Price);
            Assert.Equal(updateFilm.Stock, result.Stock);
            Assert.Equal(updateFilm.Image, result.Image);
        }

        [Fact]
        public async Task Update_ShouldReturnNull_WhenFilmDoesNotExists()
        {
            // Arrange
            await _context.Database.EnsureDeletedAsync();

            int filmId = 1;

            Film updateFilm = new Film
            {
                FilmId = filmId,
                FilmName = "The lord of the rings 2",
                ReleaseDate = "10-09-2002",
                RuntimeInMin = 150,
                Description = "This is part 2 of a movie about a ring",
                Price = 89.99M,
                Stock = 60,
                Image = "C:\\Users\\Tec\\Pictures\\1.jpg"
            };

            // Act
            var result = await _sut.Update(filmId, updateFilm);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task Delete_ShouldReturnDeleteFilm_WhenFilmIsDeleted()
        {
            // Arrange
            await _context.Database.EnsureDeletedAsync();

            int filmId = 1;

            Film film = new Film
            {
                FilmId = filmId,
                FilmName = "The lord of the rings",
                ReleaseDate = "16-09-2001",
                RuntimeInMin = 123,
                Description = "This movie is about a ring",
                Price = 79.99M,
                Stock = 50,
                Image = "C:\\Users\\Tec\\Pictures\\1.jpg"
            };

            _context.Film.Add(film);
            await _context.SaveChangesAsync();

            // Act
            var result = await _sut.Delete(filmId);
            var films = await _sut.GetAll();

            // Assert
            Assert.NotNull(result);
            Assert.IsType<Film>(result);
            Assert.Equal(filmId, result.FilmId);
            Assert.Empty(films);
        }

        [Fact]
        public async Task Delete_ShouldReturnNull_WhenGenreDoesNotExists()
        {
            // Arrange
            await _context.Database.EnsureDeletedAsync();

            int filmId = 1;

            // Act
            var result = await _sut.Delete(filmId);

            // Assert
            Assert.Null(result);
        }
    }
}
