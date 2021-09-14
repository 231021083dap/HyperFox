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

    //API Route for Item
    [Route("api/[controller]")]
    [ApiController]

    public class ItemController : Controller
    {
        //To use the ItemService.
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
                List<ItemResponse> Items = await _itemService.GetAllItems();

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

        [HttpGet("{itemId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetById([FromRoute] int itemId)
        {
            try
            {
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

        [HttpGet("{itemId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Create([FromBody] NewItem newItem)
        {
            try
            {
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
        [HttpGet("{itemId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Update ([FromRoute] int itemId, [FromBody] UpdateItem updateItem)
        {
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
        [HttpGet("{itemId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Delete([FromRoute] int itemId)
        {
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
