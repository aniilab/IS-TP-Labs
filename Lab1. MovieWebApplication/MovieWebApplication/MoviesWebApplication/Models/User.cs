using System;
using System.Collections.Generic;

namespace MoviesWebApplication
{
    public partial class User
    {
        public User()
        {
            Playlists = new HashSet<Playlist>();
        }

        public int Id { get; set; }
        public string Login { get; set; } = null!;
        public string Password { get; set; } = null!;

        public virtual ICollection<Playlist> Playlists { get; set; }
    }
}
