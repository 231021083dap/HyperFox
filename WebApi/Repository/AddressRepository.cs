using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Database.Entities;
using WebApi.Entities;

namespace WebApi.Repository
{
    public interface IAddressRepository
    {
        Task<List<Address>> GetAll();
        Task<Address> GetById(int AddressId);
        Task<Address> Create(Address address);
        Task<Address> Update(int AddressId, Address Address);
        Task<Address> Delete(int AddressId);
    }


    public class AddressRepository : IAddressRepository
    {
        private readonly WebApiContext _context;

        public AddressRepository(WebApiContext context)
        {
            _context = context;
        }
        public async Task<Address> Create(Address address)
        {
            _context.address.Add(address);
            await _context.SaveChangesAsync();
            return address;
        }

        public async Task<Address> Delete(int AddressId)
        {
            Address address = await _context.address.FirstOrDefaultAsync(a => a.AddressId == AddressId);
            if (address != null)
            {
                _context.address.Remove(address);
                await _context.SaveChangesAsync();
            }
            return address;
        }

        public async Task<List<Address>> GetAll()
        {
            return await _context.address
                //.Include(a => a.UserId)
                .ToListAsync();
        }

        public async Task<Address> GetById(int AddressId)
        {
            return await _context.address
                //.Include(a => a.Users)
                .FirstOrDefaultAsync(a => a.AddressId == AddressId);
        }

        public async Task<Address> Update(int AddressId, Address Address)
        {
            Address Updateaddress = await _context.address.FirstOrDefaultAsync(a => a.AddressId == AddressId);
            if (Updateaddress != null)
            {
                Updateaddress.Add = Address.Add;
                Updateaddress.Postal = Address.Postal;
                Updateaddress.City = Updateaddress.City;

                await _context.SaveChangesAsync();
            }

            return Updateaddress;
        }
    }
}
