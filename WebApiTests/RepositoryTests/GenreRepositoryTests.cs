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
    public class GenreRepositoryTests
    {
        private DbContextOptions<WebApiContext> _options;
        private readonly WebApiContext _context;
        private readonly GenreRepository _sut;

        public GenreRepositoryTests()
        {
            _options = new DbContextOptionsBuilder<WebApiContext>()
                .UseInMemoryDatabase(databaseName: "WebApiGenre")
                .Options;

            _context = new WebApiContext(_options);

            _sut = new GenreRepository(_context);
        }

        [Fact]
        public async Task GetAll_ShouldReturnListOfGenres_WhenGenresExists()
        {
            // Arrange

            // Delete Database
            await _context.Database.EnsureDeletedAsync();


            // Add 2 new Genres
            _context.Genre.Add(new Genre
            {
                GenreId = 1,
                GenreName = "Action",
            });

            _context.Genre.Add(new Genre
            {
                GenreId = 2,
                GenreName = "Comedy",
            });

            // Save changes
            await _context.SaveChangesAsync();

            // Act
            var result = await _sut.GetAll();

            // Assert
            Assert.NotNull(result);
            Assert.Equal(2, result.Count);
            Assert.IsType<List<Genre>>(result);
        }

        [Fact]
        public async Task GetAll_ShouldReturnEmptyListOfGenres_WhenNoGenresExists()
        {
            // Arrange
            await _context.Database.EnsureDeletedAsync();

            // Act
            var result = await _sut.GetAll();

            // Assert
            Assert.NotNull(result);
            Assert.Empty(result);
            Assert.IsType<List<Genre>>(result);
        }

        [Fact]
        public async void GetById_ShouldReturnTheGenre_IfGenreExists()
        {
            // Arrange
            await _context.Database.EnsureDeletedAsync();

            int genreId = 1;

            _context.Genre.Add(new Genre
            {
                GenreId = genreId,
                GenreName = "Action"
            });

            await _context.SaveChangesAsync();

            // Act
            var result = await _sut.GetById(genreId);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<Genre>(result);
            Assert.Equal(genreId, result.GenreId);
        }

        [Fact]
        public async void GetById_ShouldReturnNull_IfGenreDoesNotExists()
        {
            // Arrange
            await _context.Database.EnsureDeletedAsync();

            int genreId = 1;

            // Act
            var result = await _sut.GetById(genreId);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task Create_ShouldAddIdToGenre_WhenSavingToDatabase()
        {
            // Arange
            await _context.Database.EnsureDeletedAsync();

            int expectedId = 1;

            Genre genre = new Genre
            {
                GenreName = "Action"
            };

            // Act
            var result = await _sut.Create(genre);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<Genre>(result);
            Assert.Equal(expectedId, result.GenreId);
        }

        [Fact]
        public async Task Create_ShouldFailToAddGenre_WhenAddingGenreWithExistingId()
        {
            // Arrange
            await _context.Database.EnsureDeletedAsync();

            Genre genre = new Genre
            {
                GenreId = 1,
                GenreName = "Action"
            };

            _context.Genre.Add(genre);
            await _context.SaveChangesAsync();

            // Act
            Func<Task> action = async () => await _sut.Create(genre);

            // Assert
            var ex = await Assert.ThrowsAsync<ArgumentException>(action);
            Assert.Contains("An item with the same key has already been added", ex.Message);
        }

        [Fact]
        public async Task Update_ShouldChangeValuesOnGenre_WhenGenreExists()
        {
            // Arrange
            await _context.Database.EnsureDeletedAsync();

            int genreId = 1;

            Genre genre = new Genre
            {
                GenreId = genreId,
                GenreName = "Action"
            };

            _context.Genre.Add(genre);
            await _context.SaveChangesAsync();

            Genre updateGenre = new Genre
            {
                GenreId = genreId,
                GenreName = "Horror"
            };

            // Act
            var result = await _sut.Update(genreId, updateGenre);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<Genre>(result);
            Assert.Equal(genreId, result.GenreId);
            Assert.Equal(updateGenre.GenreName, result.GenreName);
        }

        [Fact]
        public async Task Update_ShouldReturnNull_WhenGenreDoesNotExists()
        {
            // Arrange
            await _context.Database.EnsureDeletedAsync();

            int genreId = 1;

            Genre updateGenre = new Genre
            {
                GenreId = genreId,
                GenreName = "Horror"
            };

            // Act
            var result = await _sut.Update(genreId, updateGenre);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task Delete_ShouldReturnDeleteGenre_WhenGenreIsDeleted()
        {
            // Arrange
            await _context.Database.EnsureDeletedAsync();

            int genreId = 1;

            Genre genre = new Genre
            {
                GenreId = genreId,
                GenreName = "Action"
            };

            _context.Genre.Add(genre);
            await _context.SaveChangesAsync();

            // Act
            var result = await _sut.Delete(genreId);
            var genres = await _sut.GetAll();

            // Assert
            Assert.NotNull(result);
            Assert.IsType<Genre>(result);
            Assert.Equal(genreId, result.GenreId);
            Assert.Empty(genres);
        }

        [Fact]
        public async Task Delete_ShouldReturnNull_WhenGenreDoesNotExists()
        {
            // Arrange
            await _context.Database.EnsureDeletedAsync();

            int genreId = 1;

            // Act
            var result = await _sut.Delete(genreId);

            // Assert
            Assert.Null(result);
        }
    }
}
