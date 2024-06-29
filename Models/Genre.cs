using System.ComponentModel.DataAnnotations;

namespace davidgyongyosi_ASP_2022231.Models
{
    public class Genre
    {
        [Key]
        public string Uid { get; set; }

        [Required, StringLength(100), Display(Name = "Genre Name")]
        public string Name { get; set; } = String.Empty;

        public virtual ICollection<Game> Games { get; set; }

        public Genre()
        {
            Uid = Guid.NewGuid().ToString();
            this.Games = new HashSet<Game>();
        }
    }
}