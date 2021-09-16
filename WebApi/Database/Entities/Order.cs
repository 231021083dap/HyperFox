using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Entities
{
    public class Order
    {
        [Key]
        public int OrderId { get; set; }

        [ForeignKey("User.UserId")]
        public int UserId { get; set; }

        [ForeignKey("Item.ItemId")]
        public int ItemId { get; set; }
        [Column(TypeName ="nvarchar(40)")]
        public string DateTime { get; set; }


        //public User User {get; set;}




    }
}