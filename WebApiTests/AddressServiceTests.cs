﻿using Moq;
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

namespace WebApiTests
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
                Add = "John",
                Postal = 2700,
                City = "Kage"



            });

            Addresss.Add(new Address
            {
                AddressId = 2,
                Add = "John",
                Postal = 2700,
                City = "Kage"



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
        public async void GetAll_ShouldReturnListOfAddressResponses_WhenAuhtorsExist()
        {
            NewAddress newAddress = new()
            {
              
                Address = "John",
                Postal = 2700,
                City = "Kage"

            };

            int AddressId = 1;

            Address Address = new()
            {
                AddressId = AddressId,
                Add = "John",
                Postal = 2700,
                City = "Kage"

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
            Assert.Equal(newAddress.Address, result.Address);
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
        public async void GetAll_ShouldReturnnull_WhenNoAddresssExists()
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

                Address = "John",
                Postal = 2700,
                City = "Kage"

            };

            int AddressId = 1;

            Address Address = new()
            {
                AddressId = AddressId,
                Add = "John",
                Postal = 2700,
                City = "Kage"

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
            Assert.Equal(newAddress.Address, result.Address);
            Assert.Equal(newAddress.Postal, result.Postal);
            Assert.Equal(newAddress.City, result.City);

        }

        [Fact]
        public async void Update_shouldreturnupdateeAddressresponse_whenupdateissuccess()
        {
            UpdateAddress update = new()
            {
                
                Address = "John",
                Postal = 2700,
                City = "Kage"

            };

            int AddressId = 1;

            Address Address = new()
            {
                AddressId = AddressId,
                Add = "John",
                Postal = 2700,
                City = "Kage"

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
            Assert.Equal(update.Address, result.Address);
            Assert.Equal(update.Postal, result.Postal);
            Assert.Equal(update.City, result.City);

        }

        [Fact]
        public async void Update_shouldreturnnull_whenAddressnoexist()
        {
            UpdateAddress update = new()
            {
                Address = "John",
                Postal = 2700,
                City = "Kage"

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
                Add = "John",
                Postal = 2700,
                City = "Kage"

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
