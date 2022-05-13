using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MoviesWebApplication
{
    public partial class Playlist
    {
        public Playlist()
        {
            MoviesInPlaylists = new HashSet<MoviesInPlaylist>();
        }

        public int Id { get; set; }

        [Display(Name = "Назва")]
        public string Name { get; set; }

        [Display(Name = "Username створювача")]
        public string UserName { get; set; }

        public virtual ICollection<MoviesInPlaylist>? MoviesInPlaylists { get; set; }
    }
}
