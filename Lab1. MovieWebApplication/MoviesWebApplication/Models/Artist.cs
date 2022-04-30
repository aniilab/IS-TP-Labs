using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MoviesWebApplication
{
    public partial class Artist
    {
        public Artist()
        {
            ArtistsJobsInMovies = new HashSet<ArtistsJobsInMovie>();
        }

        public int Id { get; set; }

        [Display(Name = "Ім'я артиста")]
        public string Name { get; set; }


        [Display(Name = "Кількість нагород")]
        public int? NumberOfOscars { get; set; }

        public virtual ICollection<ArtistsJobsInMovie> ArtistsJobsInMovies { get; set; }
    }
}
