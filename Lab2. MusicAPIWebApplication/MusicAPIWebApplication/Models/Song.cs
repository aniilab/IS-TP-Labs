﻿using System.ComponentModel.DataAnnotations;

namespace MusicAPIWebApplication.Models
{
    public class Song
    {
        public Song()
        {
            GenresOfSong = new List<GenresOfSong>();
        }
        public int Id { get; set; }

        [Required(ErrorMessage = "Поле не повинно бути порожнім")]
        [Display(Name = "Назва пісні")]
        public string Name { get; set; }
        [Display(Name = "Виконавець")]
        public int ArtistId { get; set; }
        [Display(Name = "Альбом")]
        public int AlbumId { get; set; }
        [Required(ErrorMessage = "Поле не повинно бути порожнім")]
        [Display(Name = "Тривалість (у хв)")]
        public int Duration { get; set; }


        public virtual ICollection<GenresOfSong> GenresOfSong { get; set; }
        public virtual Artist Artist { get; set; }
    }
}
