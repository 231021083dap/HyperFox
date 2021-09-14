using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Database.Entities;

namespace WebApi.Repositories
{
    public interface IGenreRepository
    {
        Task<List<Genre>> GetAll();
    }

    public class GenreRepository : IGenreRepository
    {
        private readonly WebApiContext _context;

        private GenreRepository(WebApiContext context)
        {
            _context = context;
        }

        public async Task<List<Genre>> GetAll()
        {
            return await _context.Genre.ToListAsync();
        }
    }
}
