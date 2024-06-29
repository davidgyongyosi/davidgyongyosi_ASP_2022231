using Microsoft.AspNetCore.Identity;

namespace davidgyongyosi_ASP_2022231.Models
{
    public class SiteUser : IdentityUser
    {
        public string FirstName { get; set; } = String.Empty;
        public string LastName { get; set; } = String.Empty;
        public string PictureContentType { get; set; } = String.Empty;
        public byte[]? PictureData { get; set; }

        public virtual ICollection<Game> Games { get; set; }

        public SiteUser()
        {
            this.Games = new HashSet<Game>();
        }
    }
}
