using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.DTOs.Responses
{
    public class AddressResponse
    {
        public int AddressId { get; set; }
        public string StreetName { get; set; }
        public int Postal { get; set; }
        public string City { get; set; }

        public int UserId { get; set; } 
        public AddressUserResponse User { get; set; }
    }
}
