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

    //Route for Item in the API.  localhost/api/item
    [Route("api/[controller]")]
    [ApiController]

    public class ItemController : ControllerBase
    {
        //For reading the ItemService interface.
        private readonly IItemService _itemService;

        //Contrukctor
        public ItemController(IItemService itemService)
        {
            _itemService = itemService;
        }

        //Http getRequest.
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        //GetALl Items.
        public async Task<IActionResult> GetAll()
        {
            try
            {
                // Creates a list of the Items it should get from the service.
                List<ItemResponse> Items = await _itemService.GetAllItems();

                //Checks if there is problems / data
                if (Items == null)
                {
                    return Problem("No Items was found, not even a empty list!");

                }
                if (Items.Count == 0)
                {
                    return NoContent();
                }
                return Ok(Items);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        //Http getRequest.
        [HttpGet("{itemId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        //GetById method.
        public async Task<IActionResult> GetById([FromRoute] int itemId)
        {
            try
            {
                //Checks if there is anything from Service with the Id being send.
                ItemResponse Item = await _itemService.GetById(itemId);

                if (Item == null)
                {
                    return NotFound();
                }

                return Ok(Item);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }
        
        //Http postRequest.
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        // Create method for creating an new Item.
        public async Task<IActionResult> Create([FromBody] NewItem newItem)
        {
            try
            {
                //Tries to create the item, if it fails and error will occour.
                ItemResponse Item = await _itemService.Create(newItem);
                if (Item == null)
                {
                    return Problem("Item was not created, something went wrong!");
                };
                return Ok(Item);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }
        //Http putRequest
        [HttpPut("{itemId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        //Update method.
        public async Task<IActionResult> Update ([FromRoute] int itemId, [FromBody] UpdateItem updateItem)
        {
            //Tries to update the Item.
            try
            {
                ItemResponse Item = await _itemService.Update(itemId, updateItem);

                if (Item == null)
                {
                    return Problem("Item was NOT updated! something went wrong!");
                }
                return Ok(Item);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        //Http DeleteRequest
        [HttpDelete("{itemId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        //Delete method 
        public async Task<IActionResult> Delete([FromRoute] int itemId)
        {
            //Tries to delete the Item.
            try
            {
                bool result = await _itemService.Delete(itemId);

                if (!result)
                {
                    return Problem("Item was not deleted, something went wrong");
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
