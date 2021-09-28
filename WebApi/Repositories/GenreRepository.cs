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
        Task<Genre> GetById(int genreId);
        Task<Genre> Create(Genre genre);
        Task<Genre> Update(int genreId, Genre genre);
        Task<Genre> Delete(int genreId);
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

        public async Task<Genre> GetById(int genreId)
        {
            return await _context.Genre
                .Include(f => f.Films)
                .FirstOrDefaultAsync(g => g.GenreId == genreId);
        }

        public async Task<Genre> Create(Genre genre)
        {
            _context.Genre.Add(genre);
            await _context.SaveChangesAsync();
            return genre;
        }

        public async Task<Genre> Update(int genreId, Genre genre)
        {
            Genre updateGenre = await _context.Genre.FirstOrDefaultAsync(a => a.GenreId == genreId);

            if (updateGenre != null)
            {
                updateGenre.GenreName = genre.GenreName;

                await _context.SaveChangesAsync();
            }

            return updateGenre;
        }

        public async Task<Genre> Delete(int genreId)
        {
            Genre genre = await _context.Genre.FirstOrDefaultAsync(g => g.GenreId == genreId);

            if (genre != null)
            {
                _context.Genre.Remove(genre);
                await _context.SaveChangesAsync();
            }

            return genre;
        }
    }
}
