using Microsoft.AspNetCore.Mvc.Infrastructure;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApi.Controllers;
using WebApi.DTOs.Requests;
using WebApi.DTOs.Responses;
using WebApi.Services;
using Xunit;

namespace WebApiTests.ControllerTests
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

        [Fact]
        public async void GetById_ShouldReturnStatusCode404_WhenGenreDoesNotExists()
        {
            // Arrange
            int genreId = 1;

            _genreService
                .Setup(s => s.GetById(It.IsAny<int>()))
                .ReturnsAsync(() => null);

            // Act
            var result = await _sut.GetById(genreId);

            // Assert 
            var statusCodeResult = (IStatusCodeActionResult)result;
            Assert.Equal(404, statusCodeResult.StatusCode);
        }

        [Fact]
        public async void GetById_ShouldReturnStatusCode500_WhenExceptionIsRaised()
        {
            // Arrange
            _genreService
                .Setup(s => s.GetById(It.IsAny<int>()))
                .ReturnsAsync(() => throw new System.Exception("This is an exception"));

            // Act
            var result = await _sut.GetById(1);

            // Assert 
            var statusCodeResult = (IStatusCodeActionResult)result;
            Assert.Equal(500, statusCodeResult.StatusCode);
        }

        [Fact]
        public async void Create_ShouldReturnStatusCode200_WhenDataIsCreated()
        {
            // Arrange
            int genreId = 1;

            NewGenre newGenre = new NewGenre
            {
                GenreName = "Action"
            };

            GenreResponse genre = new GenreResponse
            {
                GenreId = genreId,
                GenreName = "Action"
            };

            _genreService
                .Setup(s => s.Create(It.IsAny<NewGenre>()))
                .ReturnsAsync(genre);

            // Act
            var result = await _sut.Create(newGenre);

            // Assert 
            var statusCodeResult = (IStatusCodeActionResult)result;
            Assert.Equal(200, statusCodeResult.StatusCode);
        }

        [Fact]
        public async void Create_ShouldReturnStatusCode500_WhenExeptionIsRaised()
        {
            // Arrange
            NewGenre newGenre = new NewGenre
            {
                GenreName = "Action"
            };

            _genreService
                .Setup(s => s.Create(It.IsAny<NewGenre>()))
                .ReturnsAsync(() => throw new System.Exception("This is an exception"));

            // Act
            var result = await _sut.Create(newGenre);

            // Assert 
            var statusCodeResult = (IStatusCodeActionResult)result;
            Assert.Equal(500, statusCodeResult.StatusCode);
        }

        [Fact]
        public async void Delete_ShouldReturnStatusCode204_WhenGenreIsDeleted()
        {
            // Arrange
            int genreId = 1;

            _genreService
                .Setup(s => s.Delete(It.IsAny<int>()))
                .ReturnsAsync(true);

            // Act
            var result = await _sut.Delete(genreId);

            // Assert 
            var statusCodeResult = (IStatusCodeActionResult)result;
            Assert.Equal(204, statusCodeResult.StatusCode);
        }

        [Fact]
        public async void Delete_ShouldReturnStatusCode500_WhenExeptionIsRaised()
        {
            // Arrange
            int genreId = 1;

            _genreService
                .Setup(s => s.Delete(It.IsAny<int>()))
                .ReturnsAsync(() => throw new System.Exception("This is an exception"));

            // Act
            var result = await _sut.Delete(genreId);

            // Assert 
            var statusCodeResult = (IStatusCodeActionResult)result;
            Assert.Equal(500, statusCodeResult.StatusCode);
        }
    }
}
