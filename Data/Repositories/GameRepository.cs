using System;
using System.Diagnostics;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using davidgyongyosi_ASP_2022231.Models;

namespace davidgyongyosi_ASP_2022231.Data
{
    public class GameRepository : IGameRepository
    {
        private readonly ApplicationDbContext _db;

        public GameRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public void Create(Game game, IFormFile pic, string[] platformIds)
        {
            var old = _db.Games.FirstOrDefault(t => t.Name == game.Name);
            using (var stream = pic.OpenReadStream())
            {
                byte[] buffer = new byte[pic.Length];
                stream.Read(buffer, 0, (int)pic.Length);
                game.Data = buffer;
                game.ContentType = pic.ContentType;
            }

            foreach (var ids in platformIds)
            {
                var p = _db.Platforms.FirstOrDefault(t => t.Uid == ids);
                game.Platforms.Add(p);
            }

            if (old == null)
            {
                _db.Games.Add(game);
                _db.SaveChanges();
            }
            else
            {
                throw new ArgumentException("Game is already in the library");
            }
        }

        public void AddToLib(Game game, SiteUser user)
        {
            game.Owners.Add(user);
            _db.SaveChanges();
        }

        public void RemoveFromLib(Game game, SiteUser user)
        {
            game.Owners.Remove(user);
            _db.SaveChanges();
        }

        public void Update(Game game)
        {
            var old = Find(game.Uid);

            old.Name = game.Name;
            old.releaseDate = game.releaseDate;
            old.Price = game.Price;
            old.GenreId = game.GenreId;
            _db.SaveChanges();
        }

        public IEnumerable<Game> ReadAll()
        {
            return _db.Games;
        }

        public Game Find(string id)
        {
            var game = _db.Games.FirstOrDefault(t => t.Uid == id);
            if (game == null)
                throw new ArgumentException("Can't find that");
            else
                return game;
        }

        public List<Genre> ListGenres()
        {
            return _db.Genres.ToList();
        }

        public List<Platform> ListPlatforms()
        {
            return _db.Platforms.ToList();
        }

        public void Delete(Game game)
        {
            _db.Games.Remove(game);
            _db.SaveChanges();
        }
    }
}

