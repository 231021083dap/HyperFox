using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebApi.Database;
using WebApi.Entities;
using WebApi.Repository;
using Xunit;

namespace WebApiTests
{
    public class AddressRepositoryTests
    {
        public class AddressRepositoryTest
        {
            private readonly AddressRepository _sut;
            private readonly WebApiContext _context;
            private readonly DbContextOptions<WebApiContext> _options;

            public AddressRepositoryTest()
            {
                _options = new DbContextOptionsBuilder<WebApiContext>()
                    .UseInMemoryDatabase(databaseName: "LibraryProject")
                    .Options;
                _context = new WebApiContext(_options);

                _sut = new AddressRepository(_context);

            }

            [Fact]
            public async Task GetAll_ShouldReturnListofAddresss_WhenAddresssExists()
            {
                //Arrange
                await _context.Database.EnsureDeletedAsync();

                _context.Address.Add(new Address
                {
                    AddressId = 1,
                    Add = "John",
                    Postal = 2700,
                    City = "Kage"

                });
                _context.Address.Add(new Address
                {
                    AddressId = 2,
                    Add = "John",
                    Postal = 2700,
                    City = "Kage"
                });
                await _context.SaveChangesAsync();

                var result = await _sut.GetAll();


                //Act

                //Assert
                Assert.NotNull(result);
                Assert.Equal(2, result.Count);
                Assert.IsType<List<Address>>(result);
            }

            [Fact]
            public async void GetAll_ShouldReturnListOfAddressResponses_WhenNoAddresssExist()
            {
                //Arrange'
                await _context.Database.EnsureDeletedAsync();
                //Act
                var result = await _sut.GetAll();



                //Assert
                Assert.NotNull(result);
                Assert.Empty(result);
                Assert.IsType<List<Address>>(result);

            }

            [Fact]
            public async Task GetById_ShouldReturnTheAddress_IfAddressExists()
            {
                //Arrange
                await _context.Database.EnsureDeletedAsync();
                int AddressId = 1;

                _context.Address.Add(new Address
                {
                    AddressId = AddressId,
                    Add = "John",
                    Postal = 2700,
                    City = "Kage"



                });
                await _context.SaveChangesAsync();

                //act 
                var result = await _sut.GetById(AddressId);

                //Assert
                Assert.NotNull(result);
                Assert.IsType<Address>(result);
                Assert.Equal(AddressId, result.AddressId);

            }

            [Fact]
            public async Task GetById_sHOULDrETURNnULL_IFAddressdOESnOTeXISTS()
            {
                //arrange
                await _context.Database.EnsureDeletedAsync();
                int AddressId = 1;
                //act
                var result = await _sut.GetById(AddressId);

                //assert
                Assert.Null(result);
            }


            [Fact]
            public async Task Create_ShouldAddIdToAddress_WhenSavingToDatabase()
            {
                //Arrange
                await _context.Database.EnsureDeletedAsync();
                int ExpectedID = 1;
                Address Address = new()
                {
                    Add = "John",
                    Postal = 2700,
                    City = "Kage"
                };

                //Act
                var result = await _sut.Create(Address);

                //Assert
                Assert.NotNull(result);
                Assert.IsType<Address>(result);
                Assert.Equal(ExpectedID, result.AddressId);
            }

            [Fact]
            public async Task Create_ShouldFailToAddAddress_WhenAddingAddressWithExistingId()
            {
                // Arrange
                await _context.Database.EnsureDeletedAsync();

                Address Address = new()
                {
                    AddressId = 1,
                    Add = "John",
                    Postal = 2700,
                    City = "Kage"
                };

                _context.Address.Add(Address);
                await _context.SaveChangesAsync();
                //Act
                async Task action() => await _sut.Create(Address);

                //Assert
                var ex = await Assert.ThrowsAsync<ArgumentException>(action);
                Assert.Contains("An item with the same key has already been added", ex.Message);

            }

            [Fact]
            public async Task Update_ShouldChangeValuesOnAddress_WhenAddressExists()
            {
                //Arrange
                await _context.Database.EnsureDeletedAsync();
                int AddressId = 1;
                Address Address = new()
                {
                    AddressId = AddressId,
                    Add = "John",
                    Postal = 2700,
                    City = "Kage"

                };
                _context.Address.Add(Address);
                await _context.SaveChangesAsync();

                Address updateAddress = new()
                {
                    AddressId = AddressId,
                    Add = "John",
                    Postal = 2700,
                    City = "Kage"
                };

                //Act
                var result = await _sut.Update(AddressId, updateAddress);

                //Assert
                Assert.NotNull(result);
                Assert.IsType<Address>(result);
                Assert.Equal(AddressId, result.AddressId);
                Assert.Equal(updateAddress.Add, result.Add);
                Assert.Equal(updateAddress.Postal, result.Postal);
                Assert.Equal(updateAddress.City, result.City);

            }

            [Fact]
            public async Task Update_ShouldRetrunNull_WhenAddressDoesNotExists()
            {
                //Arrange

                await _context.Database.EnsureDeletedAsync();
                int AddressId = 1;
                Address updateAddress = new()
                {
                    AddressId = AddressId,
                    Add = "asdasd",
                    Postal = 2700,
                    City = "asdasd"
                };

                //Act
                var result = await _sut.Update(AddressId, updateAddress);

                //Assert
                Assert.Null(result);

            }


            [Fact]
            public async void Delete_ShouldReturnDeleteAddress_WhenAddressIsDeæeted()
            {
                // Arrange

                await _context.Database.EnsureDeletedAsync();
                int Addressid = 1;
                Address Address = new()
                {
                    AddressId = Addressid,
                    Add = "Joasdsdhn",
                    Postal = 2700,
                    City = "assadasd"
                };

                _context.Address.Add(Address);
                await _context.SaveChangesAsync();

                var result = await _sut.Delete(Addressid);
                var Addresss = await _sut.GetAll();

                //Assert

                Assert.NotNull(result);
                Assert.IsType<Address>(result);
                Assert.Equal(Addressid, result.AddressId);

                Assert.Empty(Addresss);

            }



            [Fact]
            public async Task Delete_ShouldReturnNULL_WhenAddressDOesNotExist()
            {
                //Arrange
                await _context.Database.EnsureDeletedAsync();
                int Addressid = 1;


                //Act
                var result = await _sut.Delete(Addressid);
                // Assert a
                Assert.Null(result);
            }



        }
    }
}
