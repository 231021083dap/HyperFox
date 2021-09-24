using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApi.DTOs.Requests;
using WebApi.DTOs.Responses;
using WebApi.Entities;
using WebApi.Repository;
using WebApi.Services;
using Xunit;

namespace WebApiTests40
{
    public class AddressServiceTests
    {

        private readonly AddressService _sut;
        private readonly Mock<IAddressRepository> _AddressRepository = new();

        public AddressServiceTests()
        {
            _sut = new AddressService(_AddressRepository.Object);

        }

        [Fact]
        public async void GetAll_ShouldReturnListOfAddressResponses_WhenAddresssExist()
        {
            //Arrange'
            List<Address> Addresss = new();
            Addresss.Add(new Address
            {
                AddressId = 1,
                StreetName = "John",
                Postal = 2700,
                City = "Kage",
                UserId = 1



            });

            Addresss.Add(new Address
            {
                AddressId = 2,
                StreetName = "John",
                Postal = 2700,
                City = "Kage",
                UserId = 2



            });

            _AddressRepository
                .Setup(a => a.GetAll())
                .ReturnsAsync(Addresss);
            //Act
            var result = await _sut.GetAllAddress();

            //Assert
            Assert.NotNull(result);
            Assert.Equal(2, result.Count);
            Assert.IsType<List<AddressResponse>>(result);

        }

        [Fact]
        public async void GetById_ShouldReturnListOfAddressResponses_WhenAuhtorsExist()
        {
            NewAddress newAddress = new()
            {

                StreetName = "John",
                Postal = 2700,
                City = "Kage",
                UserId = 1

            };

            int AddressId = 1;

            Address Address = new()
            {
                AddressId = AddressId,
                StreetName = "John",
                Postal = 2700,
                City = "Kage",
                User = new User
                {
                    UserId = 1,
                    UserName = "Karsten",
                    Email = "Karsen@gmail.com",
                    Admin = "User"
                }

            };

            _AddressRepository
                .Setup(a => a.Create(It.IsAny<Address>()))
                .ReturnsAsync(Address);

            //act

            var result = await _sut.Create(newAddress);

            //assert
            Assert.NotNull(result);
            Assert.IsType<AddressResponse>(result);
            Assert.Equal(AddressId, result.AddressId);
            Assert.Equal(newAddress.StreetName, result.StreetName);
            Assert.Equal(newAddress.Postal, result.Postal);
            Assert.Equal(newAddress.City, result.City);

        }

        [Fact]
        public async void GetAll_ShouldReturnEmptyListOfAddressResponses_WhenNoAddresssExists()
        {
            List<Address> Addresss = new();

            _AddressRepository
                .Setup(a => a.GetAll())
                .ReturnsAsync(Addresss);

            //Act
            var result = await _sut.GetAllAddress();

            //Assert 
            Assert.NotNull(result);
            Assert.Empty(result);
            Assert.IsType<List<AddressResponse>>(result);

        }

        [Fact]
        public async void GetById_ShouldReturnnull_WhenNoAddresssExists()
        {
            //arrange
            int Addressid = 1;

            _AddressRepository
                .Setup(a => a.GetById(It.IsAny<int>()))
                .ReturnsAsync(() => null);

            //Act
            var result = await _sut.GetById(Addressid);

            //Assert 
            Assert.Null(result);

        }

        [Fact]
        public async void Create_shouldreturnAddressresponse_whencreateissuccess()
        {
            //arrange
            NewAddress newAddress = new()
            {

                StreetName = "John",
                Postal = 2700,
                City = "Kage",
                UserId = 1

            };

            int AddressId = 1;

            Address Address = new()
            {
                AddressId = AddressId,
                StreetName = "John",
                Postal = 2700,
                City = "Kage",
                UserId = 1

            };

            _AddressRepository
                .Setup(a => a.Create(It.IsAny<Address>()))
                .ReturnsAsync(Address);

            //act

            var result = await _sut.Create(newAddress);

            //assert
            Assert.NotNull(result);
            Assert.IsType<AddressResponse>(result);
            Assert.Equal(AddressId, result.AddressId);
            Assert.Equal(newAddress.StreetName, result.StreetName);
            Assert.Equal(newAddress.Postal, result.Postal);
            Assert.Equal(newAddress.City, result.City);

        }

        [Fact]
        public async void Update_shouldreturnupdateeAddressresponse_whenupdateissuccess()
        {
            UpdateAddress update = new()
            {

                StreetName = "John",
                Postal = 2700,
                City = "Kage",
                UserId = 1

            };

            int AddressId = 1;

            Address Address = new()
            {
                AddressId = AddressId,
                StreetName = "John",
                Postal = 2700,
                City = "Kage",
                UserId = 1

            };

            _AddressRepository
                .Setup(a => a.Update(It.IsAny<int>(), It.IsAny<Address>()))
                .ReturnsAsync(Address);

            //act

            var result = await _sut.Update(AddressId, update);

            //assert
            Assert.NotNull(result);
            Assert.IsType<AddressResponse>(result);
            Assert.Equal(AddressId, result.AddressId);
            Assert.Equal(update.StreetName, result.StreetName);
            Assert.Equal(update.Postal, result.Postal);
            Assert.Equal(update.City, result.City);

        }

        [Fact]
        public async void Update_shouldreturnnull_whenAddressnoexist()
        {
            UpdateAddress update = new()
            {
                StreetName = "John",
                Postal = 2700,
                City = "Kage",
                UserId = 1

            };

            int AddressId = 1;



            _AddressRepository
                .Setup(a => a.Update(It.IsAny<int>(), It.IsAny<Address>()))
                .ReturnsAsync(() => null);

            //act

            var result = await _sut.Update(AddressId, update);

            //assert
            Assert.Null(result);


        }

        [Fact]
        public async void Delete_ShouldReturnTrue_WhenDeleteIsSuccess()
        {
            //arrange
            int Addressid = 1;
            Address Address = new()
            {
                StreetName = "John",
                Postal = 2700,
                City = "Kage",
                UserId = 1

            };



            _AddressRepository
                .Setup(a => a.Delete(It.IsAny<int>()))
                .ReturnsAsync(Address);

            //act

            var result = await _sut.Delete(Addressid);

            //assert
            Assert.True(result);


        }

    }
}
