using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Database.Entities;
using WebApi.DTOs.Responses;
using WebApi.Repositories;

namespace WebApi.Services
{
    public interface IGenreService
    {
        List<GenreResponse> GetAllGenres();
    }

    public class GenreService : IGenreService
    {
        private readonly IGenreRepository _genreRepository;

        public GenreService(IGenreRepository genreRepository)
        {
            _genreRepository = genreRepository;
        }

        public List<GenreResponse> GetAllGenres()
        {
            IEnumerable<Genre> Genres = _genreRepository.GetAll();

            return Genres.Select(g => new GenreResponse
            {
                GenreId = g.GenreId,
                GenreName = g.GenreName
            }).ToList();
        }
    }
}
