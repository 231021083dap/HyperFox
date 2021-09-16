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
                GenreName = g.GenreName
            }).ToList();
        }

        public async Task<GenreResponse> GetById(int genreId)
        {
            Genre genre = await _genreRepository.GetById(genreId);

            return genre == null ? null : new GenreResponse
            {
                GenreId = genre.GenreId,
                GenreName = genre.GenreName
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

        public Task<GenreResponse> Update(int genreId, UpdateGenre updateGenre)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Delete(int genreId)
        {
            throw new NotImplementedException();
        }
    }
}
