using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.DTOs.Requests
{
    public class NewOrder
    {

        [Required]
        public int UserId { get; set; }

        [Required]
        [StringLength(50, ErrorMessage = "Date and time must be no longer than 50")]
        public string DateTime { get; set; }


    }

    public class UpdateOrder
    {

        [Required]
        public int UserId { get; set; }


        [Required]
        [StringLength(50, ErrorMessage = "Date and time must be no longer than 50")]
        public string DateTime { get; set; }

    }
}
