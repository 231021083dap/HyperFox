using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Entities
{
    public class User
    {  
        [Key]
        public int UserId { get; set; }
        [Column(TypeName ="nvarchar(20)")]
        [Required]
        public string UserName { get; set; }
        [Column (TypeName ="nvarchar(40)")]
        [Required]
        public string Email { get; set; }

        [Column(TypeName = "nvarchar(40)")]
        [Required]
        public string Password { get; set; }
        [Column(TypeName = "nvarchar(5)")]
        public  Auth.Role Admin{ get; set; }

        public Address Addresses { get; set; } 
    }
}