using Microsoft.AspNetCore.Mvc.Infrastructure;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApi.Controllers;
using WebApi.Database.Entities;
using WebApi.DTOs.Requests;
using WebApi.DTOs.Responses;
using WebApi.Entities;
using WebApi.Repositories;
using WebApi.Services;
using Xunit;

namespace WebApiTests10
{
    public class ItemServiceTests
    {
        // Variabler
        private readonly ItemService _sut;
        private readonly Mock<IItemRepository> _itemRepositories = new();

        //Contructor 
        public ItemServiceTests()
        {
            _sut = new ItemService(_itemRepositories.Object);
        }

        [Fact]
        public async void GetAll_ShouldReturnListOfItemResponses_WhenAchtorsExists()
        {
            // Arange
            List<Film> films = new();

            films.Add(new Film
            {
                FilmId = 1,
                FilmName = "TestRepo",
                Description = "The repo team is out again",
                ReleaseDate = "11-14-2001",
                Price = 214,
                Image = "Stonk",
                Stock = 123
            });

            List<User> users = new();
            users.Add(new User
            {
                UserId = 1,
                UserName = "Jens",
                Email = "jensmail@gmail.com",
                Password = "123",
                Admin = "Jeg er en banan",
                Addresses = new Address()
            });

            List<Order> orders = new();

            orders.Add(new Order
            {
                OrderId = 1,
                DateTime = DateTime.Parse("2001-08-21 04:45:21"),
                UserId = 1,
                Items = new List<Item>(),
                User = new User()
            });

            List<Item> items = new();

            items.Add(new Item
            {
                ItemId = 1,
                FilmId = 1,
                OrderId = 1,
                Quantity = 1,
                Price = 1
            });

            items.Add(new Item
            {
                ItemId = 2,
                FilmId = 1,
                OrderId = 1,
                Quantity = 2,
                Price = 2
            });


            //Får alle Items og så returnerer dem.
            _itemRepositories
                .Setup(a => a.GetAll())
                .ReturnsAsync(items);

            // act
            var result = await _sut.GetAllItems();



            // Assert
            Assert.NotNull(result);
            Assert.Equal(2, result.Count);
            Assert.IsType<List<ItemResponse>>(result);


        }
        [Fact]
        public async void GetAll_ShouldReturnEmptyListOfItemReponses_WhenNoItemExists()
        {
            //Arrange
            List<Item> Items = new List<Item>();

            _itemRepositories.Setup(a => a.GetAll()).ReturnsAsync(Items);

            //Act
            var result = await _sut.GetAllItems();

            //Assert
            Assert.NotNull(result);
            Assert.Empty(result);
            Assert.IsType<List<ItemResponse>>(result);
        }

        [Fact]
        public async void GetById_ShouldReturnAnItemResponse_WhenItemExists()
        {
            //Arrange
            int itemId = 1;

            Item item = new Item
            {
                ItemId = itemId,
                FilmId = 1,
                OrderId = 1,
                Quantity = 1,
                Price = 1,
                Film = new Film()
            };

            _itemRepositories.Setup(a => a.GetById(It.IsAny<int>())).ReturnsAsync(item);

            //Act 
            var result = await _sut.GetById(itemId);

            //Assert
            Assert.NotNull(result);
            Assert.IsType<ItemResponse>(result);
            Assert.Equal(item.ItemId, result.ItemId);
            Assert.Equal(item.FilmId, result.FilmId);
            Assert.Equal(item.OrderId, result.OrderId);
            Assert.Equal(item.Quantity, result.Quantity);
            Assert.Equal(item.Price, result.Price);
        }

        [Fact]
        public async void GetById_ShouldReturnNull_WhenItemDoesNotExists()
        {
            //Arrange
            int ItemId = 1;

            _itemRepositories.Setup(a => a.GetById(It.IsAny<int>())).ReturnsAsync(() => null);

            //Act
            var result = await _sut.GetById(ItemId);

            //Assert
            Assert.Null(result);
        }

        [Fact]
        public async void Create_ShouldReturnItemResponse_WhenCreateIsSuccess()
        {
            //Arrange
            NewItem newItem = new NewItem
            {

                FilmId = 1,
                OrderId = 1,
                Quantity = 1,
                Price = 1
            };

            int itemId = 1;
            Item item = new Item
            {
                ItemId = itemId,
                FilmId = 1,
                OrderId = 1,
                Quantity = 1,
                Price = 1,

            };

            _itemRepositories
                .Setup(a => a.Create(It.IsAny<Item>()))
                .ReturnsAsync(item);


            //Act
            var result = await _sut.Create(newItem);

            //Assert
            Assert.NotNull(result);
            Assert.IsType<ItemResponse>(result);
            Assert.Equal(item.ItemId, result.ItemId);
            Assert.Equal(item.FilmId, result.FilmId);
            Assert.Equal(item.OrderId, result.OrderId);
            Assert.Equal(item.Quantity, result.Quantity);
            Assert.Equal(item.Price, result.Price);
        }

        [Fact]
        public async void Update_shouldReturnUpdateItemResponse_WhenUpdateIsSucces()
        {
            //Arrange
            UpdateItem updateItem = new UpdateItem
            {
                FilmId = 1,
                OrderId = 1,
                Quantity = 1,
                Price = 1
            };

            int itemId = 1;

            Item item = new Item
            {
                ItemId = itemId,
                FilmId = 1,
                OrderId = 1,
                Quantity = 1,
                Price = 1
            };

            _itemRepositories.Setup(a => a.Update(It.IsAny<int>(), It.IsAny<Item>())).ReturnsAsync(item);

            //Act
            var result = await _sut.Update(itemId, updateItem);

            //Assert
            Assert.NotNull(result);
            Assert.IsType<ItemResponse>(result);
            Assert.Equal(item.ItemId, result.ItemId);
            Assert.Equal(item.FilmId, result.FilmId);
            Assert.Equal(item.OrderId, result.OrderId);
            Assert.Equal(item.Quantity, result.Quantity);
            Assert.Equal(item.Price, result.Price);

        }

        [Fact]
        public async void Update_ShouldreturnNull_WhenItemDoesNotExists()
        {
            //Arrange
            UpdateItem updateItem = new UpdateItem
            {
                FilmId = 2,
                OrderId = 2,
                Quantity = 2,
                Price = 2
            };

            int itemId = 1;

            _itemRepositories.Setup(a => a.Update(It.IsAny<int>(), It.IsAny<Item>())).ReturnsAsync(() => null);

            //Act
            var result = await _sut.Update(itemId, updateItem);

            //Assert
            Assert.Null(result);
        }

        [Fact]
        public async void Delete_shouldReturnTrue_WhenDeleteIsSuccess()
        {
            //Arrange
            int itemId = 1;

            Item item = new Item
            {
                ItemId = itemId,
                FilmId = 1,
                OrderId = 1,
                Quantity = 1,
                Price = 1
            };

            _itemRepositories.Setup(a => a.Delete(It.IsAny<int>())).ReturnsAsync(item);

            //Act
            var result = await _sut.Delete(itemId);

            //Assert
            Assert.True(result);
        }
    }
}
