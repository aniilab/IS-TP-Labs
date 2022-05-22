using System.ComponentModel.DataAnnotations;
namespace MusicWebApplication.Models
{
    public class GenresOfSong
    {
        [Key]
        public int Id { get; set; }
        public int SongId { get; set; }
        public int GenreId { get; set; }
        public virtual Song Song { get; set; }
        public virtual Genre Genre { get; set; }
    }
}
