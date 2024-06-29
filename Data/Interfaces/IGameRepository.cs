using davidgyongyosi_ASP_2022231.Models;

namespace davidgyongyosi_ASP_2022231.Data
{
    public interface IGameRepository
    {
        void Create(Game game, IFormFile pic, string[] platformIds);
        public void Update(Game game);
        public void Delete(Game game);

        void AddToLib(Game game, SiteUser user);
        void RemoveFromLib(Game game, SiteUser user);

        IEnumerable<Game> ReadAll();
        public Game Find(string id);
        public List<Genre> ListGenres();
        public List<Platform> ListPlatforms();


    }
}