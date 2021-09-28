using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
namespace WebApi.DTOs.Responses
{
    public class OrderResponse
    {
        public int OrderId { get; set; }

        public int UserId { get; set; }

        public DateTime DateTime { get; set; }

        public List<OrderItemResponse>Items{get; set;}
        public OrderUserResponse User { get; set; }
    }
}