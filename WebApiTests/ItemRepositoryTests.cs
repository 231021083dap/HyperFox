using System;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApi.Database.Entities;
using WebApi.Entities;
using WebApi.Repositories;
using Xunit;
using WebApi.Database;

namespace WebApiTests
{
    public class ItemRepositoryTests
    {
        private readonly ItemRepository _sut;
        private readonly WebApiContext _context;
        private readonly DbContextOptions<WebApiContext> _options;

        public ItemRepositoryTests()
        {
            _options = new DbContextOptionsBuilder<WebApiContext>()
                .UseInMemoryDatabase(databaseName: "WebApi2") //Simulation af database
                .Options;

            _context = new WebApiContext(_options);

            _sut = new ItemRepository(_context);
        }


        [Fact]
        public async Task GetAll_ShouldReturnListOfItems_WhenItemsExists()
        {
            //Arange
            await _context.Database.EnsureDeletedAsync(); //Sikrer at databasen er slettet.

            _context.Film.Add(new Film
            {
                FilmId = 1,
                FilmName = "TestRepo",
                Description = "The repo team is out again",
                ReleaseDate = "11-14-2001",
                Price = 214,
                Image = "Stonk",
                Stock = 123
            });
            await _context.SaveChangesAsync();

            _context.User.Add(new User
            {
                UserId = 1,
                UserName = "Jens",
                Email = "jensmail@gmail.com",
                Password = "123",
                Admin = WebApi.Auth.Role.User,
                Addresses = new Address()
            });

            await _context.SaveChangesAsync();

            _context.Order.Add(new Order
            {
                OrderId = 1,
                DateTime = DateTime.Parse("2001-08-21 04:45:21"),
                UserId = 1,
                Items = new List<Item>(),
                User = new User()
            });

            await _context.SaveChangesAsync();

            _context.Item.Add(new Item
            {
                ItemId = 1,
                FilmId = 1,
                OrderId = 1,
                Quantity = 1,
                Price = 1
            });

            _context.Item.Add(new Item
            {
                ItemId = 2,
                FilmId = 1,
                OrderId = 1,
                Quantity = 2,
                Price = 2

            });
            await _context.SaveChangesAsync();

            //Act
            var result = await _sut.GetAll();


            //Assert
            Assert.NotNull(result);
            Assert.Equal(2, result.Count);
            Assert.IsType<List<Item>>(result);
        }


        [Fact]
        public async Task GetAll_ShouldReturnEmptyListOfItems_WhenNoItemsExists()
        {
            //Arange
            await _context.Database.EnsureDeletedAsync(); //Sikrer at databasen er slettet.

            //Act
            var result = await _sut.GetAll();


            //Assert
            Assert.NotNull(result);
            Assert.Empty(result);
            Assert.IsType<List<Item>>(result);
        }

        [Fact]
        public async Task GetById_ShouldReturnTheItem_IfItemExists()
        {
            //Arrange
            await _context.Database.EnsureDeletedAsync();

            _context.Film.Add(new Film
            {
                FilmId = 1,
                FilmName = "TestRepo",
                Description = "The repo team is out again",
                ReleaseDate = "11-14-2001",
                Price = 214,
                Image = "Stonk",
                Stock = 123
            });
            await _context.SaveChangesAsync();

            _context.User.Add(new User
            {
                UserId = 1,
                UserName = "Jens",
                Email = "jensmail@gmail.com",
                Password = "123",
                Admin = WebApi.Auth.Role.User,
                Addresses = new Address()
            });

            await _context.SaveChangesAsync();

            _context.Order.Add(new Order
            {
                OrderId = 1,
                DateTime = DateTime.Parse("2001-08-21 04:45:21"),
                UserId = 1,
                Items = new List<Item>(),
                User = new User()
            });
            int itemId = 1;
            _context.Item.Add(new Item
            {
                ItemId = itemId,
                FilmId = 1,
                OrderId = 1,
                Quantity = 1,
                Price = 1
            });
            await _context.SaveChangesAsync();

            //Act
            var result = await _sut.GetById(itemId);

            //Assert
            Assert.NotNull(result);
            Assert.IsType<Item>(result);
            Assert.Equal(itemId, result.ItemId);
        }

        [Fact]
        public async Task GetById_shouldReturnNull_IfItemDoesNotExists()
        {
            //Arange
            await _context.Database.EnsureDeletedAsync();
            int itemId = 1;

            //Act
            var result = await _sut.GetById(itemId);

            //Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task Create_ShouldAddIdToItem_WhenSavingToDatabase()
        {
            //Arange
            await _context.Database.EnsureDeletedAsync();
            int expectedId = 1;
            Item item = new Item
            {
                ItemId = 1,
                FilmId = 1,
                OrderId = 1,
                Quantity = 1,
                Price = 1
            };

            //Act
            var result = await _sut.Create(item);


            //Assert
            Assert.NotNull(result);
            Assert.IsType<Item>(result);
            Assert.Equal(expectedId, result.ItemId);
        }

        [Fact]
        public async Task Create_ShouldFailToaddItem_WhenAddingItemWithExistingId()
        {
            //Arange
            await _context.Database.EnsureDeletedAsync();

            Item item = new Item
            {
                ItemId = 1,
                FilmId = 1,
                OrderId = 1,
                Quantity = 1,
                Price = 1
            };
            _context.Item.Add(item);
            await _context.SaveChangesAsync();

            //Act
            Func<Task> action = async () => await _sut.Create(item);

            //Assert
            var ex = await Assert.ThrowsAsync<ArgumentException>(action);
            Assert.Contains("An item with the same key has already been added", ex.Message);
        }

        [Fact]
        public async Task Update_ShouldChangeValueOnItem_WhenItemExists()
        {

            //Arange
            await _context.Database.EnsureDeletedAsync();
            int itemId = 1;
            Item item = new Item
            {
                ItemId = 1,
                FilmId = 1,
                OrderId = 1,
                Quantity = 1,
                Price = 1
            };
            _context.Item.Add(item);
            await _context.SaveChangesAsync();

            Item updateItem = new Item
            {
                ItemId = itemId,
                FilmId = 3,
                OrderId = 3,
                Quantity = 3,
                Price = 3
            };

            //Act
            var result = await _sut.Update(itemId, updateItem);


            //Assert
            Assert.NotNull(result);
            Assert.IsType<Item>(result);
            Assert.Equal(itemId, result.ItemId);
            Assert.Equal(updateItem.ItemId, result.ItemId);
            Assert.Equal(updateItem.FilmId, result.FilmId);
            Assert.Equal(updateItem.OrderId, result.OrderId);
            Assert.Equal(updateItem.Quantity, result.Quantity);
            Assert.Equal(updateItem.Price, result.Price);
        }

        [Fact]
        public async Task Update_ShouldReturnNull_WhenItemDoesNotExists()
        {
            //Arrange
            await _context.Database.EnsureDeletedAsync();
            int itemId = 1;
            Item updateItem = new Item
            {
                ItemId = itemId,
                FilmId = 1,
                OrderId = 1,
                Quantity = 1,
                Price = 1
            };


            //Act
            var result = await _sut.Update(itemId, updateItem);

            //Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task Delete_ShouldreturnDeletedItem_WhenItemIsDeleted()
        {
            //Arrange
            await _context.Database.EnsureDeletedAsync();
            int itemId = 1;
            Item item = new Item
            {
                ItemId = itemId,
                FilmId = 1,
                OrderId = 1,
                Quantity = 1,
                Price = 1
            };
            _context.Item.Add(item);
            await _context.SaveChangesAsync();

            var result = await _sut.Delete(itemId);
            var items = await _sut.GetAll();

            //Assert
            Assert.NotNull(result);
            Assert.IsType<Item>(result);
            Assert.Equal(itemId, result.ItemId);

            Assert.Empty(items);
        }

        [Fact]
        public async Task Delete_shouldReturnNull_WhenItemsDoesNotExists()
        {
            //Arrange
            await _context.Database.EnsureDeletedAsync();
            int itemId = 1;

            //Act
            var result = await _sut.Delete(itemId);

            //Assert
            Assert.Null(result);
        }

    }
}
