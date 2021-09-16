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

namespace WebApiTests
{
    public class UserControllerTests
    {
        //Til at kunne teste.
        private readonly UserController _sut;
        private readonly Mock<IUserService> _userService = new();

        //Skulle forstille vores autherService (bruges til test).
        public UserControllerTests() //Contructor
        {
            _sut = new UserController(_userService.Object);
        }

        [Fact] // Det den burde return
        public async void GetAll_ShouldReturnStatusCode200_whenDataExist()
        {
            //Arange - Hvordan skal den se ud.
            List<UserResponse> users = new();

            users.Add(new UserResponse
            {
                UserId = 1,
                UserName = "Hansen",
                Email = "Hansen@gmail.com",
                Password = "passw0rd"
            });

            users.Add(new UserResponse
            {
                UserId = 2,
                UserName = "Petersen",
                Email = "Petersen@gmail.com",
                Password = "passw0rd"
            });

            _userService
                .Setup(s => s.GetAllUsers())
                .ReturnsAsync(users);




            //Act - Udfører test om at få alle Users.
            var result = await _sut.GetAll();

            //Assert - Kigger på resultatet.
            var statusCodeResult = (IStatusCodeActionResult)result;
            Assert.Equal(200, statusCodeResult.StatusCode);


        }

        [Fact] 
        public async void GetAll_ShouldReturnStatusCode204_whenNoDataExist()
        {
            //Arange - Hvordan skal den se ud.
            List<UserResponse> users = new();

            _userService
                .Setup(s => s.GetAllUsers())
                .ReturnsAsync(users);


            //Act - Udfører test om at få alle Users.
            var result = await _sut.GetAll();

            //Assert - Kigger på resultatet.
            var statusCodeResult = (IStatusCodeActionResult)result;
            Assert.Equal(204, statusCodeResult.StatusCode);


        }

        [Fact]
        public async void GetAll_ShouldReturnStatusCode500_whenNullIsReturnedFromService()
        {
            //Arange - Hvordan skal den se ud.
            List<UserResponse> users = new();

            _userService
                .Setup(s => s.GetAllUsers())
                .Returns(() => null);


            //Act - Udfører test om at få alle Users.
            var result = await _sut.GetAll();

            //Assert - Kigger på resultatet.
            var statusCodeResult = (IStatusCodeActionResult)result;
            Assert.Equal(500, statusCodeResult.StatusCode);


        }

        [Fact] 
        public async void GetAll_ShouldReturnStatusCode500_whenExeptionIsRaised()
        {
            //Arange - Hvordan skal den se ud.
            List<UserResponse> users = new();

            _userService
                .Setup(s => s.GetAllUsers())
                .ReturnsAsync(() => throw new Exception("This is an exeption")); //Får vores kode til at fejle med vilje


            //Act - Udfører test om at få alle Users.
            var result = await _sut.GetAll();

            //Assert - Kigger på resultatet.
            var statusCodeResult = (IStatusCodeActionResult)result;
            Assert.Equal(500, statusCodeResult.StatusCode);


        }


        [Fact]
        public async void GetById_ShouldReturnStatusCode200_WhenDataExists()
        {
            // Arrange
            int userId = 1;
            UserResponse user = new UserResponse
            {
                UserId = userId,
                UserName = "Hansen",
                Email = "Hansen@gmail.com",
                Password = "passw0rd"
            };

            _userService
                .Setup(s => s.GetById(It.IsAny<int>()))
                .ReturnsAsync(user);

            // Act
            var result = await _sut.GetById(userId);

            // Assert
            var statusCodeResult = (IStatusCodeActionResult)result;
            Assert.Equal(200, statusCodeResult.StatusCode);
        }

        [Fact]
        public async void GetById_ShouldReturnStatusCode404_WhenUserDoesNotExist()
        {
            // Arrange
            int userId = 1;

            _userService
                .Setup(s => s.GetById(It.IsAny<int>()))
                .ReturnsAsync(() => null);
            // Act
            var result = await _sut.GetById(userId);

            // Assert
            var statusCodeResult = (IStatusCodeActionResult)result;
            Assert.Equal(404, statusCodeResult.StatusCode);
        }

        [Fact]
        public async void GetById_ShouldReturnStatusCode500_WhenExceptionIsRaised()
        {
            // Arrange
            _userService
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
            int userId = 1;
            NewUser newUser = new NewUser
            {
                
                UserName = "Hansen",
                Email = "Hansen@gmail.com",
                Password = "passw0rd"
            };

            UserResponse user = new UserResponse
            {
                UserId = userId,
                UserName = "Hansen",
                Email = "Hansen@gmail.com",
                Password = "passw0rd"
            };

            _userService
                .Setup(s => s.Create(It.IsAny<NewUser>()))
                .ReturnsAsync(user);

            // Act
            var result = await _sut.Create(newUser);

            // Assert
            var statusCodeResult = (IStatusCodeActionResult)result;
            Assert.Equal(200, statusCodeResult.StatusCode);
        }

        [Fact]
        public async void Create_ShouldReturnStatusCode500_WhenExceptionIsRaised()
        {
            // Arrange
            NewUser newUser = new NewUser
            {
                UserName = "Hansen",
                Email = "Hansen@gmail.com",
                Password = "passw0rd"
            };

            _userService
                .Setup(s => s.Create(It.IsAny<NewUser>()))
                .ReturnsAsync(() => throw new System.Exception("This is an exception"));

            // Act
            var result = await _sut.Create(newUser);

            // Assert
            var statusCodeResult = (IStatusCodeActionResult)result;
            Assert.Equal(500, statusCodeResult.StatusCode);
        }

        [Fact]
        public async void Update_ShouldReturnStatusCode200_WhenDataIsSaved()
        {
            // Arrange
            int userId = 1;
            UpdateUser updateUser = new UpdateUser
            {
                
                UserName = "Hansen",
                Email = "Hansen@gmail.com",
                Password = "passw0rd"
            };

            UserResponse user = new UserResponse
            {
                UserId = userId,
                UserName = "Hansen",
                Email = "Hansen@gmail.com",
                Password = "passw0rd"
            };

            _userService
                .Setup(s => s.Update(It.IsAny<int>(), It.IsAny<UpdateUser>()))
                .ReturnsAsync(user);

            // Act
            var result = await _sut.Update(userId, updateUser);

            // Assert
            var statusCodeResult = (IStatusCodeActionResult)result;
            Assert.Equal(200, statusCodeResult.StatusCode);
        }

        [Fact]
        public async void Update_ShouldReturnStatusCode500_WhenExceptionIsRaised()
        {
            // Arrange
            int userId = 1;
            UpdateUser updateUser = new UpdateUser
            {
               
                UserName = "Hansen",
                Email = "Hansen@gmail.com",
                Password = "passw0rd"
            };

            _userService
                .Setup(s => s.Update(It.IsAny<int>(), It.IsAny<UpdateUser>()))
                .ReturnsAsync(() => throw new System.Exception("This is an exception"));

            // Act
            var result = await _sut.Update(userId, updateUser);

            // Assert
            var statusCodeResult = (IStatusCodeActionResult)result;
            Assert.Equal(500, statusCodeResult.StatusCode);
        }

        [Fact]
        public async void Delete_ShouldReturnStatusCode204_WhenUserIsDeleted()
        {
            // Arrange
            int userId = 1;

            _userService
                .Setup(s => s.Delete(It.IsAny<int>()))
                .ReturnsAsync(true);

            // Act
            var result = await _sut.Delete(userId);

            // Assert
            var statusCodeResult = (IStatusCodeActionResult)result;
            Assert.Equal(204, statusCodeResult.StatusCode);
        }

        [Fact]
        public async void Delete_ShouldReturnStatusCode500_WhenExceptionIsRaised()
        {
            // Arrange
            int userId = 1;

            _userService
                .Setup(s => s.Delete(It.IsAny<int>()))
                .ReturnsAsync(() => throw new System.Exception("This is an exception"));

            // Act
            var result = await _sut.Delete(userId);

            // Assert
            var statusCodeResult = (IStatusCodeActionResult)result;
            Assert.Equal(500, statusCodeResult.StatusCode);
        }
    }
}
