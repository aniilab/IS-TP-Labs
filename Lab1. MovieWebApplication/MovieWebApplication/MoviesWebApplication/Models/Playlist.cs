using System;
using System.Collections.Generic;

namespace MoviesWebApplication
{
    public partial class Playlist
    {
        public Playlist()
        {
            MoviesInPlaylists = new HashSet<MoviesInPlaylist>();
        }

        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public int UserId { get; set; }

        public virtual User User { get; set; } = null!;
        public virtual ICollection<MoviesInPlaylist> MoviesInPlaylists { get; set; }
    }
}
