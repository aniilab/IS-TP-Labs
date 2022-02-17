using System;
using System.Collections.Generic;

namespace MovieWebApplication
{
    public partial class Artist
    {
        public Artist()
        {
            ArtistsJobsInMovies = new HashSet<ArtistsJobsInMovie>();
        }

        public int Id { get; set; }
        public string ArtistName { get; set; } = null!;
        public int? Oscars { get; set; }

        public virtual ICollection<ArtistsJobsInMovie> ArtistsJobsInMovies { get; set; }
    }
}
