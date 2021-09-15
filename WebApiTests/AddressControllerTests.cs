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
    public class AddressControllerTests
    {
        private readonly AddressController _sut;
        private readonly Mock<IAddressService> _Addresservice = new();


        public AddressControllerTests()
        {
            _sut = new AddressController(_Addresservice.Object);
        }


        [Fact]
        public async void GetAll_ShouldReturnStatusCode200_whenDataExists()
        {
            //Arrange

            List<AddressResponse> Address = new();

            Address.Add(new AddressResponse
            {
                AddressId = 1,
                Address = "John",
                Postal = 2700,
                City = "Kage"
            });

            Address.Add(new AddressResponse
            {
                AddressId = 2,
                Address = "John",
                Postal = 2700,
                City = "Kage"
            });


            _Addresservice
                .Setup(s => s.GetAllAddress())
                .ReturnsAsync(Address);

            //Act
            var result = await _sut.GetAll();

            //Assert 
            var statusCodeResult = (IStatusCodeActionResult)result;
            Assert.Equal(200, statusCodeResult.StatusCode);



        }

        [Fact]
        public async void GetAll_ShouldReturnStatusCode204_whenNoDataExists()
        {
            List<AddressResponse> Address = new();

            _Addresservice
                .Setup(s => s.GetAllAddress())
                .ReturnsAsync(Address);

            //Act
            var result = await _sut.GetAll();

            //Assert 
            var statusCodeResult = (IStatusCodeActionResult)result;
            Assert.Equal(204, statusCodeResult.StatusCode);



        }

        [Fact]
        public async void GetAll_ShouldReturnStatusCode500_whenNullisReturnedFromService()
        {
            List<AddressResponse> Address = new();
            //
            _Addresservice
                .Setup(s => s.GetAllAddress())
                .ReturnsAsync(() => null);

            //Act
            var result = await _sut.GetAll();

            //Assert 
            var statusCodeResult = (IStatusCodeActionResult)result;
            Assert.Equal(500, statusCodeResult.StatusCode);



        }

        [Fact]
        public async void GetAll_ShouldReturnStatusCode500_WhenExeptionIsRaised()
        {

            //
            _Addresservice
                .Setup(s => s.GetAllAddress())
                .Returns(() => throw new System.Exception("This is an exception"));

            //Act 
            var result = await _sut.GetAll();

            //Assert 
            var statusCodeResult = (IStatusCodeActionResult)result;
            Assert.Equal(500, statusCodeResult.StatusCode);



        }

        [Fact]
        public async void GetById_ShouldReturnStatusCode200_whenDataExists()
        {
            //Arrange
            int AddressId = 1;
            AddressResponse Address = new()
            {
                AddressId = 1,
                Address = "John",
                Postal = 2700,
                City = "Kage"
            };




            _Addresservice
                .Setup(s => s.GetById(It.IsAny<int>()))
                .ReturnsAsync(Address);

            //Act
            var result = await _sut.GetById(AddressId);

            //Assert 
            var statusCodeResult = (IStatusCodeActionResult)result;
            Assert.Equal(200, statusCodeResult.StatusCode);



        }

        [Fact]
        public async void GetById_ShouldReturnStatusCode404_whenDataExists()
        {
            //Arrange
            int AddressId = 1;





            _Addresservice
                .Setup(s => s.GetById(It.IsAny<int>()))
                .ReturnsAsync(() => null);

            //Act
            var result = await _sut.GetById(AddressId);

            //Assert 
            var statusCodeResult = (IStatusCodeActionResult)result;
            Assert.Equal(404, statusCodeResult.StatusCode);



        }

        [Fact]
        public async void GetById_ShouldReturnStatusCode500_whenDataExists()
        {
            //Arrange

            _Addresservice
                .Setup(s => s.GetById(It.IsAny<int>()))
                .ReturnsAsync(() => throw new System.Exception("This is an exception"));

            //Act
            var result = await _sut.GetById(1);

            //Assert 
            var statusCodeResult = (IStatusCodeActionResult)result;
            Assert.Equal(500, statusCodeResult.StatusCode);



        }

        [Fact]
        public async void Create_ShouldReturnStatusCode200_whenDataExists()
        {
            //Arrange
            int AddressId = 1;
            NewAddress newAddress = new()
            {

                Address = "John",
                Postal = 2700,
                City = "Kage"
            };

            AddressResponse Address = new()
            {
                AddressId = AddressId,
                Address = "John",
                Postal = 2700,
                City = "Kage"
            };






            _Addresservice
                .Setup(s => s.Create(It.IsAny<NewAddress>()))
                .ReturnsAsync(Address);

            //Act
            var result = await _sut.Create(newAddress);

            //Assert 
            var statusCodeResult = (IStatusCodeActionResult)result;
            Assert.Equal(200, statusCodeResult.StatusCode);



        }

        [Fact]
        public async void Create_ShouldReturnStatusCode500_whenDataExists()
        {
            //Arrange

            NewAddress newAddress = new()
            {
                Address = "John",
                Postal = 2700,
                City = "Kage"
            };

            _Addresservice
                .Setup(s => s.Create(It.IsAny<NewAddress>()))
                .ReturnsAsync(() => throw new System.Exception("This is an exception"));

            //Act
            var result = await _sut.Create(newAddress);

            //Assert 
            var statusCodeResult = (IStatusCodeActionResult)result;
            Assert.Equal(500, statusCodeResult.StatusCode);



        }

        [Fact]
        public async void Update_ShouldReturnStatusCode200_whenDataExists()
        {
            //Arrange
            int AddressId = 1;
            UpdateAddress updateAddress = new()
            {

                Address = "John",
                Postal = 2700,
                City = "Kage"
            };

            AddressResponse Address = new()
            {
                AddressId = AddressId,
                Address = "John",
                Postal = 2700,
                City = "Kage"
            };






            _Addresservice
                .Setup(s => s.Update(It.IsAny<int>(), It.IsAny<UpdateAddress>()))
                .ReturnsAsync(Address);

            //Act
            var result = await _sut.Update(AddressId, updateAddress);

            //Assert 
            var statusCodeResult = (IStatusCodeActionResult)result;
            Assert.Equal(200, statusCodeResult.StatusCode);



        }

        [Fact]
        public async void Update_ShouldReturnStatusCode500_whenDataExists()
        {
            //Arrange
            int AddressId = 1;
            UpdateAddress updateAddress = new()
            {
                Address = "John",
                Postal = 2700,
                City = "Kage"
            };


            _Addresservice
                .Setup(s => s.Update(It.IsAny<int>(), It.IsAny<UpdateAddress>()))
                .ReturnsAsync(() => throw new System.Exception("This is an exception"));

            //Act
            var result = await _sut.Update(AddressId, updateAddress);

            //Assert 
            var statusCodeResult = (IStatusCodeActionResult)result;
            Assert.Equal(500, statusCodeResult.StatusCode);



        }

        [Fact]
        public async void Delete_ShouldReturnStatusCode204_whenDataExists()
        {
            //Arrange
            int AddressId = 1;

            _Addresservice
                .Setup(s => s.Delete(It.IsAny<int>()))
                .ReturnsAsync(true);

            //Act
            var result = await _sut.Delete(AddressId);

            //Assert 
            var statusCodeResult = (IStatusCodeActionResult)result;
            Assert.Equal(204, statusCodeResult.StatusCode);



        }

        [Fact]
        public async void Delete_ShouldReturnStatusCode500_whenDataExists()
        {
            //Arrange
            int AddressId = 1;

            _Addresservice
                .Setup(s => s.Delete(It.IsAny<int>()))
                .ReturnsAsync(() => throw new System.Exception("This is an exception"));

            //Act
            var result = await _sut.Delete(AddressId);

            //Assert 
            var statusCodeResult = (IStatusCodeActionResult)result;
            Assert.Equal(500, statusCodeResult.StatusCode);



        }

    }
}
