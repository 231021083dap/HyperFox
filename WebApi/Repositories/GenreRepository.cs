using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Database;
using WebApi.Database.Entities;

namespace WebApi.Repositories
{
    public interface IGenreRepository
    {
        Task<List<Genre>> GetAll();
        Task<Genre> GetById(int GenreId);
        Task<Genre> Create(Genre Genre);
        Task<Genre> Update(int GenreId, Genre Genre);
        Task<Genre> Delete(int GenreId);
    }

    public class GenreRepository : IGenreRepository
    {
        private readonly WebApiContext _context;

        public GenreRepository(WebApiContext context)
        {
            _context = context;
        }

        public async Task<List<Genre>> GetAll()
        {
            return await _context.Genre
                .Include(f => f.Films)
                .ToListAsync();
        }

        public async Task<Genre> GetById(int GenreId)
        {
            return await _context.Genre
                .Include(f => f.Films)
                .FirstOrDefaultAsync(g => g.GenreId == GenreId);
        }

        public async Task<Genre> Create(Genre Genre)
        {
            _context.Genre.Add(Genre);
            await _context.SaveChangesAsync();
            return Genre;
        }

        public async Task<Genre> Update(int GenreId, Genre Genre)
        {
            Genre updateGenre = await _context.Genre.FirstOrDefaultAsync(a => a.GenreId == GenreId);

            if (updateGenre != null)
            {
                updateGenre.GenreName = Genre.GenreName;

                await _context.SaveChangesAsync();
            }

            return updateGenre;
        }

        public async Task<Genre> Delete(int GenreId)
        {
            Genre genre = await _context.Genre.FirstOrDefaultAsync(g => g.GenreId == GenreId);

            if (genre != null)
            {
                _context.Genre.Remove(genre);
                await _context.SaveChangesAsync();
            }

            return genre;
        }
    }
}
