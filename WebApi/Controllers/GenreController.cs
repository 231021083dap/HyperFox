using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.DTOs.Responses;
using WebApi.Services;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GenreController : Controller
    {
        private readonly IGenreService _genreService;

        public GenreController(IGenreService genreService)
        {
            _genreService = genreService;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            List<GenreResponse> Genres = _genreService.GetAllGenres();

            if (Genres == null)
            {
                return Problem("Got no data, this is unexpected");
            }

            if (Genres.Count == 0)
            {
                return NoContent();
            }

            return Ok(Genres);
        }
    }
}
