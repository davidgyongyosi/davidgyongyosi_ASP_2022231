using davidgyongyosi_ASP_2022231.Models;

namespace davidgyongyosi_ASP_2022231.Data
{
    public interface IPlatformRepository
    {
        void Create(Platform platform);
        void Update(Platform platform);
        void Delete(Platform platform);

        Platform Find(string id);
        IEnumerable<Platform> ReadAll();
    }
}