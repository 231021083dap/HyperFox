﻿using System;
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
                    DateTime = newOrder.DateTime,
                    IId = newOrder.ItemId
                    

                };

                Order = await _OrderRepository.Create(Order);

                return Order == null ? null : new OrderResponse
                {
                    OrderId = Order.OrderId,
                    ItemId = Order.IId,
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
                    ItemId = a.IId,
                    UserId = a.UserId,
                    DateTime = a.DateTime,
                    Item = new OrderItemResponse
                    {
                        ItemId = a.Item.ItemId,
                        Quantity = a.Item.Quantity,
                        Price = a.Item.Price

                    },
                    User = new OrderUserResponse
                    {
                        UserId = a.User.UserId,
                        UserName = a.User.UserName,
                        Email = a.User.Email,
                        Password = a.User.Password,
                        Admin = a.User.Admin
        
                    }
                    




                }).ToList();

            }

            public async Task<OrderResponse> GetById(int OrderId)
            {
                Order Orders = await _OrderRepository.GetById(OrderId);
                return Orders == null ? null : new OrderResponse
                {
                    OrderId = Orders.OrderId,
                    ItemId = Orders.IId,
                    UserId = Orders.UserId,
                    DateTime = Orders.DateTime,
                    Item = new OrderItemResponse
                    {
                        ItemId = Orders.Item.ItemId,
                        Quantity = Orders.Item.Quantity,
                        Price = Orders.Item.Price

                    },
                    User = new OrderUserResponse
                    {
                        UserId = Orders.User.UserId,
                        UserName = Orders.User.UserName,
                        Email = Orders.User.Email,
                        Password = Orders.User.Password,
                        Admin = Orders.User.Admin

                    }


                };
            }

            public async Task<OrderResponse> Update(int OrderId, UpdateOrder updateOrder)
            {
                Order Order = new()
                {
                    UserId = updateOrder.UserId,
                    IId = updateOrder.ItemId,
                    DateTime = updateOrder.DateTime


                };

                Order = await _OrderRepository.Update(OrderId, Order);

                return Order == null ? null : new OrderResponse
                {
                    OrderId = Order.OrderId,
                    ItemId = Order.IId,
                    UserId = Order.UserId,
                    DateTime = Order.DateTime

                };


            }


        }
    
}
