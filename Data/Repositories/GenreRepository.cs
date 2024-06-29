using System;
using davidgyongyosi_ASP_2022231.Models;

namespace davidgyongyosi_ASP_2022231.Data
{
    public class GenreRepository : IGenreRepository
    {
        private readonly ApplicationDbContext _db;

        public GenreRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public void Create(Genre genre)
        {
            var old = _db.Genres.FirstOrDefault(t => t.Name == genre.Name);
            if (old == null)
            {
                _db.Genres.Add(genre);
                _db.SaveChanges();
            }
            else
            {
                throw new ArgumentException("Genre already exists");
            }
        }

        public IEnumerable<Genre> ReadAll()
        {
            return _db.Genres;
        }

        public Genre Find(string id)
        {
            var genre = _db.Genres.FirstOrDefault(t => t.Uid == id);

            if (genre == null)
                throw new ArgumentException("Can't find that");
            else
                return genre;
        }

        public void Update(Genre genre)
        {
            var old = Find(genre.Uid);
            old.Name = genre.Name;
            _db.SaveChanges();
        }

        public void Delete(Genre genre)
        {
            _db.Genres.Remove(genre);
            _db.SaveChanges();
        }
    }
}

