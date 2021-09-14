using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.DTOs.Requests
{
    public class NewUser
    {
        [Required]
        [StringLength(32, ErrorMessage = "Username must be less than 32 chars")]
        [MinLength(1, ErrorMessage = "Username must contain atleast 1 char")]
        public string UserName { get; set; }

        [Required]
        [StringLength(32, ErrorMessage = "Email must be less than 32 chars")]
        [MinLength(1, ErrorMessage = "Email must contain atleast 1 char")]
        public string Email { get; set; }

        [Required]
        [StringLength(32, ErrorMessage = "Password must be less than 32 chars")]
        [MinLength(6, ErrorMessage = "Email must contain atleast 6 char")]
        public string Password { get; set; }

        [Required]
        [StringLength(5, ErrorMessage = "Admin or user")]
        [MinLength(5, ErrorMessage = "Admin or user")]
        public string Admin { get; set; }
    }
}
