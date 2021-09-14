using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Database.Entities
{
    public class Genre
    {
        [Key]
        public int GenreId { get; set; }

        [Column(TypeName = "nvarchar(32)")]
        public string GenreName { get; set; }
    }
}
