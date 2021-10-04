using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Auth;
using WebApi.DTOs.Requests;
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
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                List<FilmResponse> Films = await _filmServise.GetAllFilms();

                if (Films == null)
                {
                    return Problem("Got no data, this is unexpected");
                }

                if (Films.Count == 0)
                {
                    return NoContent();
                }

                return Ok(Films);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        [HttpGet("{FilmId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetById([FromRoute] int FilmId)
        {
            try
            {
                FilmResponse Film = await _filmServise.GetById(FilmId);

                if (Film == null)
                {
                    return NotFound();
                }

                return Ok(Film);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }
        [Authorize(Role.Admin)]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Create([FromBody] NewFilm newFilm)
        {
            try
            {
                FilmResponse Film = await _filmServise.Create(newFilm);

                if (Film == null)
                {
                    return Problem("Genre was not created, something went wrong");
                }

                return Ok(Film);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }
        [Authorize(Role.Admin)]
        [HttpPut("{FilmId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Update([FromRoute] int FilmId, [FromBody] UpdateFilm updateFilm)
        {
            try
            {
                FilmResponse Film = await _filmServise.Update(FilmId, updateFilm);

                if (Film == null)
                {
                    return Problem("Genre was not updated, something went wrong");
                }

                return Ok(Film);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }
        [Authorize(Role.Admin)]
        [HttpDelete("{FilmId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Delete([FromRoute] int FilmId)
        {
            try
            {
                bool result = await _filmServise.Delete(FilmId);

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
