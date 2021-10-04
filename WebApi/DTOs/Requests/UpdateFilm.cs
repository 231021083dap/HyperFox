using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.DTOs.Requests
{
    public class UpdateFilm
    {
        [Required]
        [StringLength(500, ErrorMessage = "FilmName must be less than 500 chars")]
        [MinLength(1, ErrorMessage = "FilmName must contain atleast 1 char")]
        public string FilmName { get; set; }

        [Required]
        [StringLength(15, ErrorMessage = "ReleaseDate must be less than 15 chars")]
        public string ReleaseDate { get; set; }

        [Required]
        [Range(1, Int16.MaxValue, ErrorMessage = "RuntimeInMin must contain atleast 1 char")]
        public Int16 RuntimeInMin { get; set; }

        [Required]
        [MinLength(1, ErrorMessage = "Description must contain atleast 1 char")]
        public string Description { get; set; }

        [Required]
        [Range(0, 9999.99, ErrorMessage = "Price must contain between 1 - 10000 chars")]
        public decimal Price { get; set; }

        [Required]
        [Range(1, Int16.MaxValue, ErrorMessage = "Stock must contain atleast 1 char")]
        public int Stock { get; set; }

        [Required]
        [StringLength(5000, ErrorMessage = "Image must be less than 5000 chars")]
        [MinLength(5, ErrorMessage = "Image must contain atleast 5 char")]
        public string Image { get; set; }

        [Required]
        public int GenreId { get; set; }
    }
}
