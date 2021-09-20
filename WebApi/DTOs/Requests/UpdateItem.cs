using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.DTOs.Requests
{
    //"Updated" item get / set.
    public class UpdateItem
    {
        [Required] //Its required to have an ItemId... etc.
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
