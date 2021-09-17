using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Database.Entities;

namespace WebApi.Repositories
{
    public interface IFilmRepository
    {
        Task<List<Film>> GetAll();
        Task<Film> GetById(int filmId);
    }

    public class FilmRepository : IFilmRepository
    {
        private readonly WebApiContext _context;

        public FilmRepository(WebApiContext context)
        {
            _context = context;
        }

        public async Task<List<Film>> GetAll()
        {
            return await _context.Film.ToListAsync();
        }

        public async Task<Film> GetById(int filmId)
        {
            return await _context.Film.FirstOrDefaultAsync(f => f.FilmId == filmId);
        }
    }
}
