using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.DTOs.Requests;
using WebApi.DTOs.Responses;
using WebApi.Entities;
using WebApi.Repository;

namespace WebApi.Services
{
    public interface IAddressService
    {
        
        Task<List<AddressResponse>> GetAllAddress();
        Task<AddressResponse> GetById(int AddressId);
        Task<bool> Delete(int AddressId);
        Task<AddressResponse> Create(NewAddress newAddress);
        Task<AddressResponse> Update(int addressId, UpdateAddress updateAddress);


    }

    public class AddressService : IAddressService
    {
        private readonly IAddressRepository _AddressRepository;

        public AddressService(IAddressRepository addressRepository)
        {
            _AddressRepository = addressRepository;
        }
        public async Task<AddressResponse> Create(NewAddress newAddress)
        {
            Address address = new()
            {
                StreetName = newAddress.StreetName,
                Postal = newAddress.Postal,
                City = newAddress.City,
                UserId = newAddress.UserId

            };

            address = await _AddressRepository.Create(address);

            return address == null ? null : new AddressResponse
            {
                AddressId = address.AddressId,
                StreetName = address.StreetName,
                Postal = address.Postal,
                City = address.City

            };
        }

        public async Task<bool> Delete(int AddressId)
        {
            var result = await _AddressRepository.Delete(AddressId);
            return true;
        }

        public async Task<List<AddressResponse>> GetAllAddress()
        {
            List<Address> addresses = await _AddressRepository.GetAll();

            return addresses?.Select(a => new AddressResponse
            {
                AddressId = a.AddressId,
                StreetName = a.StreetName,
                Postal = a.Postal,
                City = a.City,
                UserId = a.UserId,
                //User = new AddressUserResponse
                //{
                //    UserId = a.User.UserId,
            


                //}

            }).ToList();

        }

        public async Task<AddressResponse> GetById(int AddressId)
        {
            Address addresses = await _AddressRepository.GetById(AddressId);
            return addresses == null ? null : new AddressResponse
            {
                AddressId = addresses.AddressId,
                StreetName = addresses.StreetName,
                Postal = addresses.Postal,
                City = addresses.City,
                User = new AddressUserResponse
                {
                    UserId = addresses.User.UserId,
                    UserName = addresses.User.UserName,
                    Email = addresses.User.Email,
                    Admin = addresses.User.Admin
                }

            };
        }

        public async Task<AddressResponse> Update(int addressId, UpdateAddress updateAddress)
        {
            Address address = new()
            {
                StreetName = updateAddress.StreetName,
                City = updateAddress.City,
                Postal = updateAddress.Postal


            };

            address = await _AddressRepository.Update(addressId, address);

            return address == null ? null : new AddressResponse
            {
                AddressId = address.AddressId,
                StreetName = address.StreetName,
                Postal = address.Postal,
                City = address.City

            };


        }
    }
}
