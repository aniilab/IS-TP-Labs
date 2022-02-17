using System;
using System.Collections.Generic;

namespace MovieWebApplication
{
    public partial class MovieInPlaylist
    {
        public int Id { get; set; }
        public int? MovieId { get; set; }
        public int? PlaylistId { get; set; }

        public virtual Movie? Movie { get; set; }
        public virtual Playlist? Playlist { get; set; }
    }
}
