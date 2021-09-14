using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Database.Entities;
using WebApi.Entities;

namespace WebApi.Repositories
{
    public interface IItemRepository
    {
        Task<List<Item>> GetAll();
        Task<Item> GetById(int itemId);
        Task<Item> Create(Item item);
        Task<Item> Update(int itemId, Item item);
        Task<Item> Delete(int itemId);
    }


    public class ItemRepository : IItemRepository
    {
        private readonly WebApiContext _context;

        public ItemRepository(WebApiContext context)
        {
            _context = context;
        }

        public async Task<Item> GetById(int itemId)
        {
            return await _context.Item.FirstOrDefaultAsync(a => a.ItemId == itemId);
        }

        public async Task<Item> Create(Item item)
        {
            _context.Item.Add(item);
            await _context.SaveChangesAsync();
            return item;
        }

        public async Task<Item> Delete(int itemId)
        {
            Item item = await _context.Item.FirstOrDefaultAsync(a => a.ItemId == itemId);
            if (item != null)
            {
                _context.Item.Remove(item);
                await _context.SaveChangesAsync();

            }
            return item;
        }

        public async Task<Item> Update(int itemId, Item item)
        {
            Item updateItem = await _context.Item.FirstOrDefaultAsync(a => item.ItemId == itemId);
            if (updateItem != null)
            {
                updateItem.ItemId = item.ItemId;
                updateItem.FilmId = item.FilmId;
                updateItem.OrderId = item.OrderId;
                updateItem.Quantity = item.Quantity;
                updateItem.Price = item.Price;
                await _context.SaveChangesAsync();
            }
            return updateItem;
        }

        public async Task<List<Item>> GetAll()
        {
            return await _context.Item.ToListAsync();
        }

    }
}
