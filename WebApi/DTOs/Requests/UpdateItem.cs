using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.DTOs.Requests
{
    public class UpdateItem
    {
        [Required]
        public int ItemId { get; set; }

        [Required]
        public int FilmId { get; set; }

        [Required]
        public int OrderId { get; set; }

        [Required]
        public int Quantity { get; set; }

        [Required]
        public int Price { get; set; }
    }
}
