using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Database;
using WebApi.Entities;

namespace WebApi.Repository
{
    public interface IOrderRepository
    {
        Task<List<Order>> GetAll();
        Task<Order> GetById(int OrderId);
        Task<Order> Create(Order Order);
        Task<Order> Update(int OrderId, Order Order);
        Task<Order> Delete(int OrderId);
    }


    public class OrderRepository : IOrderRepository
    {
        private readonly WebApiContext _context;

        public OrderRepository(WebApiContext context)
        {
            _context = context;
        }
        public async Task<Order> Create(Order Order)
        {
            _context.Order.Add(Order);
            await _context.SaveChangesAsync();
            return Order;
        }

        public async Task<Order> Delete(int OrderId)
        {
            Order Order = await _context.Order.FirstOrDefaultAsync(a => a.OrderId == OrderId);
            if (Order != null)
            {
                _context.Order.Remove(Order);
                await _context.SaveChangesAsync();
            }
            return Order;
        }

        public async Task<List<Order>> GetAll()
        {
            return await _context.Order
                .Include(a => a.User)
                .ToListAsync();
        }

        public async Task<Order> GetById(int OrderId)
        {
            return await _context.Order
                .Include(a => a.Items)
                .Include(a => a.User)
                .FirstOrDefaultAsync(a => a.OrderId == OrderId);
        }

        public async Task<Order> Update(int OrderId, Order Order)
        {
            Order UpdateOrder = await _context.Order.FirstOrDefaultAsync(a => a.OrderId == OrderId);
            if (UpdateOrder != null)
            {
                UpdateOrder.UserId = Order.UserId;
                
                UpdateOrder.DateTime = UpdateOrder.DateTime;

                await _context.SaveChangesAsync();
            }

            return UpdateOrder;
        }
    }
}
