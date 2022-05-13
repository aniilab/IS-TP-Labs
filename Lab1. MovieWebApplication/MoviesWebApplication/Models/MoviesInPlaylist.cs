using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MoviesWebApplication
{
    public partial class MoviesInPlaylist
    {
        public int Id { get; set; }

        [Display(Name = "Назва фільму")]
        public int MovieId { get; set; }

        [Display(Name = "Назва підбірки")]
        public int PlaylistId { get; set; }


        [Display(Name = "Фільм")]
        public virtual Movie? Movie { get; set; }

        [Display(Name = "Підбірка")]
        public virtual Playlist? Playlist { get; set; }
    }
}
