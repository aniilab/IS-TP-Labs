using System;
using System.Collections.Generic;

namespace MovieWebApplication
{
    public partial class MoviesGenre
    {
        public int Id { get; set; }
        public int? MovieId { get; set; }
        public int GenreId { get; set; }

        public virtual Genre Genre { get; set; } = null!;
        public virtual Movie? Movie { get; set; }
    }
}
