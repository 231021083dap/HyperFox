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
    public interface IGenreService
    {
        Task<List<GenreResponse>> GetAllGenres();
        Task<GenreResponse> GetById(int genreId);
        Task<GenreResponse> Create(NewGenre newGenre);
        Task<GenreResponse> Update(int genreId, UpdateGenre updateGenre);
        Task<bool> Delete(int genreId);
    }

    public class GenreService : IGenreService
    {
        private readonly IGenreRepository _genreRepository;

        public GenreService(IGenreRepository genreRepository)
        {
            _genreRepository = genreRepository;
        }

        public async Task<List<GenreResponse>> GetAllGenres()
        {
            List<Genre> genres = await _genreRepository.GetAll();

            return genres == null ? null : genres.Select(g => new GenreResponse
            {
                GenreId = g.GenreId,
                GenreName = g.GenreName,
                Films = g.Films.Select(f => new GenreFilmResponse
                {
                    FilmId = f.FilmId,
                    FilmName = f.FilmName,
                    ReleaseDate = f.ReleaseDate,
                    RuntimeInMin = f.RuntimeInMin,
                    Description = f.Description,
                    Price = f.Price,
                    Stock = f.Stock,
                    Image = f.Image
                }).ToList()
            }).ToList();
        }

        public async Task<GenreResponse> GetById(int genreId)
        {
            Genre genre = await _genreRepository.GetById(genreId);

            return genre == null ? null : new GenreResponse
            {
                GenreId = genre.GenreId,
                GenreName = genre.GenreName,
                Films = genre.Films.Select(f => new GenreFilmResponse
                {
                    FilmId = f.FilmId,
                    FilmName = f.FilmName,
                    ReleaseDate = f.ReleaseDate,
                    RuntimeInMin = f.RuntimeInMin,
                    Description = f.Description,
                    Price = f.Price,
                    Stock = f.Stock,
                    Image = f.Image
                }).ToList()
            };
        }

        public async Task<GenreResponse> Create(NewGenre newGenre)
        {
            Genre genre = new Genre
            {
                GenreName = newGenre.GenreName
            };

            genre = await _genreRepository.Create(genre);

            return genre == null ? null : new GenreResponse
            {
                GenreId = genre.GenreId,
                GenreName = genre.GenreName
            };
        }

        public async Task<GenreResponse> Update(int genreId, UpdateGenre updateGenre)
        {
            Genre genre = new Genre
            {
                GenreName = updateGenre.GenreName
            };

            genre = await _genreRepository.Update(genreId, genre);

            return genre == null ? null : new GenreResponse
            {
                GenreId = genre.GenreId,
                GenreName = genre.GenreName
            };
        }

        public async Task<bool> Delete(int genreId)
        {
            var result = await _genreRepository.Delete(genreId);

            return true;
        }
    }
}
