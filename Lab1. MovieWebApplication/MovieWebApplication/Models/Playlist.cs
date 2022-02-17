using System;
using System.Collections.Generic;

namespace MovieWebApplication
{
    public partial class Playlist
    {
        public Playlist()
        {
            MovieInPlaylists = new HashSet<MovieInPlaylist>();
            Users = new HashSet<User>();
        }

        public int Id { get; set; }
        public string PlaylistName { get; set; } = null!;
        public string UserId { get; set; } = null!;

        public virtual User User { get; set; } = null!;
        public virtual ICollection<MovieInPlaylist> MovieInPlaylists { get; set; }
        public virtual ICollection<User> Users { get; set; }
    }
}
