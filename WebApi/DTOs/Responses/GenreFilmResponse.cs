using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.DTOs.Responses
{
    public class GenreFilmResponse
    {
        public int FilmId { get; set; }
        public string FilmName { get; set; }
        public string ReleaseDate { get; set; }
        public int RuntimeInMin { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        public int Stock { get; set; }
        public string Image { get; set; }
    }
}
