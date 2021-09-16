using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.DTOs.Requests;
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
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
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

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Create([FromBody] NewGenre newGenre)
        {
            try
            {
                GenreResponse Genre = await _genreService.Create(newGenre);

                if (Genre == null)
                {
                    return Problem("Genre was not created, something went wrong");
                }

                return Ok(Genre);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        [HttpPut("{genreId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Update([FromRoute] int genreId, [FromBody] UpdateGenre updateGenre)
        {
            try
            {
                GenreResponse Genre = await _genreService.Update(genreId, updateGenre);

                if (Genre == null)
                {
                    return Problem("Genre was not updated, something went wrong");
                }

                return Ok(Genre);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        [HttpDelete("{genreId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Delete([FromRoute] int genreId)
        {
            try
            {
                bool result = await _genreService.Delete(genreId);

                if (!result)
                {
                    return Problem("Genre was not deleted, something went wrong");
                }

                return NoContent();
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }
    }
}
