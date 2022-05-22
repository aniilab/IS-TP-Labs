using System.ComponentModel.DataAnnotations;

namespace MusicWebApplication.Models
{
    public class SongsInPlaylist
    {

        [Key]
        public int Id { get; set; }
        public int SongId { get; set; }
        public int PlaylistId { get; set; }
        public virtual Song Song { get; set; }
        public virtual Playlist Playlist { get; set; }
    }

}
