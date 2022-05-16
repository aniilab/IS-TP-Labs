using System.ComponentModel.DataAnnotations;

namespace MusicAPIWebApplication.Models
{
    public class Genre
    {
        public Genre()
        {
            GenresOfSong = new List<GenresOfSong>();
        }
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Поле не повинно бути порожнім")]
        [Display(Name = "Назва жанру")]
        public string Name { get; set; }

        public virtual ICollection<GenresOfSong> GenresOfSong { get; set; }

    }
}
