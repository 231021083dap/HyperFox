using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
 using Microsoft.AspNetCore.Mvc.Infrastructure;
using WebApi.Controllers;
using Moq;
using WebApi.Services;
using WebApi.DTOs.Responses;

namespace WebApiTests
{
    public class GenreControllerTests
    {
        private readonly GenreController _sut;
        private readonly Mock<IGenreService> _genreService = new();

        public GenreControllerTests()
        {
            _sut = new GenreController(_genreService.Object);
        }

        [Fact]
        public async void GetAll_ShouldReturnStatusCode200_WhenDataExists()
        {
            // Arrange
            List<GenreResponse> Genres = new();

            Genres.Add(new GenreResponse
            {
                GenreId = 1,
                GenreName = "Action"
            });

            Genres.Add(new GenreResponse
            {
                GenreId = 2,
                GenreName = "Comedy"
            });

            _genreService
                .Setup(g => g.GetAllGenres())
                .ReturnsAsync(Genres);

            // Act
            var result = await _sut.GetAll();

            // Assert
            var statusCodeResult = (IStatusCodeActionResult)result;
            Assert.Equal(200, statusCodeResult.StatusCode);
        }

        [Fact]
        public async void GetAll_ShouldReturnStatusCode204_WhenNoElementsExists()
        {
            // Arrange
            List<GenreResponse> Genres = new();

            _genreService
                .Setup(g => g.GetAllGenres())
                .ReturnsAsync(Genres);

            // Act
            var result = await _sut.GetAll();

            // Assert
            var statusCodeResult = (IStatusCodeActionResult)result;
            Assert.Equal(204, statusCodeResult.StatusCode);
        }

        [Fact]
        public async void GetAll_ShouldReturnStatusCode500_WhenNullIsReturnedFromService()
        {
            // Arrange
            _genreService
                .Setup(g => g.GetAllGenres())
                .ReturnsAsync(() => null);

            // Act
            var result = await _sut.GetAll();

            // Assert
            var statusCodeResult = (IStatusCodeActionResult)result;
            Assert.Equal(500, statusCodeResult.StatusCode);
        }

        [Fact]
        public async void GetAll_ShouldReturnStatusCode500_WhenExceptionIsRaised()
        {
            // Arrange
            _genreService
                .Setup(g => g.GetAllGenres())
                .ReturnsAsync(() => throw new System.Exception("This is an eception"));

            // Act
            var result = await _sut.GetAll();

            // Assert
            var statusCodeResult = (IStatusCodeActionResult)result;
            Assert.Equal(500, statusCodeResult.StatusCode);
        }

        [Fact]
        public async void GetById_ShouldReturnStatusCode200_WhenDataExists()
        {
            // Arrange
            int genreId = 1;

            GenreResponse genre = new GenreResponse
            {
                GenreId = genreId,
                GenreName = "Action"
            };

            _genreService
                .Setup(s => s.GetById(It.IsAny<int>()))
                .ReturnsAsync(genre);

            // Act
            var result = await _sut.GetById(genreId);

            // Assert 
            var statusCodeResult = (IStatusCodeActionResult)result;
            Assert.Equal(200, statusCodeResult.StatusCode);
        }
    }
}
