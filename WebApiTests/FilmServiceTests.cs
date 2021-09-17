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
    public class FilmServiceTests
    {
        private readonly FilmService _sut;
        private readonly Mock<IFilmRepository> _filmRepository = new();

        public FilmServiceTests()
        {
            _sut = new FilmService(_filmRepository.Object);
        }

        [Fact]
        public void GetAll_ShouldReturnListOfFilmResponses_WhenFilmExist()
        {
            // Arrange
            List<Film> Films = new List<Film>();

            Films.Add(new Film
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

            Films.Add(new Film
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

            _filmRepository
                .Setup(f => f.GetAll())
                .Returns(Films);

            // Act
            var result = _sut.GetAllFilms();

            // Assert 
            Assert.NotNull(result);
            Assert.Equal(2, result.Count);
            Assert.IsType<List<FilmResponse>>(result);
        }
    }
}
