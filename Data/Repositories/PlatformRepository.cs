using System;
using davidgyongyosi_ASP_2022231.Models;

namespace davidgyongyosi_ASP_2022231.Data
{
    public class PlatformRepository : IPlatformRepository
    {
        private readonly ApplicationDbContext _db;

        public PlatformRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public void Create(Platform platform)
        {
            var old = _db.Platforms.FirstOrDefault(t => t.Name == platform.Name);
            if (old == null)
            {
                _db.Platforms.Add(platform);
                _db.SaveChanges();
            }
            else
            {
                throw new ArgumentException("Platform already exists");
            }
        }

        public IEnumerable<Platform> ReadAll()
        {
            return _db.Platforms;
        }

        public Platform Find(string id)
        {
            var platform = _db.Platforms.FirstOrDefault(t => t.Uid == id);

            if (platform == null)
                throw new ArgumentException("Can't find that");
            else
                return platform;
        }

        public void Update(Platform platform)
        {
            var old = Find(platform.Uid);
            old.Name = platform.Name;
            _db.SaveChanges();
        }

        public void Delete(Platform platform)
        {
            _db.Platforms.Remove(platform);
            _db.SaveChanges();
        }
    }
}

