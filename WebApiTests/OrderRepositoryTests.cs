using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApi.Database;
using WebApi.Entities;
using WebApi.Repository;
using Xunit;

namespace WebApiTests2
{
    public class OrderRepositoryTest
    {
        private readonly OrderRepository _sut;
        private readonly WebApiContext _context;
        private readonly DbContextOptions<WebApiContext> _options;

        public OrderRepositoryTest()
        {
            _options = new DbContextOptionsBuilder<WebApiContext>()
                .UseInMemoryDatabase(databaseName: "LibraryProject2")
                .Options;
            _context = new WebApiContext(_options);

            _sut = new OrderRepository(_context);

        }

        [Fact]
        public async Task GetAll_ShouldReturnListofOrders_WhenOrdersExists()
        {
            //Arrange
            await _context.Database.EnsureDeletedAsync();

            _context.User.Add(new User
            {
                UserId = 1,
                UserName = "Master",
                Email = "Dragon",
                Password = "12345",
                Admin = "Elder",
                Addresses = new Address()


            });
            await _context.SaveChangesAsync();

            _context.User.Add(new User
            {
                UserId = 2,
                UserName = "Master",
                Email = "Dragon",
                Password = "12345",
                Admin = "Elder",
                Addresses = new Address()


            });
            await _context.SaveChangesAsync();


            _context.Order.Add(new Order
            {
                OrderId = 1,
                DateTime = DateTime.Parse("2001-08-21 04:45:41"),
                UserId = 1,
                Items = new List<Item>(),
                User = new User()

            });
            _context.Order.Add(new Order
            {
                OrderId = 2,
                DateTime = DateTime.Parse("2001-08-21 04:45:41"),
                UserId = 1,
                Items = new List<Item>(),
                User = new User()
            });
            await _context.SaveChangesAsync();

            var result = await _sut.GetAll();


            //Act

            //Assert
            Assert.NotNull(result);
            Assert.Equal(2, result.Count);
            Assert.IsType<List<Order>>(result);
        }

        [Fact]
        public async void GetAll_ShouldReturnListOfOrderResponses_WhenNoOrdersExist()
        {
            //Arrange'
            await _context.Database.EnsureDeletedAsync();
            //Act
            var result = await _sut.GetAll();

            //Assert
            Assert.NotNull(result);
            Assert.Empty(result);
            Assert.IsType<List<Order>>(result);

        }

        [Fact]
        public async Task GetById_ShouldReturnTheAuthor_IfAuthorExists()
        {
            //Arrange
            await _context.Database.EnsureDeletedAsync();
            int OrderId = 1;

            _context.User.Add(new User
            {
                UserId = 1,
                UserName = "Master",
                Email = "Dragon",
                Password = "12345",
                Admin = "Elder",
                Addresses = new Address()


            });
            await _context.SaveChangesAsync();

            _context.Order.Add(new Order
            {
                OrderId = 1,
                DateTime = DateTime.Parse("2001-08-21 04:45:41"),
                UserId = 1,
                Items = new List<Item>(),
                User = new User()

            });
            await _context.SaveChangesAsync();

            //act 
            var result = await _sut.GetById(OrderId);

            //Assert
            Assert.NotNull(result);
            Assert.IsType<Order>(result);
            Assert.Equal(OrderId, result.OrderId);

        }

        [Fact]
        public async Task GetById_sHOULDrETURNnULL_IFaUTHORdOESnOTeXISTS()
        {
            //arrange
            await _context.Database.EnsureDeletedAsync();
            int OrderID = 1;
            //act
            var result = await _sut.GetById(OrderID);

            //assert
            Assert.Null(result);
        }

        [Fact]
        public async Task Create_ShouldAddIdToOrder_WhenSavingToDatabase()
        {
            //Arrange
            await _context.Database.EnsureDeletedAsync();
            int ExpectedID = 1;
            Order Order = new Order
            {
                DateTime = DateTime.Parse("2001-08-21 04:45:41"),
                UserId = 1,
                Items = new List<Item>(),
                User = new User()
            };

            //Act
            var result = await _sut.Create(Order);

            //Assert
            Assert.NotNull(result);
            Assert.IsType<Order>(result);
            Assert.Equal(ExpectedID, result.OrderId);
        }

        [Fact]
        public async Task Create_ShouldFailToAddAuthor_WhenAddingAuthorWithExistingId()
        {
            // Arrange
            await _context.Database.EnsureDeletedAsync();

            Order Order = new Order
            {
                OrderId = 1,
                DateTime = DateTime.Parse("2001-08-21 04:45:41"),
                UserId = 1,
                Items = new List<Item>(),
                User = new User()

            };

            _context.Order.Add(Order);
            await _context.SaveChangesAsync();
            //Act
            Func<Task> action = async () => await _sut.Create(Order);

            //Assert
            var ex = await Assert.ThrowsAsync<ArgumentException>(action);
            Assert.Contains("An item with the same key has already been added", ex.Message);

        }

        [Fact]
        public async Task Update_ShouldChangeValuesOnAuthor_WhenAuthorExists()
        {
            //Arrange
            await _context.Database.EnsureDeletedAsync();
            int OrderId = 1;
            Order Order = new Order
            {
                OrderId = 1,
                DateTime = DateTime.Parse("2001-08-21 04:45:42"),
                UserId = 1,
                Items = new List<Item>(),
                User = new User()

            };
            _context.Order.Add(Order);
            await _context.SaveChangesAsync();

            Order updateOrder = new Order
            {
                OrderId = 1,
                DateTime = DateTime.Parse("2001-08-21 04:45:42"),
                UserId = 1,
                Items = new List<Item>(),
                User = new User()
            };

            //Act
            var result = await _sut.Update(OrderId, updateOrder);

            //Assert
            Assert.NotNull(result);
            Assert.IsType<Order>(result);
            Assert.Equal(OrderId, result.OrderId);
            Assert.Equal(updateOrder.DateTime, result.DateTime);
            Assert.Equal(updateOrder.UserId, result.UserId);


        }

        [Fact]
        public async Task Update_ShouldRetrunNull_WhenAuthorDoesNotExists()
        {
            //Arrange

            await _context.Database.EnsureDeletedAsync();
            int Orderid = 1;
            Order updateOrder = new Order
            {
                OrderId = 1,
                DateTime = DateTime.Parse("2001-08-21 04:45:41"),
                UserId = 1,
                Items = new List<Item>(),
                User = new User()

            };

            //Act
            var result = await _sut.Update(Orderid, updateOrder);

            //Assert
            Assert.Null(result);

        }


        [Fact]
        public async void Delete_ShouldReturnDeleteAuthor_WhenAuthorIsDeæeted()
        {
            // Arrange

            await _context.Database.EnsureDeletedAsync();
            int Orderid = 1;
            Order Order = new Order
            {
                OrderId = 1,
                DateTime = DateTime.Parse("2001-08-21 04:45:41"),
                UserId = 1,
                Items = new List<Item>(),
                User = new User()
            };

            _context.Order.Add(Order);
            await _context.SaveChangesAsync();

            var result = await _sut.Delete(Orderid);
            var Orders = await _sut.GetAll();

            //Assert

            Assert.NotNull(result);
            Assert.IsType<Order>(result);
            Assert.Equal(Orderid, result.OrderId);

            Assert.Empty(Orders);

        }



        [Fact]
        public async Task Delete_ShouldReturnNULL_WhenAuthorDOesNotExist()
        {
            //Arrange
            await _context.Database.EnsureDeletedAsync();
            int Orderid = 1;


            //Act
            var result = await _sut.Delete(Orderid);
            // Assert a
            Assert.Null(result);
        }



    }
}
