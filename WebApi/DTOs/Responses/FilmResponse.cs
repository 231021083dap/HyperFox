using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.DTOs.Responses
{
    public class FilmResponse
    {
        public int FilmId { get; set; }
        public string FilmName { get; set; }
        public string ReleaseDate { get; set; }
        public Int16 RuntimeInMin { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int Stock { get; set; }
        public string Image { get; set; }

        public FilmGenreResponse Genre { get; set; }
    }


}
