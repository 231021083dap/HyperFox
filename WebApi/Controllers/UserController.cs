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
    public class UserController : Controller
    {
        //private readonly IUserService _userService;
        
        //public UserController (IUserService userService)
        //{
        //    _userService = userService;
        //}
        
        
        //public async Task<IActionResult> GetAll()
        //{
            
        //    try
        //    {
        //        List<UserResponse> users = await _userService.GetAllUsers();
        //        if (users == null)
        //        {
        //            return Problem("Got no data, not even an empty list, this is unexpected");
        //        }
        //        if (users.Count == 0)
        //        {
        //            return NoContent();
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        return Problem(ex.Message);
        //    }
        //}
    }
}
