using System;
using System.Collections.Generic;

namespace MovieWebApplication
{
    public partial class Movie
    {
        public Movie()
        {
            ArtistsJobsInMovies = new HashSet<ArtistsJobsInMovie>();
            MovieInPlaylists = new HashSet<MovieInPlaylist>();
            MoviesGenres = new HashSet<MoviesGenre>();
            Productions = new HashSet<Production>();
        }

        public int Id { get; set; }
        public string MovieName { get; set; } = null!;
        public TimeSpan MovieDuration { get; set; }
        public bool HasOscar { get; set; }
        public int ProductionId { get; set; }

        public virtual Production Production { get; set; } = null!;
        public virtual ICollection<ArtistsJobsInMovie> ArtistsJobsInMovies { get; set; }
        public virtual ICollection<MovieInPlaylist> MovieInPlaylists { get; set; }
        public virtual ICollection<MoviesGenre> MoviesGenres { get; set; }
        public virtual ICollection<Production> Productions { get; set; }
    }
}
