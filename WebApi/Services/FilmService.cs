using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Database.Entities;
using WebApi.DTOs.Responses;
using WebApi.Repositories;

namespace WebApi.Services
{
    public interface IFilmService
    {
        List<FilmResponse> GetAllFilms();
    }

    public class FilmService : IFilmService
    {
        private readonly IFilmRepository _filmRepository;

        public FilmService(IFilmRepository filmRepository)
        {
            _filmRepository = filmRepository;
        }

        public List<FilmResponse> GetAllFilms()
        {
            IEnumerable<Film> Films = _filmRepository.GetAll();

            return Films.Select(f => new FilmResponse
            {
                FilmId = f.FilmId,
                FilmName = f.FilmName,
                ReleaseDate = f.ReleaseDate,
                RuntimeInMin = f.RuntimeInMin,
                Description = f.Description,
                Price = f.Price,
                Stock = f.Stock,
                Image = f.Image
            }).ToList();
        }
    }
}
