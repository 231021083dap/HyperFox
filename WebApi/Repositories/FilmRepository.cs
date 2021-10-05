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

        public async Task<Film> Create(Film Film)
        {
            _context.Film.Add(Film);
            await _context.SaveChangesAsync();
            return Film;
        }

        public async Task<Film> Update(int FilmId, Film film)
        {
            Film updateFilm = await _context.Film.FirstOrDefaultAsync(f => f.FilmId == FilmId);

            if (updateFilm != null)
            {
                updateFilm.FilmName = Film.FilmName;
                updateFilm.ReleaseDate = Film.ReleaseDate;
                updateFilm.RuntimeInMin = Film.RuntimeInMin;
                updateFilm.Description = Film.Description;
                updateFilm.Price = Film.Price;
                updateFilm.Stock = Film.Stock;
                updateFilm.Image = Film.Image;
                updateFilm.GenreId = Film.GenreId;

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

        //public async Task<Film> GetAllById(int FilmId, Genre Genre)
        //{
        //    return await _context.Film
        //        .Include(g => g.Genre)
        //        .FirstOrDefaultAsync(f => f.FilmId == FilmId);
        //}
    }
}
