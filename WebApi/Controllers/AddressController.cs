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
    public class AddressController : ControllerBase
    {
        private readonly IAddressService _addressService;

        public AddressController(IAddressService authorService)
        {
            _addressService = authorService;

        }


        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                List<AddressResponse> Addresses = await _addressService.GetAllAddress();

                if (Addresses == null)
                {
                    return Problem("Got no data, not even an empty list, this is unexpected");
                }
                if (Addresses.Count == 0)
                {
                    return NoContent();
                }

                return Ok(Addresses);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

    }
}
