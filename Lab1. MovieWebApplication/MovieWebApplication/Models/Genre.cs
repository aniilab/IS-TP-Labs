using System;
using System.Collections.Generic;

namespace MovieWebApplication
{
    public partial class Genre
    {
        public Genre()
        {
            MoviesGenres = new HashSet<MoviesGenre>();
        }

        public int Id { get; set; }
        public string GenreName { get; set; } = null!;

        public virtual ICollection<MoviesGenre> MoviesGenres { get; set; }
    }
}
