using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Entities
{
    public class Address
    {
        [Key]
        public int AddressId { get; set; }
        [Column(TypeName = "nvarchar(80)")]
        [Required]
        public string Add { get; set; }
        [Required]
        public int Postal { get; set; }
        [Required]
        [Column(TypeName = "nvarchar(40)")]
        public string City { get; set; }

        [ForeignKey ("User.UserId")]
        public int UserId { get; set; }

        //public User User {get; set;}




    }
}