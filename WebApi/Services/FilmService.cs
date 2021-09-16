using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.DTOs.Responses;

namespace WebApi.Services
{
    public interface IFilmService
    {
        List<FilmResponse> GetAllFilms();
    }

    public class FilmService : IFilmService
    {
        public List<FilmResponse> GetAllFilms()
        {
            throw new NotImplementedException();
        }
    }
}
