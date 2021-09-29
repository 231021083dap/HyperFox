using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Database;
using WebApi.Database.Entities;

namespace WebApi.Repositories
{
    public interface IFilmRepository
    {
        Task<List<Film>> GetAll();
        Task<Film> GetById(int FilmId);
        Task<Film> Create(Film film);
        Task<Film> Update(int FilmId, Film film);
        Task<Film> Delete(int FilmId);
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
            return await _context.Film
                .Include(g => g.Genre)
                .ToListAsync();
        }

        public async Task<Film> GetById(int FilmId)
        {
            return await _context.Film
                .Include(g => g.Genre)
                .FirstOrDefaultAsync(f => f.FilmId == FilmId);
        }

        public async Task<Film> Create(Film film)
        {
            _context.Film.Add(film);
            await _context.SaveChangesAsync();
            return film;
        }

        public async Task<Film> Update(int FilmId, Film film)
        {
            Film updateFilm = await _context.Film.FirstOrDefaultAsync(f => f.FilmId == FilmId);

            if (updateFilm != null)
            {
                updateFilm.FilmName = film.FilmName;
                updateFilm.ReleaseDate = film.ReleaseDate;
                updateFilm.RuntimeInMin = film.RuntimeInMin;
                updateFilm.Description = film.Description;
                updateFilm.Price = film.Price;
                updateFilm.Stock = film.Stock;
                updateFilm.Image = film.Image;
                updateFilm.GenreId = film.GenreId;

                await _context.SaveChangesAsync();
            }

            return updateFilm;
        }

        public async Task<Film> Delete(int FilmId)
        {
            Film film = await _context.Film.FirstOrDefaultAsync(f => f.FilmId == FilmId);

            if (film != null)
            {
                _context.Film.Remove(film);
                await _context.SaveChangesAsync();
            }

            return film;
        }
    }
}
