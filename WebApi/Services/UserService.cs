using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Auth;
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
        Task<LoginResponse> Authenticate(LoginRequest login);
        Task<UserResponse> Register(RegisterUser newUser);
        Task<bool> Delete(int userId);

    }
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IJwtUtils _jwUtils;

        public UserService(IUserRepository userRepository, IJwtUtils jwtUtils)
        {
            _userRepository = userRepository;
            _jwUtils = jwtUtils;
        }
        public async Task<List<UserResponse>> GetAllUsers()
        {
            List<User> users = await _userRepository.GetAll();
            return users == null ? null : users.Select(a => new UserResponse
            {
                UserId = a.UserId,
                UserName = a.UserName,
                Email = a.Email,
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
                Admin = user.Admin,
                UserAddressResponses = new UserAddressResponse 
                {
                    AddressId = user.Addresses.AddressId,
                    StreetName = user.Addresses.StreetName,
                    City = user.Addresses.City,
                    Postal = user.Addresses.Postal

                }
                
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
                Admin = user.Admin
            };
        }

        public async Task<bool> Delete(int userId)
        {
            var result = await _userRepository.Delete(userId);
            return true;
        }

        public async Task<LoginResponse> Authenticate(LoginRequest login)
        {
            User user = await _userRepository.GetByEmail(login.Email);
            if (user == null)
            {
                return null;
            }

            if (user.Password == login.Password)
            {
                LoginResponse response = new LoginResponse
                {
                    UserId = user.UserId,
                    Email = user.Email,
                    Username = user.UserName,
                    Admin = user.Admin,
                    Token = _jwUtils.GenerateJwtToken(user)
                };
                return response;
            }

            return null;
        }

        public async Task<UserResponse> Register(RegisterUser newUser)
        {
            User user = new User
            {
                Email = newUser.Email,
                UserName = newUser.Username,
                Password = newUser.Password,
                Admin = Auth.Role.User // force all users created through Register, to Role.User
            };

            user = await _userRepository.Create(user);

            return userResponse(user);
        }

        private UserResponse userResponse(User user)
        {
            return user == null ? null : new UserResponse
            {
                UserId = user.UserId,
                Email = user.Email,
                UserName = user.UserName,
                Admin = user.Admin
            };
        }
    }
}