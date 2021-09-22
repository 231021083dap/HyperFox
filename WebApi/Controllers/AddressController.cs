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

        public AddressController(IAddressService Orderservice)
        {
            _addressService = Orderservice;

        }

        /// <summary>
        ///  try to Execute GetAllAddress from AddressService in order to get a list of all Addresses from the Database
        /// </summary>
        /// <returns>
        ///   <para>- A List of all Addresses in the Database </para>
        ///   <para>- Can return a problem if Null</para>
        ///   <para>- Can return NoContent if there is no data </para>
        ///   <para>- Can return an Exeception </para>
        ///   <para>
        ///     <br />
        ///   </para>
        /// </returns>
        /// 
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

        /// <summary>Receive an int from the user through Httpget and display an address</summary>
        /// <param name="addressId">The address identifier.</param>
        /// <returns>
        ///   <para>- Information about the given address</para>
        ///   <para>- Can return Address not found </para>
        ///   <para>- Can return an Exception </para>
        /// </returns>
        /// 
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

        /// <summary>Receive information from the user through Httpget and Creates a new address</summary>
        /// <param name="newAddress">
        ///   <para>
        /// The new address.</para>
        /// </param>
        /// <returns>
        ///   <para>New address successfully added </para>
        ///   <para>Can return a problem </para>
        ///   <para>Can return an Exception</para>
        /// </returns>
        /// 
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Create([FromBody] NewAddress newAddress)
        {
            try
            {
                AddressResponse Orders = await _addressService.Create(newAddress);

                if (Orders == null)
                {
                    return Problem("something went wrong");
                }


                return Ok(Orders);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        /// <summary>Receives an address id, an address information though httpput and Updates the Address </summary>
        /// <param name="addressId">The address identifier.</param>
        /// <param name="updateAddress">The update address.</param>
        /// <returns>
        ///   <para>The address is successfully updated </para>
        ///   <para>Can return a problem </para>
        ///   <para>Can return an Exception</para>
        /// </returns>
        /// 
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
