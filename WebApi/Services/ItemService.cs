using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.DTOs.Requests;
using WebApi.DTOs.Responses;
using WebApi.Entities;
using WebApi.Repositories;

namespace WebApi.Services
{

    //Interfaces for Controllers, a blueprint.
    public interface IItemService
    {
        Task<List<ItemResponse>> GetAllItems();
        Task<ItemResponse> GetById(int itemId);
        Task<ItemResponse> Create(NewItem newItem);
        Task<ItemResponse> Update(int itemId, UpdateItem updateItem);
        Task<bool> Delete(int itemId);
    }

    public class ItemService : IItemService  
    {
        private readonly IItemRepository _itemRepository;
        public ItemService(IItemRepository itemRepository)
        {
            _itemRepository = itemRepository;
        }

        //Methods
        public async Task<List<ItemResponse>> GetAllItems()
        {
            List<Item> items = await _itemRepository.GetAll();
            return items == null ? null : items.Select(a => new ItemResponse
            {
                ItemId = a.ItemId,
                FilmId = a.FilmId,
                OrderId = a.OrderId,
                Quantity = a.Quantity,
                Price = a.Price
            }).ToList();
        }

        public async Task<ItemResponse> GetById(int itemId)
        {
            Item item = await _itemRepository.GetById(itemId);

            return item == null ? null : new ItemResponse
            {
                ItemId = item.ItemId,
                FilmId = item.FilmId,
                OrderId = item.OrderId,
                Quantity = item.Quantity,
                Price = item.Price
            };
        }
            
        public async Task<ItemResponse> Create(NewItem newItem)
        {
            Item item = new Item
            {
                ItemId = newItem.ItemId,
                FilmId = newItem.FilmId,
                OrderId = newItem.OrderId,
                Quantity = newItem.Quantity,
                Price = newItem.Price
            };

            item = await _itemRepository.Create(item);

            return item == null ? null : new ItemResponse
            {
                ItemId = item.ItemId,
                FilmId = item.FilmId,
                OrderId = item.OrderId,
                Quantity = item.Quantity,
                Price = item.Price
            };
        }

        public async Task<ItemResponse> Update(int itemId, UpdateItem updateItem)
        {
            Item item = new Item
            {
                ItemId = updateItem.ItemId,
                FilmId = updateItem.ItemId,
                OrderId = updateItem.ItemId,
                Quantity = updateItem.Quantity,
                Price = updateItem.Price
            };
            item = await _itemRepository.Update(itemId, item);

            return item == null ? null : new ItemResponse
            {
                ItemId = item.ItemId,
                FilmId = item.FilmId,
                OrderId = item.OrderId,
                Quantity = item.Quantity,
                Price = item.Price
            };
        }
    
        public async Task<bool> Delete(int itemId)
        {
            var result = await _itemRepository.Delete(itemId);
            return true;
        }
    
    }
}
