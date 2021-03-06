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
        Task<FilmResponse> GetById(int FilmId);
        Task<FilmResponse> Create(NewFilm newFilm);
        Task<FilmResponse> Update(int FilmId, UpdateFilm updateFilm);
        Task<bool> Delete(int FilmId);
    }

    public class FilmService : IFilmService
    {
        private readonly IFilmRepository _filmRepository;
        private readonly IGenreRepository _genreRepository;

        public FilmService(IFilmRepository filmRepository, IGenreRepository genreRepository)
        {
            _filmRepository = filmRepository;
            _genreRepository = genreRepository;
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

        public async Task<FilmResponse> GetById(int FilmId)
        {
            Film film = await _filmRepository.GetById(FilmId);

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

            if (film != null)
            {
                Genre genre = await _genreRepository.GetById(film.GenreId);

                return new FilmResponse
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
                        GenreId = genre.GenreId,
                        GenreName = genre.GenreName
                    }
                };
            }

            return null;
        }

        //Task, async, await.
        public async Task<FilmResponse> Update(int FilmId, UpdateFilm updateFilm)
        {
            Film film = new Film //New object of film.
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

            // To show user its been updated.
            film = await _filmRepository.Update(FilmId, film);
            



            if (film != null)
            {
                film.Genre = await _genreRepository.GetById(film.GenreId);

                return new FilmResponse
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

            return null;
        }

        public async Task<bool> Delete(int FilmId)
        {
            var result = await _filmRepository.Delete(FilmId);
            return true;
        }
    }
}
