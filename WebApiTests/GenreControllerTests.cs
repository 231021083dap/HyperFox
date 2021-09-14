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
        public void GetAll_ShouldReturnStatusCode200_WhenDataExists()
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
                .Returns(Genres);

            // Act
            var result = _sut.GetAll();

            // Assert
            var statusCodeResult = (IStatusCodeActionResult)result;
            Assert.Equal(200, statusCodeResult.StatusCode);
        }
    }
}
