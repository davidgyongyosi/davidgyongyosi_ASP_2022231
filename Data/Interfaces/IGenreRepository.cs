using davidgyongyosi_ASP_2022231.Models;

namespace davidgyongyosi_ASP_2022231.Data
{
    public interface IGenreRepository
    {
        void Create(Genre genre);
        void Update(Genre genre);
        void Delete(Genre genre);

        Genre Find(string id);
        IEnumerable<Genre> ReadAll();
        
    }
}