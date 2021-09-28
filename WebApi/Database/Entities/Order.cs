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
        [Column(TypeName = "nvarchar(80)")]

        [Required]
        public DateTime DateTime { get; set; }

        [ForeignKey ("User.UserId")]
        public int UserId { get; set; }

        public List<Item> Items { get; set; } = new();

        public User User { get; set; }

    }
}