using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MoviesWebApplication
{
    public partial class MoviesGenre
    {
        public int Id { get; set; }

        [Display(Name = "Фільм")]
        public int MovieId { get; set; }

        [Display(Name = "Жанр")]
        public int GenreId { get; set; }

        public virtual Genre? Genre { get; set; }

        [Display(Name = "Фільми")]
        public virtual Movie? Movie { get; set; }
    }
}
