using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApi.Controllers;
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
using Microsoft.AspNetCore.Mvc.Infrastructure;

namespace WebApiTests.ServiceTests
{
    public class OrderControllerTests
    {
        private readonly OrderController _sut;
        private readonly Mock<IOrderService> _Orderservice = new();

        public OrderControllerTests()
        {
            _sut = new OrderController(_Orderservice.Object);
        }

        [Fact]
        public async void GetAll_ShouldReturnStatusCode200_whenDataExists()
        {
            //Arrange
            List<OrderResponse> Orders = new();



        Orders.Add(new OrderResponse
            {
                OrderId = 1,
                UserId = 1,
            DateTime = DateTime.Parse("2001-08-21 04:45:41"),
            Items = new List<OrderItemResponse>(),
            User = new ()
        });

            Orders.Add(new OrderResponse
            {
                OrderId = 2,
                UserId = 1,
                DateTime = DateTime.Parse("2001-08-21 04:45:41"),
                Items = new List<OrderItemResponse>(),
                User = new()
            });

            _Orderservice
                .Setup(s => s.GetAllOrder())
                .ReturnsAsync(Orders);

            //Act
            var result = await _sut.GetAll();

            //Assert 
            var statusCodeResult = (IStatusCodeActionResult)result;
            Assert.Equal(200, statusCodeResult.StatusCode);



        }

        [Fact]
        public async void GetAll_ShouldReturnStatusCode204_whenNoDataExists()
        {
            List<OrderResponse> Orders = new();

            _Orderservice
                .Setup(s => s.GetAllOrder())
                .ReturnsAsync(Orders);

            //Act
            var result = await _sut.GetAll();

            //Assert 
            var statusCodeResult = (IStatusCodeActionResult)result;
            Assert.Equal(204, statusCodeResult.StatusCode);



        }

        [Fact]
        public async void GetAll_ShouldReturnStatusCode500_whenNullisReturnedFromService()
        {
            List<OrderResponse> Orders = new();
            //
            _Orderservice
                .Setup(s => s.GetAllOrder())
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
            _Orderservice
                .Setup(s => s.GetAllOrder())
                .Returns(() => throw new System.Exception("This is an exception"));

            //Act 
            var result = await _sut.GetAll();

            //Assert 
            var statusCodeResult = (IStatusCodeActionResult)result;
            Assert.Equal(500, statusCodeResult.StatusCode);



        }

    }
}
