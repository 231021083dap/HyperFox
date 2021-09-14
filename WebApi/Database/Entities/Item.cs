using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Entities
{
    public class Item
    {
        [Key]
        public int ItemId { get; set; }

        [ForeignKey("FilmId")]
        public int FilmId { get; set; }
        [ForeignKey("OrderId")]
        public int OrderId { get; set; }
        public int Quantity { get; set; }
        //ForeignKey ??
        public int Price { get; set; }
    }
}