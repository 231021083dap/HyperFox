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
            List<FilmResponse> Films = new();

            Films.Add(new FilmResponse
            {
                FilmId = 1,
                FilmName = "The lord of the rings",
                ReleaseDate = "16-09-2001",
                RuntimeInMin = 123,
                Description = "This movie is about a ring",
                Price = 79.99,
                Stock = 50,
                Image = "C:\\Users\\Tec\\Pictures\\1.jpg"
            });

            Films.Add(new FilmResponse
            {
                FilmId = 2,
                FilmName = "Harry potter",
                ReleaseDate = "16-09-2001",
                RuntimeInMin = 123,
                Description = "This movie is about the wizard world",
                Price = 79.99,
                Stock = 50,
                Image = "C:\\Users\\Tec\\Pictures\\2.jpg"
            });

            return Films;
        }
    }
}
