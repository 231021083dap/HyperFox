using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApiTests
{
    class UserServiceTests
    {

         // Variabler
        private readonly AuthorService _sut;
        private readonly Mock<IAuthorRepository> _authorRepositories = new();

        //Contructor 
        public AuthorServiceTests()
        {
            _sut = new AuthorService(_authorRepositories.Object);
        }

        [Fact]
        public async void GetAll_ShouldReturnListOfAuthorResponses_WhenAchtorsExists()
        {
            // Arange
            List<Author> authors = new();

            authors.Add(new Author
            {
                id = 1,
                FirstName = "Hansen",
                LastName = "Jensen",
                MiddleName = "Karn"
            });

            authors.Add(new Author
            {
                id = 1,
                FirstName = "Hansen",
                LastName = "Jensen",
                MiddleName = "Karn"
            });

            //Får alle Authors og så returnerer dem.
            _authorRepositories
                .Setup(a => a.GetAll())
                .ReturnsAsync(authors);

            // act
            var result = await _sut.GetAllAuthors();



            // Assert
            Assert.NotNull(result);
            Assert.Equal(2, result.Count);
            Assert.IsType<List<AuthorResponse>>(result);


        }
        [Fact]
        public async void GetAll_ShouldReturnEmptyListOfAuthorReponses_WhenNoAuthorExists()
        {
            //Arrange
            List<Author> Authors = new List<Author>();

            _authorRepositories.Setup(a => a.GetAll()).ReturnsAsync(Authors);

            //Act
            var result = await _sut.GetAllAuthors();

            //Assert
            Assert.NotNull(result);
            Assert.Empty(result);
            Assert.IsType<List<AuthorResponse>>(result);
        }

        [Fact]
        public async void GetById_ShouldReturnAnAuthorResponse_WhenAuthorExists()
        {
            //Arrange
            int authorId = 1;

            Author author = new Author
            {
                id = authorId,
                FirstName = "Goe",
                LastName = "Leo",
                MiddleName = "trio"
            };

            _authorRepositories.Setup(a => a.GetbyId(It.IsAny<int>())).ReturnsAsync(author);

            //Act 
            var result = await _sut.GetById(authorId);

            //Assert
            Assert.NotNull(result);
            Assert.IsType<AuthorResponse>(result);
            Assert.Equal(author.id, result.id);
            Assert.Equal(author.FirstName, result.FirstName);
            Assert.Equal(author.LastName, result.LastName);
            Assert.Equal(author.MiddleName, result.MiddleName);
        }

        [Fact]
        public async void GetById_ShouldReturnNull_WhenAuthorDoesNotExists()
        {
            //Arrange
            int authorId = 1;

            _authorRepositories.Setup(a => a.GetbyId(It.IsAny<int>())).ReturnsAsync(() => null);

            //Act
            var result = await _sut.GetById(authorId);

            //Assert
            Assert.Null(result);
        }

        [Fact]
        public async void Create_ShouldReturnAuthorResponse_WhenCreateIsSuccess()
        {
            //Arrange
            NewAuthor newAuthor = new NewAuthor
            {

                FirstName = "SomeJoe",
                LastName = "JoeMama",
                MiddleName = "Johnson"
            };

            int authorId = 1;
            Author author = new Author
            {
                id = authorId,
                FirstName = "SomeJoe",
                LastName = "JoeMama",
                MiddleName = "Johnson"

            };

            _authorRepositories
                .Setup(a => a.Create(It.IsAny<Author>()))
                .ReturnsAsync(author);


            //Act
            var result = await _sut.Create(newAuthor);

            //Assert
            Assert.NotNull(result);
            Assert.IsType<AuthorResponse>(result);
            Assert.Equal(authorId, result.id);
            Assert.Equal(newAuthor.FirstName, result.FirstName);
            Assert.Equal(newAuthor.LastName, result.LastName);
            Assert.Equal(newAuthor.MiddleName, result.MiddleName);
        }

        [Fact]
        public async void Update_shouldReturnUpdateAuthorResponse_WhenUpdateIsSucces()
        {
            //Arrange
            UpdateAuthor updateAuthor = new UpdateAuthor
            {
                FirstName = "SomeJoe",
                LastName = "JoeMama",
                MiddleName = "Johnson"
            };

            int authorId = 1;

            Author author = new Author
            {
                id = authorId,
                FirstName = "SomeJoe",
                LastName = "JoeMama",
                MiddleName = "Johnson"
            };

            _authorRepositories.Setup(a => a.Update(It.IsAny<int>(), It.IsAny<Author>())).ReturnsAsync(author);

            //Act
            var result = await _sut.Update(authorId, updateAuthor);

            //Assert
            Assert.NotNull(result);
            Assert.IsType<AuthorResponse>(result);
            Assert.Equal(authorId, result.id);
            Assert.Equal(updateAuthor.FirstName, result.FirstName);
            Assert.Equal(updateAuthor.LastName, result.LastName);
            Assert.Equal(updateAuthor.MiddleName, result.MiddleName);

        }

        [Fact]
        public async void Update_ShouldreturnNull_WhenAuthorDoesNotExists()
        {
            //Arrange
            UpdateAuthor updateAuthor = new UpdateAuthor
            {
                FirstName = "Jens",
                LastName = "Jensen",
                MiddleName = "Jensnens"
            };

            int authorId = 1;

            _authorRepositories.Setup(a => a.Update(It.IsAny<int>(), It.IsAny<Author>())).ReturnsAsync(() => null);

            //Act
            var result = await _sut.Update(authorId, updateAuthor);

            //Assert
            Assert.Null(result);
        }

        [Fact]
        public async void Delete_shouldReturnTrue_WhenDeleteIsSuccess()
        {
            //Arrange
            int authorId = 1;

            Author author = new Author
            {
                id = authorId,
                FirstName = "joe",
                LastName = "jep",
                MiddleName = "je"
            };

            _authorRepositories.Setup(a => a.Delete(It.IsAny<int>())).ReturnsAsync(author);

            //Act
            var result = await _sut.Delete(authorId);

            //Assert
            Assert.True(result);
        }
    }
}
