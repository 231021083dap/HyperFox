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
    public interface IUserService
    {
        Task<List<UserResponse>> GetAllUsers();
        Task<UserResponse> GetById(int userId);
        Task<UserResponse> Create(NewUser newUser);
        Task<UserResponse> Update(int userId, UpdateUser updateUser);
        Task<bool> Delete(int userId);

    }
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        
       public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        public async Task<List<UserResponse>> GetAllUsers()
        {
            List<User> users = await _userRepository.GetAll();
            return users == null ? null : users.Select(a => new UserResponse
            {
                UserId = a.UserId,
                UserName = a.UserName,
                Email = a.Email,
                Password = a.Password,
                Admin = a.Admin
            }).ToList();
        }

        public async Task<UserResponse> GetById(int userId)
        {
            User user = await _userRepository.GetById(userId);
            return user == null ? null : new UserResponse
            {
                UserId = user.UserId,
                UserName = user.UserName,
                Email = user.Email,
                Password = user.Password,
                Admin = user.Admin
            };
        }

        public async Task<UserResponse> Create(NewUser newUser)
        {
            User user = new User
            {
                UserName = newUser.UserName,
                Email = newUser.Email,
                Password = newUser.Password,
                Admin = newUser.Admin
            };
            user = await _userRepository.Create(user);
            
            return user == null ? null : new UserResponse
            {
                UserId = user.UserId,
                UserName = user.UserName,
                Email = user.Email,
                Password = user.Password,
                Admin = user.Admin
            };
        }

      




        public async Task<UserResponse> Update(int userId, UpdateUser updateuser)
        {
            User user = new User
            {
                UserName = updateuser.UserName,
                Email = updateuser.Email,
                Password = updateuser.Password,
                Admin = updateuser.Admin
            };
            user = await _userRepository.Update(userId, user);

            return user == null ? null : new UserResponse
            {
                UserId = user.UserId,
                UserName = user.UserName,
                Email = user.Email,
                Password = user.Password,
                Admin = user.Admin
            };
        }

        public async Task<bool> Delete(int userId)
        {
            var result = await _userRepository.Delete(userId);
            return true;
        }
    }
}
