using System;
using System.Collections.Generic;

namespace MovieWebApplication
{
    public partial class User
    {
        public User()
        {
            Playlists = new HashSet<Playlist>();
        }

        public string Id { get; set; } = null!;
        public string UserPassword { get; set; } = null!;
        public string UserEmail { get; set; } = null!;
        public string UserInfo { get; set; } = null!;
        public int? PlaylistId { get; set; }

        public virtual Playlist? Playlist { get; set; }
        public virtual ICollection<Playlist> Playlists { get; set; }
    }
}
