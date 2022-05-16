using System.ComponentModel.DataAnnotations;

namespace MusicAPIWebApplication.Models
{
    public class Playlist
    {
        public Playlist()
        {
            SongsInPlaylists = new List<SongsInPlaylist>();
        }

        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public virtual ICollection<SongsInPlaylist> SongsInPlaylists { get; set; }
    }
}
