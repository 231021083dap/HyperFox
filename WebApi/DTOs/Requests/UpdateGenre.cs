using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.DTOs.Requests
{
    public class UpdateGenre
    {
        [Required]
        [StringLength(32, ErrorMessage = "GenreName must be less than 32 chars")]
        [MinLength(1, ErrorMessage = "GenreName must contain atleast 1 char")]
        public string GenreName { get; set; }
    }
}
