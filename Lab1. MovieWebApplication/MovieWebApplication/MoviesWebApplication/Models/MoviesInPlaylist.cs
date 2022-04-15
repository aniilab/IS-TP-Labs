using System;
using System.Collections.Generic;

namespace MoviesWebApplication
{
    public partial class MoviesInPlaylist
    {
        public int Id { get; set; }
        public int MovieId { get; set; }
        public int PlaylistId { get; set; }

        public virtual Movie Movie { get; set; }
        public virtual Playlist Playlist { get; set; }
    }
}
