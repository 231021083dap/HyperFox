using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApi.Database.Entities;
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
        public void GetAll_ShouldReturnListOfGenreResponses_WhenGenresExist()
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
                .Returns(Genres);

            // Act
            var result = _sut.GetAllGenres();

            // Assert 
            Assert.NotNull(result);
            Assert.Equal(2, result.Count);
            Assert.IsType<List<GenreResponse>>(result);
        }
    }
}
