using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApiTests
{
    class UserControllerTests
    {
        //Til at kunne teste.
        private readonly AuthorController _sut;
        private readonly Mock<IAuthorService> _authorService = new();

        //Skulle forstille vores autherService (bruges til test).
        public AuthorControllerTest() //Contructor
        {
            _sut = new AuthorController(_authorService.Object);
        }

        [Fact] // Det den burde return
        public async void GetAll_ShouldReturnStatusCode200_whenDataExist()
        {
            //Arange - Hvordan skal den se ud.
            List<AuthorResponse> authors = new();

            authors.Add(new AuthorResponse
            {
                id = 1,
                FirstName = "Hansen",
                LastName = "Jensen",
                MiddleName = "Karn"
            });

            authors.Add(new AuthorResponse
            {
                id = 2,
                FirstName = "Bo",
                LastName = "Ivansen",
                MiddleName = "Joe"
            });

            _authorService
                .Setup(s => s.GetAllAuthors())
                .ReturnsAsync(authors);




            //Act - Udfører test om at få alle Authors.
            var result = await _sut.GetAll();

            //Assert - Kigger på resultatet.
            var statusCodeResult = (IStatusCodeActionResult)result;
            Assert.Equal(200, statusCodeResult.StatusCode);


        }

        [Fact] 
        public async void GetAll_ShouldReturnStatusCode204_whenNoDataExist()
        {
            //Arange - Hvordan skal den se ud.
            List<AuthorResponse> authors = new();

            _authorService
                .Setup(s => s.GetAllAuthors())
                .ReturnsAsync(authors);


            //Act - Udfører test om at få alle Authors.
            var result = await _sut.GetAll();

            //Assert - Kigger på resultatet.
            var statusCodeResult = (IStatusCodeActionResult)result;
            Assert.Equal(204, statusCodeResult.StatusCode);


        }

        [Fact]
        public async void GetAll_ShouldReturnStatusCode500_whenNullIsReturnedFromService()
        {
            //Arange - Hvordan skal den se ud.
            List<AuthorResponse> authors = new();

            _authorService
                .Setup(s => s.GetAllAuthors())
                .Returns(() => null);


            //Act - Udfører test om at få alle Authors.
            var result = await _sut.GetAll();

            //Assert - Kigger på resultatet.
            var statusCodeResult = (IStatusCodeActionResult)result;
            Assert.Equal(500, statusCodeResult.StatusCode);


        }

        [Fact] 
        public async void GetAll_ShouldReturnStatusCode500_whenExeptionIsRaised()
        {
            //Arange - Hvordan skal den se ud.
            List<AuthorResponse> authors = new();

            _authorService
                .Setup(s => s.GetAllAuthors())
                .ReturnsAsync(() => throw new Exception("This is an exeption")); //Får vores kode til at fejle med vilje


            //Act - Udfører test om at få alle Authors.
            var result = await _sut.GetAll();

            //Assert - Kigger på resultatet.
            var statusCodeResult = (IStatusCodeActionResult)result;
            Assert.Equal(500, statusCodeResult.StatusCode);


        }


        [Fact]
        public async void GetById_ShouldReturnStatusCode200_WhenDataExists()
        {
            // Arrange
            int authorId = 1;
            AuthorResponse author = new AuthorResponse
            {
                id = authorId,
                FirstName = "George",
                LastName = "Martin",
                MiddleName = "R.R."
            };

            _authorService
                .Setup(s => s.GetById(It.IsAny<int>()))
                .ReturnsAsync(author);

            // Act
            var result = await _sut.GetById(authorId);

            // Assert
            var statusCodeResult = (IStatusCodeActionResult)result;
            Assert.Equal(200, statusCodeResult.StatusCode);
        }

        [Fact]
        public async void GetById_ShouldReturnStatusCode404_WhenAuthorDoesNotExist()
        {
            // Arrange
            int authorId = 1;

            _authorService
                .Setup(s => s.GetById(It.IsAny<int>()))
                .ReturnsAsync(() => null);
            // Act
            var result = await _sut.GetById(authorId);

            // Assert
            var statusCodeResult = (IStatusCodeActionResult)result;
            Assert.Equal(404, statusCodeResult.StatusCode);
        }

        [Fact]
        public async void GetById_ShouldReturnStatusCode500_WhenExceptionIsRaised()
        {
            // Arrange
            _authorService
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
            int authorId = 1;
            NewAuthor newAuthor = new NewAuthor
            {
                FirstName = "George",
                LastName = "Martin",
                MiddleName = "R.R."
            };

            AuthorResponse author = new AuthorResponse
            {
                id = authorId,
                FirstName = "George",
                LastName = "Martin",
                MiddleName = "R.R."
            };

            _authorService
                .Setup(s => s.Create(It.IsAny<NewAuthor>()))
                .ReturnsAsync(author);

            // Act
            var result = await _sut.Create(newAuthor);

            // Assert
            var statusCodeResult = (IStatusCodeActionResult)result;
            Assert.Equal(200, statusCodeResult.StatusCode);
        }

        [Fact]
        public async void Create_ShouldReturnStatusCode500_WhenExceptionIsRaised()
        {
            // Arrange
            NewAuthor newAuthor = new NewAuthor
            {
                FirstName = "George",
                LastName = "Martin",
                MiddleName = "R.R."
            };

            _authorService
                .Setup(s => s.Create(It.IsAny<NewAuthor>()))
                .ReturnsAsync(() => throw new System.Exception("This is an exception"));

            // Act
            var result = await _sut.Create(newAuthor);

            // Assert
            var statusCodeResult = (IStatusCodeActionResult)result;
            Assert.Equal(500, statusCodeResult.StatusCode);
        }

        [Fact]
        public async void Update_ShouldReturnStatusCode200_WhenDataIsSaved()
        {
            // Arrange
            int authorId = 1;
            UpdateAuthor updateAuthor = new UpdateAuthor
            {
                FirstName = "George",
                LastName = "Martin",
                MiddleName = "R.R."
            };

            AuthorResponse author = new AuthorResponse
            {
                id = authorId,
                FirstName = "George",
                LastName = "Martin",
                MiddleName = "R.R."
            };

            _authorService
                .Setup(s => s.Update(It.IsAny<int>(), It.IsAny<UpdateAuthor>()))
                .ReturnsAsync(author);

            // Act
            var result = await _sut.Update(authorId, updateAuthor);

            // Assert
            var statusCodeResult = (IStatusCodeActionResult)result;
            Assert.Equal(200, statusCodeResult.StatusCode);
        }

        [Fact]
        public async void Update_ShouldReturnStatusCode500_WhenExceptionIsRaised()
        {
            // Arrange
            int authorId = 1;
            UpdateAuthor updateAuthor = new UpdateAuthor
            {
                FirstName = "George",
                LastName = "Martin",
                MiddleName = "R.R."
            };

            _authorService
                .Setup(s => s.Update(It.IsAny<int>(), It.IsAny<UpdateAuthor>()))
                .ReturnsAsync(() => throw new System.Exception("This is an exception"));

            // Act
            var result = await _sut.Update(authorId, updateAuthor);

            // Assert
            var statusCodeResult = (IStatusCodeActionResult)result;
            Assert.Equal(500, statusCodeResult.StatusCode);
        }

        [Fact]
        public async void Delete_ShouldReturnStatusCode204_WhenAuthorIsDeleted()
        {
            // Arrange
            int authorId = 1;

            _authorService
                .Setup(s => s.Delete(It.IsAny<int>()))
                .ReturnsAsync(true);

            // Act
            var result = await _sut.Delete(authorId);

            // Assert
            var statusCodeResult = (IStatusCodeActionResult)result;
            Assert.Equal(204, statusCodeResult.StatusCode);
        }

        [Fact]
        public async void Delete_ShouldReturnStatusCode500_WhenExceptionIsRaised()
        {
            // Arrange
            int authorId = 1;

            _authorService
                .Setup(s => s.Delete(It.IsAny<int>()))
                .ReturnsAsync(() => throw new System.Exception("This is an exception"));

            // Act
            var result = await _sut.Delete(authorId);

            // Assert
            var statusCodeResult = (IStatusCodeActionResult)result;
            Assert.Equal(500, statusCodeResult.StatusCode);
        }
    }
}
