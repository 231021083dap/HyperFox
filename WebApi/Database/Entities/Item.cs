using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Entities
{
    //How Item should look in the Database.
    public class Item
    {
        //Primary key
        [Key]
        public int ItemId { get; set; }
        //Foreignkey to FilmId in Film.
        [ForeignKey("Film.FilmId")]
        public int FilmId { get; set; }
        //Foreignkey to OrderId in Order.
        [ForeignKey("Order.OrderId")]
        public int OrderId { get; set; }

        public int Quantity { get; set; }
        //ForeignKey ?? - idk if it should be foreignkey.
        public int Price { get; set; }
    }
}