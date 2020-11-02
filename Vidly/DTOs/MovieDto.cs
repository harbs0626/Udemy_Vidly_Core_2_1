using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Vidly.DTOs
{
    public class MovieDto
    {
        [Key]
        public int Id { get; set; }

        [StringLength(500)]
        public string Name { get; set; }

        public DateTime? ReleaseDate { get; set; }

        public int? NumberInStock { get; set; }

        public int? GenreId { get; set; }

    }
}
