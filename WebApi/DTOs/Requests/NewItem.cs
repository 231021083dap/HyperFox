using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.DTOs.Requests
{
    //"New" item get / set.
    public class NewItem
    {

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
