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

namespace WebApiTests30
{
    public class OrderServiceTests
    {
        private readonly OrderService _sut;
        private readonly Mock<IOrderRepository> _OrderRepository = new();

        public OrderServiceTests()
        {
            _sut = new OrderService(_OrderRepository.Object);

        }

        [Fact]
        public async void GetAll_ShouldReturnListOfOrderResponses_WhenOrdersExist()
        {

            List<User> Users = new List<User>();
            Users.Add(new User
            {
                UserId = 1,
                UserName = "Master",
                Email = "Dragon",
                Password = "12345",
                Admin = "Elder",
                Addresses = new Address()
            });

            Users.Add(new User
            {
                UserId = 2,
                UserName = "Master",
                Email = "Dragon",
                Password = "12345",
                Admin = "Elder",
                Addresses = new Address()
            });

            //Arrange'
            List<Order> Orders = new List<Order>();
            Orders.Add(new Order
            {
                OrderId = 1,
                DateTime = DateTime.Parse("2001-08-21 04:45:41"),
                UserId = 1,
                Items = new List<Item>(),
                User = new User()
            });

            Orders.Add(new Order
            {
                OrderId = 1,
                DateTime = DateTime.Parse("2001-08-21 04:45:41"),
                UserId = 2,
                Items = new List<Item>(),
                User = new User()


            });

            _OrderRepository
                .Setup(a => a.GetAll())
                .ReturnsAsync(Orders);
            //Act
            var result = await _sut.GetAllOrder();

            //Assert
            Assert.NotNull(result);
            Assert.Equal(2, result.Count);
            Assert.IsType<List<OrderResponse>>(result);

        }

        [Fact]
        public async void GetAll_ShouldReturnListOfUserResponses_WhenAuhtorsExist()
        {
            NewOrder newOrder = new NewOrder
            {
                DateTime = DateTime.Parse("2001-08-21 04:45:41"),
                UserId = 2,

            };

            int Orderid = 1;

            Order Order = new Order
            {
                OrderId = Orderid,
                DateTime = DateTime.Parse("2001-08-21 04:45:41"),
                UserId = 2,
                Items = new List<Item>(),
                User = new User()

            };

            _OrderRepository
                .Setup(a => a.Create(It.IsAny<Order>()))
                .ReturnsAsync(Order);

            //act

            var result = await _sut.Create(newOrder);

            //assert
            Assert.NotNull(result);
            Assert.IsType<OrderResponse>(result);
            Assert.Equal(Orderid, result.OrderId);
            Assert.Equal(newOrder.DateTime, result.DateTime);
            Assert.Equal(newOrder.UserId, result.UserId);

        }

        [Fact]
        public async void GetAll_ShouldReturnEmptyListOfOrderResponses_WhenNoUsersExists()
        {
            List<Order> Orders = new List<Order>();

            _OrderRepository
                .Setup(a => a.GetAll())
                .ReturnsAsync(Orders);

            //Act
            var result = await _sut.GetAllOrder();

            //Assert 
            Assert.NotNull(result);
            Assert.Empty(result);
            Assert.IsType<List<OrderResponse>>(result);

        }

        [Fact]
        public async void GetAll_ShouldReturnnull_WhenNoUsersExists()
        {
            //arrange
            int Orderid = 1;

            _OrderRepository
                .Setup(a => a.GetById(It.IsAny<int>()))
                .ReturnsAsync(() => null);

            //Act
            var result = await _sut.GetById(Orderid);

            //Assert 
            Assert.Null(result);

        }

        [Fact]
        public async void Create_shouldreturnUserresponse_whencreateissuccess()
        {
            //arrange
            NewOrder newOrder = new NewOrder
            {
                
                DateTime = DateTime.Parse("2001-08-21 04:45:41"),
                UserId = 2,
                
            };

            int OrderId = 10;

            Order Order = new Order
            {
                OrderId = OrderId,
                DateTime = DateTime.Parse("2001-08-21 04:45:41"),
                UserId = 2,
                Items = new List<Item>(),
                User = new User()

            };

            _OrderRepository
                .Setup(a => a.Create(It.IsAny<Order>()))
                .ReturnsAsync(Order);

            //act

            var result = await _sut.Create(newOrder);

            //assert
            Assert.NotNull(result);
            Assert.IsType<OrderResponse>(result);
            Assert.Equal(OrderId, result.OrderId);
            Assert.Equal(newOrder.DateTime, result.DateTime);
            Assert.Equal(newOrder.UserId, result.UserId);

        }

        [Fact]
        public async void Update_shouldreturnupdateeUserresponse_whenupdateissuccess()
        {
            UpdateOrder update = new UpdateOrder
            {
                DateTime = DateTime.Parse("2001-08-21 04:45:41"),
                UserId = 2,
                

            };

            int Orderid = 1;

            Order Order = new Order
            {
                OrderId = Orderid,
                DateTime = DateTime.Parse("2001-08-21 04:45:41"),
                UserId = 2,
                Items = new List<Item>(),
                User = new User()

            };

            _OrderRepository
                .Setup(a => a.Update(It.IsAny<int>(), It.IsAny<Order>()))
                .ReturnsAsync(Order);

            //act

            var result = await _sut.Update(Orderid, update);

            //assert
            Assert.NotNull(result);
            Assert.IsType<OrderResponse>(result);
            Assert.Equal(Orderid, result.OrderId);
            Assert.Equal(update.DateTime, result.DateTime);
            Assert.Equal(update.UserId, result.UserId);

        }

        [Fact]
        public async void Update_shouldreturnnull_whenUsernoexist()
        {
            UpdateOrder update = new UpdateOrder
            {
                DateTime = DateTime.Parse("2001-08-21 04:45:41"),
                UserId = 2,
             
            };

            int OrderId = 1;



            _OrderRepository
                .Setup(a => a.Update(It.IsAny<int>(), It.IsAny<Order>()))
                .ReturnsAsync(() => null);

            //act

            var result = await _sut.Update(OrderId, update);

            //assert
            Assert.Null(result);


        }

        [Fact]
        public async void Delete_ShouldReturnTrue_WhenDeleteIsSuccess()
        {
            //arrange
            int Orderid = 1;
            Order Order = new Order
            {
                DateTime = DateTime.Parse("2001-08-21 04:45:42"),
                UserId = 2,

            };



            _OrderRepository
                .Setup(a => a.Delete(It.IsAny<int>()))
                .ReturnsAsync(Order);

            //act

            var result = await _sut.Delete(Orderid);

            //assert
            Assert.True(result);


        }
    }
}
