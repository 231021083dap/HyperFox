using Microsoft.AspNetCore.Mvc.Infrastructure;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApi.Controllers;
using WebApi.DTOs.Responses;
using WebApi.Services;
using Xunit;

namespace WebApiTests
{
    public class FilmControllerTests
    {
        private readonly FilmController _sut;
        private readonly Mock<IFilmService> _filmService = new();

        public FilmControllerTests()
        {
            _sut = new FilmController(_filmService.Object);
        }

        [Fact]
        public void GetAll_ShouldReturnStatusCode200_WhenDataExists()
        {
            // Arrange
            List<FilmResponse> Films = new();

            Films.Add(new FilmResponse
            {
                FilmId = 1,
                FilmName = "The lord of the rings",
                ReleaseDate = "16-09-2001",
                RuntimeInMin = 123,
                Description = "This movie is about a ring",
                Price = 79.99,
                Stock = 50,
                Image = "C:\\Users\\Tec\\Pictures\\1.jpg"
            });

            Films.Add(new FilmResponse
            {
                FilmId = 2,
                FilmName = "Harry potter",
                ReleaseDate = "16-09-2001",
                RuntimeInMin = 123,
                Description = "This movie is about the wizard world",
                Price = 79.99,
                Stock = 50,
                Image = "C:\\Users\\Tec\\Pictures\\2.jpg"
            });

            _filmService
                .Setup(s => s.GetAllFilms())
                .Returns(Films);

            // Act
            var result = _sut.GetAll();

            // Assert
            var statusCodeResult = (IStatusCodeActionResult)result;
            Assert.Equal(200, statusCodeResult.StatusCode);
        }

        [Fact]
        public void GetAll_ShouldReturnStatusCode204_WhenNoDataExists()
        {
            // Arrange
            List<FilmResponse> Films = new();

            _filmService
                .Setup(s => s.GetAllFilms())
                .Returns(Films);

            // Act
            var result = _sut.GetAll();

            // Assert
            var statusCodeResult = (IStatusCodeActionResult)result;
            Assert.Equal(204, statusCodeResult.StatusCode);
        }

        [Fact]
        public void GetAll_ShouldReturnStatusCode500_WhenNullIsReturnedFromService()
        {
            // Arrange
            _filmService
                .Setup(s => s.GetAllFilms())
                .Returns(() => null);

            // Act
            var result = _sut.GetAll();

            // Assert
            var statusCodeResult = (IStatusCodeActionResult)result;
            Assert.Equal(500, statusCodeResult.StatusCode);
        }
    }
}
