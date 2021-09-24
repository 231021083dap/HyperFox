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

namespace WebApiTests10
{
    public class ItemControllerTests
    {
        //Til at kunne teste.
        private readonly ItemController _sut;
        private readonly Mock<IItemService> _itemService = new();

        //Skulle forstille vores autherService (bruges til test).
        public ItemControllerTests() //Contructor
        {
            _sut = new ItemController(_itemService.Object);
        }

        [Fact] // Det den burde return
        public async void GetAll_ShouldReturnStatusCode200_whenDataExist()
        {
            //Arange - Hvordan skal den se ud.
            List<ItemResponse> Items = new();

            Items.Add(new ItemResponse
            {
                ItemId = 1,
                FilmId = 1,
                OrderId = 1,
                Quantity = 1,
                Price = 1
            });

            Items.Add(new ItemResponse
            {
                ItemId = 2,
                FilmId = 2,
                OrderId = 2,
                Quantity = 2,
                Price = 2
            });

            _itemService
                .Setup(s => s.GetAllItems())
                .ReturnsAsync(Items);




            //Act - Udfører test om at få alle Items.
            var result = await _sut.GetAll();

            //Assert - Kigger på resultatet.
            var statusCodeResult = (IStatusCodeActionResult)result;
            Assert.Equal(200, statusCodeResult.StatusCode);


        }

        [Fact]
        public async void GetAll_ShouldReturnStatusCode204_whenNoDataExist()
        {
            //Arange - Hvordan skal den se ud.
            List<ItemResponse> Items = new();

            _itemService
                .Setup(s => s.GetAllItems())
                .ReturnsAsync(Items);


            //Act - Udfører test om at få alle Items.
            var result = await _sut.GetAll();

            //Assert - Kigger på resultatet.
            var statusCodeResult = (IStatusCodeActionResult)result;
            Assert.Equal(204, statusCodeResult.StatusCode);


        }

        [Fact]
        public async void GetAll_ShouldReturnStatusCode500_whenNullIsReturnedFromService()
        {
            //Arange - Hvordan skal den se ud.
            List<ItemResponse> Items = new();

            _itemService
                .Setup(s => s.GetAllItems())
                .Returns(() => null);


            //Act - Udfører test om at få alle Items.
            var result = await _sut.GetAll();

            //Assert - Kigger på resultatet.
            var statusCodeResult = (IStatusCodeActionResult)result;
            Assert.Equal(500, statusCodeResult.StatusCode);


        }

        [Fact]
        public async void GetAll_ShouldReturnStatusCode500_whenExeptionIsRaised()
        {
            //Arange - Hvordan skal den se ud.
            List<ItemResponse> Items = new();

            _itemService
                .Setup(s => s.GetAllItems())
                .ReturnsAsync(() => throw new Exception("This is an exeption")); //Får vores kode til at fejle med vilje


            //Act - Udfører test om at få alle Items.
            var result = await _sut.GetAll();

            //Assert - Kigger på resultatet.
            var statusCodeResult = (IStatusCodeActionResult)result;
            Assert.Equal(500, statusCodeResult.StatusCode);


        }


        [Fact]
        public async void GetById_ShouldReturnStatusCode200_WhenDataExists()
        {
            // Arrange
            int ItemId = 1;
            ItemResponse Item = new ItemResponse
            {
                ItemId = ItemId,
                FilmId = 1,
                OrderId = 1,
                Quantity = 1,
                Price = 1,
                Film = new ItemFilmResponse
                {
                    FilmId = 1,
                    FilmName = "TestFilmen",
                    Description = "Dette er en test film",
                    ReleaseDate = "19-12-2000",
                    RuntimeInMin = 26,
                    Price = 1225,
                    Image = "stringImage",
                    Stock = 124
                },


            };

            _itemService
                .Setup(s => s.GetById(It.IsAny<int>()))
                .ReturnsAsync(Item);

            // Act
            var result = await _sut.GetById(ItemId);

            // Assert
            var statusCodeResult = (IStatusCodeActionResult)result;
            Assert.Equal(200, statusCodeResult.StatusCode);
        }

        [Fact]
        public async void GetById_ShouldReturnStatusCode404_WhenItemDoesNotExist()
        {
            // Arrange
            int ItemId = 1;

            _itemService
                .Setup(s => s.GetById(It.IsAny<int>()))
                .ReturnsAsync(() => null);
            // Act
            var result = await _sut.GetById(ItemId);

            // Assert
            var statusCodeResult = (IStatusCodeActionResult)result;
            Assert.Equal(404, statusCodeResult.StatusCode);
        }

        [Fact]
        public async void GetById_ShouldReturnStatusCode500_WhenExceptionIsRaised()
        {
            // Arrange
            _itemService
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
            NewItem newItem = new NewItem
            {
                FilmId = 1,
                OrderId = 1,
                Quantity = 1,
                Price = 1
            };

            ItemResponse Item = new ItemResponse
            {
                ItemId = 1,
                FilmId = 1,
                OrderId = 1,
                Quantity = 1,
                Price = 1,
                Film = new ItemFilmResponse
                {
                    FilmId = 2,
                    FilmName = "Den lange slange",
                    Description = "Det er en lang slange",
                    ReleaseDate = "11-14-2014",
                    Price = 51,
                    Image = "StockImageOfSnake",
                    Stock = 114
                }
            };

            _itemService
                .Setup(s => s.Create(It.IsAny<NewItem>()))
                .ReturnsAsync(Item);

            // Act
            var result = await _sut.Create(newItem);

            // Assert
            var statusCodeResult = (IStatusCodeActionResult)result;
            Assert.Equal(200, statusCodeResult.StatusCode);
        }

        [Fact]
        public async void Create_ShouldReturnStatusCode500_WhenExceptionIsRaised()
        {
            // Arrange
            NewItem newItem = new NewItem
            {
                FilmId = 1,
                OrderId = 1,
                Quantity = 1,
                Price = 1
            };

            _itemService
                .Setup(s => s.Create(It.IsAny<NewItem>()))
                .ReturnsAsync(() => throw new System.Exception("This is an exception"));

            // Act
            var result = await _sut.Create(newItem);

            // Assert
            var statusCodeResult = (IStatusCodeActionResult)result;
            Assert.Equal(500, statusCodeResult.StatusCode);
        }

        [Fact]
        public async void Update_ShouldReturnStatusCode200_WhenDataIsSaved()
        {
            // Arrange
            int ItemId = 1;
            UpdateItem updateItem = new UpdateItem
            {

                FilmId = 2,
                OrderId = 2,
                Quantity = 1,
                Price = 1
            };

            ItemResponse Item = new ItemResponse
            {
                ItemId = ItemId,
                FilmId = 1,
                OrderId = 1,
                Quantity = 1,
                Price = 1,
                Film = new ItemFilmResponse
                {
                    FilmId = 1,
                    FilmName = "Den gule regnjakke",
                    Description = "en pige med en gul regnjakke i solvejr",
                    ReleaseDate = "21-11-2021",
                    Price = 399,
                    Image = "StockImageOfGirl",
                    Stock = 10
                }

            };

            _itemService
                .Setup(s => s.Update(It.IsAny<int>(), It.IsAny<UpdateItem>()))
                .ReturnsAsync(Item);

            // Act
            var result = await _sut.Update(ItemId, updateItem);

            // Assert
            var statusCodeResult = (IStatusCodeActionResult)result;
            Assert.Equal(200, statusCodeResult.StatusCode);
        }

        [Fact]
        public async void Update_ShouldReturnStatusCode500_WhenExceptionIsRaised()
        {
            // Arrange
            int ItemId = 1;
            UpdateItem updateItem = new UpdateItem
            {
                FilmId = 1,
                OrderId = 1,
                Quantity = 1,
                Price = 1
            };

            _itemService
                .Setup(s => s.Update(It.IsAny<int>(), It.IsAny<UpdateItem>()))
                .ReturnsAsync(() => throw new System.Exception("This is an exception"));

            // Act
            var result = await _sut.Update(ItemId, updateItem);

            // Assert
            var statusCodeResult = (IStatusCodeActionResult)result;
            Assert.Equal(500, statusCodeResult.StatusCode);
        }

        [Fact]
        public async void Delete_ShouldReturnStatusCode204_WhenItemIsDeleted()
        {
            // Arrange
            int ItemId = 1;

            _itemService
                .Setup(s => s.Delete(It.IsAny<int>()))
                .ReturnsAsync(true);

            // Act
            var result = await _sut.Delete(ItemId);

            // Assert
            var statusCodeResult = (IStatusCodeActionResult)result;
            Assert.Equal(204, statusCodeResult.StatusCode);
        }

        [Fact]
        public async void Delete_ShouldReturnStatusCode500_WhenExceptionIsRaised()
        {
            // Arrange
            int ItemId = 1;

            _itemService
                .Setup(s => s.Delete(It.IsAny<int>()))
                .ReturnsAsync(() => throw new System.Exception("This is an exception"));

            // Act
            var result = await _sut.Delete(ItemId);

            // Assert
            var statusCodeResult = (IStatusCodeActionResult)result;
            Assert.Equal(500, statusCodeResult.StatusCode);
        }
    }
}