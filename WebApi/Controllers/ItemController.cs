using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Controllers
{
    
    //API Route for Item
    //[Route("api/[controller]")]
    //[ApiController]
    
    public class ItemController : Controller
    {
        //To use the ItemService.
        //private readonly IItemService _itemService;

        ////Contrukctor
        //public ItemController(IItemService itemService)
        //{
        //    _itemService = itemService;
        //}

        ////Http getRequest.
        //[HttpGet]
        //[ProducesResponseType(StatusCode.Status200Ok)]
        //[ProducesResponseType(StatusCode.Status204NoContent)]
        //[ProducesResponseType(StatusCode.Status500InternakServerError)]

        public IActionResult Index()
        {
            return View();
        }
    }
}
