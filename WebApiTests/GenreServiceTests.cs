using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApi.Database.Entities;
using WebApi.DTOs.Requests;
using WebApi.DTOs.Responses;
using WebApi.Repositories;
using WebApi.Services;
using Xunit;

namespace WebApiTests
{
    public class GenreServiceTests
    {
        private readonly GenreService _sut;
        private readonly Mock<IGenreRepository> _genreRepository = new();

        public GenreServiceTests()
        {
            _sut = new GenreService(_genreRepository.Object);
        }

        [Fact]
        public async void GetAll_ShouldReturnListOfGenreResponses_WhenGenresExist()
        {
            // Arrange
            List<Genre> Genres = new List<Genre>();

            Genres.Add(new Genre
            {
                GenreId = 1,
                GenreName = "Action"
            });

            Genres.Add(new Genre
            {
                GenreId = 1,
                GenreName = "Action"
            });

            _genreRepository
                .Setup(g => g.GetAll())
                .ReturnsAsync(Genres);

            // Act
            var result = await _sut.GetAllGenres();

            // Assert 
            Assert.NotNull(result);
            Assert.Equal(2, result.Count);
            Assert.IsType<List<GenreResponse>>(result);
        }

        [Fact]
        public async void GetById_ShouldReturnAGenreResponse_WhenGenreExists()
        {
            // Arrange
            int genreId = 1;

            Genre genre = new Genre
            {
                GenreId = genreId,
                GenreName = "Action"
            };

            _genreRepository
                .Setup(g => g.GetById(It.IsAny<int>()))
                .ReturnsAsync(genre);

            // Act
            var result = await _sut.GetById(genreId);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<GenreResponse>(result);
            Assert.Equal(genre.GenreId, result.GenreId);
            Assert.Equal(genre.GenreName, result.GenreName);
        }

        [Fact]
        public async void GetById_ShouldReturnNull_WhenGenreDoesNotExists()
        {
            // Arrange
            int genreId = 1;

            _genreRepository
                .Setup(a => a.GetById(It.IsAny<int>()))
                .ReturnsAsync(() => null);

            // Act
            var result = await _sut.GetById(genreId);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async void Create_ShouldReturnAGenreResponse_WhenCreateIsSuccess()
        {
            // Arrange
            NewGenre newGenre = new NewGenre
            {
                GenreName = "Action"
            };

            int genreId = 1;

            Genre genre = new Genre
            {
                GenreId = genreId,
                GenreName = "Action"
            };

            _genreRepository
                .Setup(g => g.Create(It.IsAny<Genre>()))
                .ReturnsAsync(genre);

            // Act
            var result = await _sut.Create(newGenre);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<GenreResponse>(result);
            Assert.Equal(genreId, result.GenreId);
            Assert.Equal(newGenre.GenreName, result.GenreName);
        }

        [Fact]
        public async void Update_ShouldReturnUpdatedGenreResponse_WhenUpdateIsSuccess()
        {
            // Arrange
            UpdateGenre updateGenre = new UpdateGenre
            {
                GenreName = "Action"
            };

            int genreId = 1;

            Genre genre = new Genre
            {
                GenreId = genreId,
                GenreName = "Action"
            };

            _genreRepository
                .Setup(g => g.Update(It.IsAny<int>(), It.IsAny<Genre>()))
                .ReturnsAsync(genre);

            // Act
            var result = await _sut.Update(genreId, updateGenre);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<GenreResponse>(result);
            Assert.Equal(genreId, result.GenreId);
            Assert.Equal(updateGenre.GenreName, result.GenreName);
        }

        [Fact]
        public async void Update_ShouldReturnNull_WhenGenreDoesNotExist()
        {
            // Arrange
            UpdateGenre updateGenre = new UpdateGenre
            {
                GenreName = "Action"
            };

            int genreId = 1;

            _genreRepository
                .Setup(a => a.Update(It.IsAny<int>(), It.IsAny<Genre>()))
                .ReturnsAsync(() => null);

            // Act
            var result = await _sut.Update(genreId, updateGenre);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async void Delete_ShouldReturnTrue_WhenDeleteIsSuccess()
        {
            // Arrange
            int genreId = 1;

            Genre genre = new Genre
            {
                GenreId = genreId,
                GenreName = "Action"
            };

            _genreRepository
                .Setup(a => a.Delete(It.IsAny<int>()))
                .ReturnsAsync(genre);

            // Act
            var result = await _sut.Delete(genreId);

            // Assert
            Assert.True(result);
        }
    }
}
