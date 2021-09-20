using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.DTOs.Requests;
using WebApi.DTOs.Responses;
using WebApi.Services;

namespace WebApi.Controllers
{

    //Api/Order 
        [Route("api/[controller]")]
    [ApiController]

    public class OrderController : ControllerBase
    {
        private readonly IOrderService _OrderService;

        public OrderController(IOrderService authorService)
        {
            _OrderService = authorService;

        }

        /// <summary>
        ///  try to Execute GetAllOrder from OrderService in order to get a list of all Orderes from the Database
        /// </summary>
        /// <returns>
        ///   <para>- A List of all Orderes in the Database </para>
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
                List<OrderResponse> Orderes = await _OrderService.GetAllOrder();

                if (Orderes == null)
                {
                    return Problem("Got no data, not even an empty list, this is unexpected");
                }
                if (Orderes.Count == 0)
                {
                    return NoContent();
                }

                return Ok(Orderes);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }

        }

        /// <summary>Receive an int from the user through Httpget and display an Order</summary>
        /// <param name="OrderId">The Order identifier.</param>
        /// <returns>
        ///   <para>- Information about the given Order</para>
        ///   <para>- Can return Order not found </para>
        ///   <para>- Can return an Exception </para>
        /// </returns>
        /// 
        [HttpGet("{OrderId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetById([FromRoute] int OrderId)
        {
            try
            {
                OrderResponse Orderes = await _OrderService.GetById(OrderId);

                if (Orderes == null)
                {
                    return NotFound();
                }


                return Ok(Orderes);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        /// <summary>Receive information from the user through Httpget and Creates a new Order</summary>
        /// <param name="newOrder">
        ///   <para>
        /// The new Order.</para>
        /// </param>
        /// <returns>
        ///   <para>New Order successfully added </para>
        ///   <para>Can return a problem </para>
        ///   <para>Can return an Exception</para>
        /// </returns>
        /// 
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Create([FromBody] NewOrder newOrder)
        {
            try
            {
                OrderResponse Authors = await _OrderService.Create(newOrder);

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

        /// <summary>Receives an Order id, an Order information though httpput and Updates the Order </summary>
        /// <param name="OrderId">The Order identifier.</param>
        /// <param name="updateOrder">The update Order.</param>
        /// <returns>
        ///   <para>The Order is successfully updated </para>
        ///   <para>Can return a problem </para>
        ///   <para>Can return an Exception</para>
        /// </returns>
        /// 
        [HttpPut("{OrderId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Update([FromRoute] int OrderId, [FromBody] UpdateOrder updateOrder)
        {
            try
            {
                OrderResponse Author = await _OrderService.Update(OrderId, updateOrder);

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

        [HttpDelete("{OrderId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Delete([FromRoute] int OrderId)
        {
            try
            {
                bool result = await _OrderService.Delete(OrderId);

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
