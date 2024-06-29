using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace davidgyongyosi_ASP_2022231.Models
{
    public class Game
    {
        [Key]
        public string Uid { get; set; }

        [Required, StringLength(100), Display(Name = "Game Name")]
        public string Name { get; set; }

        [Required, Display(Name = "Release Year")]
        public string releaseDate { get; set; }

        [Required, Display(Name = "Price(USD)")]
        public double Price { get; set; }

        [Display(Name = "Genre")]
        public string GenreId { get; set; }

        public virtual Genre? Genre { get; set; }

        [Display(Name = "Platfoms")]
        public virtual ICollection<Platform> Platforms{ get; set; }

        public virtual ICollection<SiteUser> Owners { get; set; }

        public string ContentType { get; set; }
        public byte[]? Data { get; set; } 

        public Game()
        {
            Uid = Guid.NewGuid().ToString();
            this.Platforms = new HashSet<Platform>();
            this.Owners = new HashSet<SiteUser>();
        }
    }
}

