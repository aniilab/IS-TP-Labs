using System.ComponentModel.DataAnnotations;

namespace MusicAPIWebApplication.Models
{
    public class Album
    {
        public Album()
        {
            Songs = new List<Song>();
        }

        public int Id { get; set; }
        [Required(ErrorMessage ="Поле не повинно бути порожнім")]
        [Display(Name ="Назва альбому")]
        public string Name { get; set; }
        [Display(Name = "Виконавець")]
        public int ArtistId { get; set; }

        [Required(ErrorMessage = "Поле не повинно бути порожнім")]
        [Display(Name = "Рік випуску")]
        public int ProdYear { get; set; }

        public virtual ICollection<Song> Songs { get; set; }
        public virtual Artist Artist { get; set; }
    }
}
