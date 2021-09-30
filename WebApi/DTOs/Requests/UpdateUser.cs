using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Auth;

namespace WebApi.DTOs.Requests
{
    public class UpdateUser
    {
        [Required]
        public string UserName { get; set; }

        [Required]
        public string Email { get; set; }
        public string Password { get; set; }
        
        [Required]
        public Role Admin { get; set; }
    }
}
