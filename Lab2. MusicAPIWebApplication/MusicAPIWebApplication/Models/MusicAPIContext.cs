using Microsoft.EntityFrameworkCore;

namespace MusicAPIWebApplication.Models
{
    public class MusicAPIContext : DbContext
    {
        public virtual DbSet<Song> Songs { get; set; }
        public virtual DbSet<Album> Albums { get; set; }
        public virtual DbSet<Artist> Artists { get; set; }  
        public virtual DbSet<Genre> Genres { get; set; }
        public virtual DbSet<GenresOfSong> GenresOfSongs { get; set; }
        public MusicAPIContext(DbContextOptions<MusicAPIContext> options) : base(options)
        {
            Database.EnsureCreated();
        }
    }
}
