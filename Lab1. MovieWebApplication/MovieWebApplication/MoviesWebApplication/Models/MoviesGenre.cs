using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MoviesWebApplication
{
    public partial class MoviesGenre
    {
        public int Id { get; set; }
        public int MovieId { get; set; }
        public int GenreId { get; set; }

        public virtual Genre Genre { get; set; }

        [Display(Name = "Фільми")]
        public virtual Movie Movie { get; set; }
    }
}
