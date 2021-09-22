using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.DTOs.Responses
{
    public class ItemResponse
    {
        //Getters and setters
        public int ItemId { get; set; }
        public int FilmId{ get; set; }
        public int OrderId { get; set; }
        public int Quantity { get; set; }
        public int  Price { get; set; }

        public ItemFilmResponse Film { get; set; }
    }
}
