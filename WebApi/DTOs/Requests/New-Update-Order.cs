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
        public DateTime  DateTime { get; set; }


    }

    public class UpdateOrder
    {

        [Required]
        public int UserId { get; set; }


        [Required]
        public DateTime DateTime { get; set; }

    }
}
