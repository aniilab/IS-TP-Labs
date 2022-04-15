using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MoviesWebApplication
{
    public partial class Genre
    {
        public Genre()
        {
            MoviesGenres = new HashSet<MoviesGenre>();
        }

        public int Id { get; set; }

        [Display(Name = "Жанр")]
        public string Name { get; set; } = null!;

        public virtual ICollection<MoviesGenre> MoviesGenres { get; set; }
    }
}
