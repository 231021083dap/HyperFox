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
        public string FilmId{ get; set; }
        public string OrderId { get; set; }
        public string Quantity { get; set; }
        public bool Price { get; set; }
    }
}
