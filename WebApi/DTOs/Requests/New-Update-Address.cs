using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.DTOs.Requests
{
    public class NewAddress
    {
        [Required]
        [StringLength(80, ErrorMessage ="Address must be less than 80 chars")]
        public string StreetName { get; set; }

        [Required]
        [Range(1050,9990, ErrorMessage ="Invalid postal number please try again")]
        public int Postal { get; set; }

        [Required]
        [StringLength(40,ErrorMessage = "City name must be less than 40 chars")]
        public string City { get; set; }
    }

    public class UpdateAddress
    {
        [Required]
        [StringLength(80, ErrorMessage = "Address must be less than 80 chars")]
        public string StreetName { get; set; }

        [Required]
        [Range(1050,9990, ErrorMessage = "Invalid postal number please try again")]
        public int Postal { get; set; }

        [Required]
        [StringLength(40, ErrorMessage = "City name must be less than 40 chars")]
        public string City { get; set; }
    }
}
