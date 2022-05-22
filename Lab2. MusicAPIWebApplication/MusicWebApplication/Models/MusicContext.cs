using Microsoft.EntityFrameworkCore;

namespace MusicWebApplication.Models
{
    public class MusicContext : DbContext
    {
        public virtual DbSet<Song> Songs { get; set; }
        public virtual DbSet<Album> Albums { get; set; }
        public virtual DbSet<Artist> Artists { get; set; }  
        public virtual DbSet<Genre> Genres { get; set; }
        public virtual DbSet<GenresOfSong> GenresOfSongs { get; set; }
        public virtual DbSet<Playlist> Playlists { get; set; }
        public virtual DbSet<SongsInPlaylist> SongsInPlaylist { get; set; }
        public MusicContext(DbContextOptions<MusicContext> options) 
            : base(options)
        {
            Database.EnsureCreated();
        }
    }
}

