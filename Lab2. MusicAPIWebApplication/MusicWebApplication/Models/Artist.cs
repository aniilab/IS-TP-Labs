using System.ComponentModel.DataAnnotations;
namespace MusicWebApplication.Models
{
    public class Artist
    {
        public Artist()
        {
            Albums = new List<Album>();
        }
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Поле не повинно бути порожнім")]
        [Display(Name = "Ім'я виконавця")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Поле не повинно бути порожнім")]
        [Display(Name = "Країна")]
        public string Country { get; set; }

        public virtual ICollection<Album> Albums { get; set; }
    }

}

