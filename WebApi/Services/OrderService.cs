using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.DTOs.Requests;
using WebApi.DTOs.Responses;
using WebApi.Repository;
using WebApi.Entities;

namespace WebApi.Services
{
    
        public interface IOrderService
        {

            Task<List<OrderResponse>> GetAllOrder();
            Task<OrderResponse> GetById(int OrderId);
            Task<bool> Delete(int OrderId);
            Task<OrderResponse> Create(NewOrder newOrder);
            Task<OrderResponse> Update(int OrderId, UpdateOrder updateOrder);


        }


        public class OrderService : IOrderService
        {
            private readonly IOrderRepository _OrderRepository;

            public OrderService(IOrderRepository OrderRepository)
            {
                _OrderRepository = OrderRepository;
            }
            public async Task<OrderResponse> Create(NewOrder newOrder)
            {
                Order Order = new()
                {
                    UserId = newOrder.UserId,
                    DateTime = newOrder.DateTime

                };

                Order = await _OrderRepository.Create(Order);

                return Order == null ? null : new OrderResponse
                {
                    OrderId = Order.OrderId,
                    UserId = Order.UserId,
                    DateTime = Order.DateTime

                };
            }

            public async Task<bool> Delete(int OrderId)
            {
                var result = await _OrderRepository.Delete(OrderId);
                return true;
            }

            public async Task<List<OrderResponse>> GetAllOrder()
            {
                List<Order> Orderes = await _OrderRepository.GetAll();

                return Orderes?.Select(a => new OrderResponse
                {
                    OrderId = a.OrderId,
                    UserId = a.UserId,
                    DateTime = a.DateTime


                }).ToList();

            }

            public async Task<OrderResponse> GetById(int OrderId)
            {
                Order Orders = await _OrderRepository.GetById(OrderId);
                return Orders == null ? null : new OrderResponse
                {
                    OrderId = Orders.OrderId,
                    UserId = Orders.UserId,
                    DateTime = Orders.DateTime


                };
            }

            public async Task<OrderResponse> Update(int OrderId, UpdateOrder updateOrder)
            {
                Order Order = new()
                {
                    UserId = updateOrder.UserId,
                    DateTime = updateOrder.DateTime


                };

                Order = await _OrderRepository.Update(OrderId, Order);

                return Order == null ? null : new OrderResponse
                {
                    OrderId = Order.OrderId,
                    UserId = Order.UserId,
                    DateTime = Order.DateTime

                };


            }


        }
    
}
