using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Database.Entities;
using WebApi.DTOs.Requests;
using WebApi.DTOs.Responses;
using WebApi.Repositories;

namespace WebApi.Services
{
    public interface IFilmService
    {
        Task<List<FilmResponse>> GetAllFilms();
        Task<FilmResponse> GetById(int filmId);
        Task<FilmResponse> Create(NewFilm newFilm);
        Task<FilmResponse> Update(int filmId, UpdateFilm updateFilm);
        Task<bool> Delete(int filmId);
    }

    public class FilmService : IFilmService
    {
        private readonly IFilmRepository _filmRepository;

        public FilmService(IFilmRepository filmRepository)
        {
            _filmRepository = filmRepository;
        }

        public async Task<List<FilmResponse>> GetAllFilms()
        {
            List<Film> films = await _filmRepository.GetAll();

            return films == null ? null : films.Select(f => new FilmResponse
            {
                FilmId = f.FilmId,
                FilmName = f.FilmName,
                ReleaseDate = f.ReleaseDate,
                RuntimeInMin = f.RuntimeInMin,
                Description = f.Description,
                Price = f.Price,
                Stock = f.Stock,
                Image = f.Image,
                Genre = new FilmGenreResponse
                {
                    GenreId = f.Genre.GenreId,
                    GenreName = f.Genre.GenreName
                }
            }).ToList();
        }

        public async Task<FilmResponse> GetById(int filmId)
        {
            Film film = await _filmRepository.GetById(filmId);

            return film == null ? null : new FilmResponse
            {
                FilmId = film.FilmId,
                FilmName = film.FilmName,
                ReleaseDate = film.ReleaseDate,
                RuntimeInMin = film.RuntimeInMin,
                Description = film.Description,
                Price = film.Price,
                Stock = film.Stock,
                Image = film.Image,
                Genre = new FilmGenreResponse
                {
                    GenreId = film.Genre.GenreId,
                    GenreName = film.Genre.GenreName
                }
            };
        }

        public async Task<FilmResponse> Create(NewFilm newFilm)
        {
            Film film = new Film
            {
                FilmName = newFilm.FilmName,
                ReleaseDate = newFilm.ReleaseDate,
                RuntimeInMin = newFilm.RuntimeInMin,
                Description = newFilm.Description,
                Price = newFilm.Price,
                Stock = newFilm.Stock,
                Image = newFilm.Image,
                GenreId = newFilm.GenreId
            };

            film = await _filmRepository.Create(film);

            return film == null ? null : new FilmResponse
            {
                FilmId = film.FilmId,
                FilmName = film.FilmName,
                ReleaseDate = film.ReleaseDate,
                RuntimeInMin = film.RuntimeInMin,
                Description = film.Description,
                Price = film.Price,
                Stock = film.Stock,
                Image = film.Image,
                Genre = new FilmGenreResponse
                {
                    GenreId = film.Genre.GenreId,
                    GenreName = film.Genre.GenreName
                }
            };
        }

        public async Task<FilmResponse> Update(int filmId, UpdateFilm updateFilm)
        {
            Film film = new Film
            {
                FilmName = updateFilm.FilmName,
                ReleaseDate = updateFilm.ReleaseDate,
                RuntimeInMin = updateFilm.RuntimeInMin,
                Description = updateFilm.Description,
                Price = updateFilm.Price,
                Stock = updateFilm.Stock,
                Image = updateFilm.Image,
                GenreId = updateFilm.GenreId
            };

            film = await _filmRepository.Update(filmId, film);

            return film == null ? null : new FilmResponse
            {
                FilmId = film.FilmId,
                FilmName = film.FilmName,
                ReleaseDate = film.ReleaseDate,
                RuntimeInMin = film.RuntimeInMin,
                Description = film.Description,
                Price = film.Price,
                Stock = film.Stock,
                Image = film.Image,
                Genre = new FilmGenreResponse
                {
                    GenreId = film.Genre.GenreId,
                    GenreName = film.Genre.GenreName
                }
            };
        }

        public async Task<bool> Delete(int filmId)
        {
            var result = await _filmRepository.Delete(filmId);
            return true;
        }
    }
}
