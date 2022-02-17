using System;
using System.Collections.Generic;

namespace MovieWebApplication
{
    public partial class ArtistsJobsInMovie
    {
        public int Id { get; set; }
        public int? MovieId { get; set; }
        public int ArtistId { get; set; }
        public string? Job { get; set; }

        public virtual Artist Artist { get; set; } = null!;
        public virtual Movie? Movie { get; set; }
    }
}
