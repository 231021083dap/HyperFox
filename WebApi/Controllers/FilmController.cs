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
    public class FilmController : Controller
    {
        private readonly IFilmService _filmServise;

        public FilmController(IFilmService filmService)
        {
            _filmServise = filmService;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            List<FilmResponse> Films = _filmServise.GetAllFilms();

            if (Films.Count == 0)
            {
                return NoContent();
            }

            return Ok(Films);
        }
    }
}
