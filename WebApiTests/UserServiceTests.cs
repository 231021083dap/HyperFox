using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApi.DTOs.Requests;
using WebApi.DTOs.Responses;
using WebApi.Entities;
using WebApi.Repositories;
using WebApi.Services;
using Xunit;


namespace WebApiTests
{
    public class UserServiceTests
    {

         // Variabler
        private readonly UserService _sut;
        private readonly Mock<IUserRepository> _userRepository = new();

        //Contructor 
        public UserServiceTests()
        {
            _sut = new UserService(_userRepository.Object);
        }

        [Fact]
        public async void GetAll_ShouldReturnListOfUserResponses_WhenUsersExists()
        {
            // Arange
            List<User> users = new();

            users.Add(new User
            {
                UserId = 1,
                UserName = "Hansen",
                Email = "Hansen@gmail.com",
                Password = "passw0rd"
            });

            users.Add(new User
            {
                UserId = 1,
                UserName = "Hansen",
                Email = "Hansen@gmail.com",
                Password = "passw0rd"
            });

            //Får alle Users og så returnerer dem.
            _userRepository
                .Setup(a => a.GetAll())
                .ReturnsAsync(users);

            // act
            var result = await _sut.GetAllUsers();



            // Assert
            Assert.NotNull(result);
            Assert.Equal(2, result.Count);
            Assert.IsType<List<UserResponse>>(result);


        }
        [Fact]
        public async void GetAll_ShouldReturnEmptyListOfUserReponses_WhenNoUserExists()
        {
            //Arrange
            List<User> Users = new List<User>();

            _userRepository.Setup(a => a.GetAll()).ReturnsAsync(Users);

            //Act
            var result = await _sut.GetAllUsers();

            //Assert
            Assert.NotNull(result);
            Assert.Empty(result);
            Assert.IsType<List<UserResponse>>(result);
        }

        [Fact]
        public async void GetById_ShouldReturnAnUserResponse_WhenUserExists()
        {
            //Arrange
            int userId = 1;

            User user = new User
            {
                UserId = userId,
                UserName = "Goe",
                Email = "Leo@gmail.com",
                Password = "passw0rd"
            };

            _userRepository.Setup(a => a.GetById(It.IsAny<int>())).ReturnsAsync(user);

            //Act 
            var result = await _sut.GetById(userId);

            //Assert
            Assert.NotNull(result);
            Assert.IsType<UserResponse>(result);
            Assert.Equal(user.UserId, result.UserId);
            Assert.Equal(user.UserName, result.UserName);
            Assert.Equal(user.Email, result.Email);

        }

        [Fact]
        public async void GetById_ShouldReturnNull_WhenUserDoesNotExists()
        {
            //Arrange
            int userId = 1;

            _userRepository.Setup(a => a.GetById(It.IsAny<int>())).ReturnsAsync(() => null);

            //Act
            var result = await _sut.GetById(userId);

            //Assert
            Assert.Null(result);
        }

        [Fact]
        public async void Create_ShouldReturnUserResponse_WhenCreateIsSuccess()
        {
            //Arrange
            NewUser newUser = new NewUser
            {

                UserName = "SomeJoe",
                Email = "JoeMama@gmail.com",
                Password = "Johnson"
            };

            int userId = 1;
            User user = new User
            {
                UserId = userId,
                UserName = "SomeJoe",
                Email = "JoeMama@gmail.com",
                Password = "Johnson"

            };

            _userRepository
                .Setup(a => a.Create(It.IsAny<User>()))
                .ReturnsAsync(user);


            //Act
            var result = await _sut.Create(newUser);

            //Assert
            Assert.NotNull(result);
            Assert.IsType<UserResponse>(result);
            Assert.Equal(userId, result.UserId);
            Assert.Equal(newUser.UserName, result.UserName);
            Assert.Equal(newUser.Email, result.Email);

        }

        [Fact]
        public async void Update_shouldReturnUpdateUserResponse_WhenUpdateIsSucces()
        {
            //Arrange
            UpdateUser updateUser = new UpdateUser
            {
                UserName = "SomeJoe",
                Email = "JoeMama@gmail.com",
                Password = "Johnson"
            };

            int userId = 1;

            User user = new User
            {
                UserId = userId,
                UserName = "SomeJoe",
                Email = "JoeMama@gmail.com",
                Password = "Johnson"
            };

            _userRepository.Setup(a => a.Update(It.IsAny<int>(), It.IsAny<User>())).ReturnsAsync(user);

            //Act
            var result = await _sut.Update(userId, updateUser);

            //Assert
            Assert.NotNull(result);
            Assert.IsType<UserResponse>(result);
            Assert.Equal(userId, result.UserId);
            Assert.Equal(updateUser.UserName, result.UserName);
            Assert.Equal(updateUser.Email, result.Email);


        }

        [Fact]
        public async void Update_ShouldreturnNull_WhenUserDoesNotExists()
        {
            //Arrange
            UpdateUser updateUser = new UpdateUser
            {
                UserName = "SomeJoe",
                Email = "JoeMama@gmail.com",
                Password = "Johnson"
            };

            int userId = 1;

            _userRepository.Setup(a => a.Update(It.IsAny<int>(), It.IsAny<User>())).ReturnsAsync(() => null);

            //Act
            var result = await _sut.Update(userId, updateUser);

            //Assert
            Assert.Null(result);
        }

        [Fact]
        public async void Delete_shouldReturnTrue_WhenDeleteIsSuccess()
        {
            //Arrange
            int userId = 1;

            User user = new User
            {
                UserId = userId,
                UserName = "SomeJoe",
                Email = "JoeMama@gmail.com",
                Password = "Johnson"
            };

            _userRepository.Setup(a => a.Delete(It.IsAny<int>())).ReturnsAsync(user);

            //Act
            var result = await _sut.Delete(userId);

            //Assert
            Assert.True(result);
        }
    }
}
