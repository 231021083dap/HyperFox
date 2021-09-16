using Microsoft.AspNetCore.Http;
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
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                List<GenreResponse> Genres = await _genreService.GetAllGenres();

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
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        [HttpGet("{genreId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetById([FromRoute] int genreId)
        {
            try 
	        {	        
		        GenreResponse Genre = await _genreService.GetById(genreId);

                if (Genre == null)
	            {
                    return NotFound();
	            }

                return Ok(Genre);
	        }
	        catch (Exception ex)
	        {
                return Problem(ex.Message);
	        }
        }
    }
}
