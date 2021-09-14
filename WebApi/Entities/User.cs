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
        [Key]get;
        public int UserId { get; set; }
        [Column(TypeName ="nvarchar(20)")]
        [Required]
        public string UserName { get; set; }
        [Column (TypeName ="nvarchar(40)")]
        [Required]
        public string Email { get; set; }


    }
}