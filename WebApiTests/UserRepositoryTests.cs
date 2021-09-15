using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApiTests
{
    class UserRepositoryTests
    {
         private readonly AuthorRepositories _sut;
        private readonly LibraryProjectContext _context;
        private readonly DbContextOptions<LibraryProjectContext> _options;

        public AuthorRepositoriesTests()
        {
            _options = new DbContextOptionsBuilder<LibraryProjectContext>()
                .UseInMemoryDatabase(databaseName: "LibraryPorject") //Simulation af database
                .Options;

            _context = new LibraryProjectContext(_options);

            _sut = new AuthorRepositories(_context);
        }


        [Fact]
        public async Task GetAll_ShouldReturnListOfAuthors_WhenAuthorsExists()
        {
            //Arange
            await _context.Database.EnsureDeletedAsync(); //Sikrer at databasen er slettet.
            _context.Author.Add(new Author
            {
                id = 1,
                FirstName = "Hansen",
                LastName = "Jensen",
                MiddleName = "Karn"
            });

            _context.Author.Add(new Author
            {
                id = 2,
                FirstName = "Hansen",
                LastName = "Jensen",
                MiddleName = "Karn"
            });
            await _context.SaveChangesAsync();

            //Act
            var result = await _sut.GetAll();


            //Assert
            Assert.NotNull(result);
            Assert.Equal(2, result.Count);
            Assert.IsType<List<Author>>(result);
        }


        [Fact]
        public async Task GetAll_ShouldReturnEmptyListOfAuthors_WhenNoAuthorsExists()
        {
            //Arange
            await _context.Database.EnsureDeletedAsync(); //Sikrer at databasen er slettet.

            //Act
            var result = await _sut.GetAll();


            //Assert
            Assert.NotNull(result);
            Assert.Empty(result);
            Assert.IsType<List<Author>>(result);
        }

        [Fact]
        public async Task GetById_ShouldReturnTheAuthor_IfAuthorExists()
        {
            //Arrange
            await _context.Database.EnsureDeletedAsync();
            int authorId = 1;
            _context.Author.Add(new Author
            {
                id = authorId,
                FirstName = "Mira",
                LastName = "Henne",
                MiddleName = "R.R"
            });
            await _context.SaveChangesAsync();

            //Act
            var result = await _sut.GetbyId(authorId);

            //Assert
            Assert.NotNull(result);
            Assert.IsType<Author>(result);
            Assert.Equal(authorId, result.id);
        }

        [Fact]
        public async Task GetById_shouldReturnNull_IfAuthorDoesNotExists()
        {
            //Arange
            await _context.Database.EnsureDeletedAsync();
            int authorId = 1;

            //Act
            var result = await _sut.GetbyId(authorId);

            //Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task Create_ShouldAddIdToAuthor_WhenSavingToDatabase()
        {
            //Arange
            await _context.Database.EnsureDeletedAsync();
            int expectedId = 1;
            Author author = new Author
            {
                FirstName = "Hansen",
                LastName = "Jensen",
                MiddleName = "Karn"
            };

            //Act
            var result = await _sut.Create(author);


            //Assert
            Assert.NotNull(result);
            Assert.IsType<Author>(result);
            Assert.Equal(expectedId, result.id);
        }
    
        [Fact]
        public async Task Create_ShouldFailToaddAuthor_WhenAddingAuthorWithExistingId()
        {
            //Arange
            await _context.Database.EnsureDeletedAsync();

            Author author = new Author
            {
                id = 1,
                FirstName = "Hanne",
                LastName = "Jensen",
                MiddleName = "P.R"
            };
            _context.Author.Add(author);
            await _context.SaveChangesAsync();

            //Act
            Func<Task> action = async () => await _sut.Create(author);

            //Assert
            var ex = await Assert.ThrowsAsync<ArgumentException>(action);
            Assert.Contains("An item with the same key has already been added", ex.Message);
        }
    
        [Fact]
        public async Task Update_ShouldChangeValueOnAuthor_WhenAuthorExists()
        {

            //Arange
            await _context.Database.EnsureDeletedAsync();
            int authorId = 1;
            Author author = new Author
            {
                id = authorId,
                FirstName = "fillip",
                LastName = "Thaiam",
                MiddleName = "Hansen"
            };
            _context.Author.Add(author);
            await _context.SaveChangesAsync();

            Author updateAuthor = new Author
            {
                id = authorId,
                FirstName = "Rita",
                LastName = "Jensen",
                MiddleName = "Ikkersen"
            };

            //Act
            var result = await _sut.Update(authorId, updateAuthor);


            //Assert
            Assert.NotNull(result);
            Assert.IsType<Author>(result);
            Assert.Equal(authorId, result.id);
            Assert.Equal(updateAuthor.FirstName, result.FirstName);
            Assert.Equal(updateAuthor.LastName, result.LastName);
            Assert.Equal(updateAuthor.MiddleName, result.MiddleName);
        }

        [Fact]
        public async Task Update_ShouldReturnNull_WhenAuthorDoesNotExists()
        {
            //Arrange
            await _context.Database.EnsureDeletedAsync();
            int authorId = 1;
            Author updateAuthor = new Author
            {
                id = authorId,
                FirstName = "Jamie",
                LastName = "Nielsen",
                MiddleName = "Has"
            };


            //Act
            var result = await _sut.Update(authorId, updateAuthor);

            //Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task Delete_ShouldreturnDeletedAuthor_WhenAuthorIsDeleted()
        {
            //Arrange
            await _context.Database.EnsureDeletedAsync();
            int authorId = 1;
            Author author = new Author
            {
                id = authorId,
                FirstName = "Hans",
                LastName = "Hansen",
                MiddleName = "Robert"
            };
            _context.Author.Add(author);
            await _context.SaveChangesAsync();

            var result = await _sut.Delete(authorId);
            var authors = await _sut.GetAll();

            //Assert
            Assert.NotNull(result);
            Assert.IsType<Author>(result);
            Assert.Equal(authorId, result.id);

            Assert.Empty(authors);
        }
        
        [Fact]
        public async Task Delete_shouldReturnNull_WhenAuthorsDoesNotExists()
        {
            //Arrange
            await _context.Database.EnsureDeletedAsync();
            int authorId = 1;
            
            //Act
            var result = await _sut.Delete(authorId);

            //Assert
            Assert.Null(result);
        }
    }
}
