using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Database;
using WebApi.Entities;

namespace WebApi.Repositories
{
    public interface IUserRepository
    {
        Task<List<User>> GetAll();
        Task<User> GetById(int UserId);
        Task<User> Create(User user);
        Task<User> Update(int userId, User user);
        Task<User> Delete(int userId);
    }
    public class UserRepository : IUserRepository
    {
        private readonly WebApiContext _context;

        public UserRepository(WebApiContext context)
        {
            _context = context;
        }
        public async Task<List<User>> GetAll()
        {
            return await _context.User.ToListAsync(); //
               
        }
        public async Task<User> GetById(int userId)
        {
            return await _context.User.FirstOrDefaultAsync(a => a.UserId == userId); //
        }
        public async Task<User> Create(User user)
        {
            _context.User.Add(user);
            await _context.SaveChangesAsync();
            return user;
        }
        public async Task<User> Delete(int userId)
        {
            User user = await _context.User.FirstOrDefaultAsync(a => a.UserId == userId);
                if (user != null)
            {
                _context.User.Remove(user);
                await _context.SaveChangesAsync();
            }return user;
        }
        public async Task<User> Update(int userId, User user)
        {
            User updateUser = await _context.User.FirstOrDefaultAsync(a => a.UserId == userId);
                if (updateUser != null)
            {
                updateUser.UserName = user.UserName;
                updateUser.Email = user.Email;
                updateUser.Password = user.Password;
                updateUser.Admin = user.Admin;
                await _context.SaveChangesAsync();
            }return updateUser;
        }
    }
}
