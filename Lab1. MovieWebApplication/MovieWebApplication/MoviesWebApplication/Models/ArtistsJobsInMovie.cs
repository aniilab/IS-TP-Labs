using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MoviesWebApplication
{
    public partial class ArtistsJobsInMovie
    {
        public int Id { get; set; }
        public int MovieId { get; set; }
        public int ArtistId { get; set; }

        [Display(Name = "Роль у створенні")]
        public string Job { get; set; }


        [Display(Name = "Ім'я артиста")]
        public virtual Artist Artist { get; set; }

        [Display(Name = "Назва фільму")]
        public virtual Movie Movie { get; set; }
    }
}
