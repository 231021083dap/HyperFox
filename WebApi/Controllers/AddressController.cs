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
    //Api/address 
    [Route("api/[controller]")]
    [ApiController]

    public class AddressController : ControllerBase
    {
        private readonly IAddressService _addressService;

        public AddressController(IAddressService authorService)
        {
            _addressService = authorService;

        }

        /// <summary>Should Return all addresses</summary>
        /// <returns>
        ///   <br />
        /// </returns>
        
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

        [HttpGet("{addressId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetById([FromRoute] int addressId)
        {
            try
            {
                AddressResponse addresses = await _addressService.GetById(addressId);

                if (addresses == null)
                {
                    return NotFound();
                }


                return Ok(addresses);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Create([FromBody] NewAddress newAddress)
        {
            try
            {
                AddressResponse Authors = await _addressService.Create(newAddress);

                if (Authors == null)
                {
                    return Problem("something went wrong");
                }


                return Ok(Authors);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        [HttpPut("{addressId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Update([FromRoute] int addressId, [FromBody] UpdateAddress updateAddress)
        {
            try
            {
                AddressResponse Author = await _addressService.Update(addressId, updateAddress);

                if (Author == null)
                {
                    return Problem("Wrong");
                }


                return Ok(Author);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        [HttpDelete("{addressId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Delete([FromRoute] int addressId)
        {
            try
            {
                bool result = await _addressService.Delete(addressId);

                if (!result)
                {
                    return Problem("wrong");
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
