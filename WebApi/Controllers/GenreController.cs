using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.DTOs.Responses;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GenreController : Controller
    {
        [HttpGet]
        public IActionResult GetAll()
        {
            List<GenreResponse> Genres = new();

            Genres.Add(new GenreResponse
            {
                GenreId = 1,
                GenreName = "Action"
            });

            Genres.Add(new GenreResponse
            {
                GenreId = 2,
                GenreName = "Comedy"
            });

            return Ok(Genres);
        }
    }
}
