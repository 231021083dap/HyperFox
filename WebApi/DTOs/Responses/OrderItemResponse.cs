using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.DTOs.Responses
{
    public class OrderItemResponse
    {
        public int ItemId { get; set; }

        public int Quantity { get; set; }
        public int Price { get; set; }

    }
}
