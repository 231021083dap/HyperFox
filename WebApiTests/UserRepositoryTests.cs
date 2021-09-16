using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApi.Database;
using WebApi.Entities;
using WebApi.Repositories;
using Xunit;

namespace WebApiTests
{
    public class UserRepositoryTests
    {
        private readonly UserRepository _sut;
        private readonly WebApiContext _context;
        private readonly DbContextOptions<WebApiContext> _options;

        public UserRepositoryTests()
        {
            _options = new DbContextOptionsBuilder<WebApiContext>()
                .UseInMemoryDatabase(databaseName: "WebApi") //Simulation af database
                .Options;

            _context = new WebApiContext(_options);

            _sut = new UserRepository(_context);
        }


        [Fact]
        public async Task GetAll_ShouldReturnListOfUsers_WhenUsersExists()
        {
            //Arange
            await _context.Database.EnsureDeletedAsync(); //Sikrer at databasen er slettet.
            _context.User.Add(new User
            {
                UserId = 1,
                UserName = "Hansen",
                Email = "Hansen@gmail.com",
                Password = "passw0rd"
            });

            _context.User.Add(new User
            {
                UserId = 2,
                UserName = "Hansen",
                Email = "Hansen@gmail.com",
                Password = "passw0rd"
            });
            await _context.SaveChangesAsync();

            //Act
            var result = await _sut.GetAll(); 


            //Assert
            Assert.NotNull(result);
            Assert.Equal(2, result.Count);
            Assert.IsType<List<User>>(result);
        }


        [Fact]
        public async Task GetAll_ShouldReturnEmptyListOfUsers_WhenNoUsersExists()
        {
            //Arange
            await _context.Database.EnsureDeletedAsync(); //Sikrer at databasen er slettet.

            //Act
            var result = await _sut.GetAll();


            //Assert
            Assert.NotNull(result);
            Assert.Empty(result);
            Assert.IsType<List<User>>(result);
        }

        [Fact]
        public async Task GetById_ShouldReturnTheUser_IfUserExists()
        {
            //Arrange
            await _context.Database.EnsureDeletedAsync();
            int userId = 1;
            _context.User.Add(new User
            {
                UserId = userId,
                UserName = "Hansen",
                Email = "Hansen@gmail.com",
                Password = "passw0rd"
            });
            await _context.SaveChangesAsync();

            //Act
            var result = await _sut.GetById(userId);

            //Assert
            Assert.NotNull(result);
            Assert.IsType<User>(result);
            Assert.Equal(userId, result.UserId);
        }

        [Fact]
        public async Task GetById_shouldReturnNull_IfUserDoesNotExists()
        {
            //Arange
            await _context.Database.EnsureDeletedAsync();
            int userId = 1;

            //Act
            var result = await _sut.GetById(userId);

            //Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task Create_ShouldAddIdToUser_WhenSavingToDatabase()
        {
            //Arange
            await _context.Database.EnsureDeletedAsync();
            int expectedId = 1;
            User user = new User
            {
                UserName = "Hansen",
                Email = "Hansen@gmail.com",
                Password = "passw0rd"
            };

            //Act
            var result = await _sut.Create(user);


            //Assert
            Assert.NotNull(result);
            Assert.IsType<User>(result);
            Assert.Equal(expectedId, result.UserId);
        }
    
        [Fact]
        public async Task Create_ShouldFailToaddUser_WhenAddingUserWithExistingId()
        {
            //Arange
            await _context.Database.EnsureDeletedAsync();

            User user = new User
            {
                UserId = 1,
                UserName = "Hansen",
                Email = "Hansen@gmail.com",
                Password = "passw0rd"
            };
            _context.User.Add(user);
            await _context.SaveChangesAsync();

            //Act
            Func<Task> action = async () => await _sut.Create(user);

            //Assert
            var ex = await Assert.ThrowsAsync<ArgumentException>(action);
            Assert.Contains("An item with the same key has already been added", ex.Message);
        }
    
        [Fact]
        public async Task Update_ShouldChangeValueOnUser_WhenUserExists()
        {

            //Arange
            await _context.Database.EnsureDeletedAsync();
            int userId = 1;
            User user = new User
            {
                UserId = userId,
                UserName = "Hansen",
                Email = "Hansen@gmail.com",
                Password = "passw0rd"
            };
            _context.User.Add(user);
            await _context.SaveChangesAsync();

            User updateUser = new User
            {
                UserId = userId,
                UserName = "Hansen",
                Email = "Hansen@gmail.com",
                Password = "passw0rd"
            };

            //Act
            var result = await _sut.Update(userId, updateUser);


            //Assert
            Assert.NotNull(result);
            Assert.IsType<User>(result);
            Assert.Equal(userId, result.UserId);
            Assert.Equal(updateUser.UserName, result.UserName);
            Assert.Equal(updateUser.Email, result.Email);
            Assert.Equal(updateUser.Password, result.Password);
        }

        [Fact]
        public async Task Update_ShouldReturnNull_WhenUserDoesNotExists()
        {
            //Arrange
            await _context.Database.EnsureDeletedAsync();
            int userId = 1;
            User updateUser = new User
            {
                UserId = userId,
                UserName = "Hansen",
                Email = "Hansen@gmail.com",
                Password = "passw0rd"
            };


            //Act
            var result = await _sut.Update(userId, updateUser);

            //Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task Delete_ShouldreturnDeletedUser_WhenUserIsDeleted()
        {
            //Arrange
            await _context.Database.EnsureDeletedAsync();
            int userId = 1;
            User user = new User
            {
                UserId = userId,
                UserName = "Hansen",
                Email = "Hansen@gmail.com",
                Password = "passw0rd"
            };
            _context.User.Add(user);
            await _context.SaveChangesAsync();

            var result = await _sut.Delete(userId);
            var users = await _sut.GetAll();

            //Assert
            Assert.NotNull(result);
            Assert.IsType<User>(result);
            Assert.Equal(userId, result.UserId);

            Assert.Empty(users);
        }
        
        [Fact]
        public async Task Delete_shouldReturnNull_WhenUsersDoesNotExists()
        {
            //Arrange
            await _context.Database.EnsureDeletedAsync();
            int userId = 1;
            
            //Act
            var result = await _sut.Delete(userId);

            //Assert
            Assert.Null(result);
        }
    }
}
