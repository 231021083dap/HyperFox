﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Database.Entities;

namespace WebApi.Entities
{
    //How Item should look in the Database.
    public class Item
    {
        //Primary key
        [Key]
        public int ItemId { get; set; }
        //Foreignkey to FilmId in Film.
        [ForeignKey("FilmId")]
        public int FilmId { get; set; }
        //Foreignkey to OrderId in Order.
        [ForeignKey("OrderId")]
        public int OrderId { get; set; }

        public int Quantity { get; set; }
        //ForeignKey ?? - idk if it should be foreignkey.
        public decimal Price { get; set; }

        public Film Film { get; set; }
    }
}