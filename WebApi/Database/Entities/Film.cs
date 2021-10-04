using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Database.Entities
{
    public class Film
    {
        [Key]
        public int FilmId { get; set; }

        [Required]
        [Column(TypeName = "nvarchar(500)")]
        public string FilmName { get; set; }

        [Required]
        [Column(TypeName = "nvarchar(15)")]
        public string ReleaseDate { get; set; }

        [Required]
        [Column(TypeName = "smallInt")]
        public Int16 RuntimeInMin { get; set; }

        [Required]
        [Column(TypeName = "nvarchar(max)")]
        public string Description { get; set; }

        [Required]
        [Column(TypeName = "decimal(6,2)")]
        public decimal Price { get; set; }

        [Required]
        [Column(TypeName = "smallInt")]
        public int Stock { get; set; }

        [Required]
        [Column(TypeName = "nvarchar(4000)")]
        public string Image { get; set; }


        [ForeignKey("Genre.GenreId")]
        public int GenreId { get; set; }

        public Genre Genre { get; set; }
    }
}
