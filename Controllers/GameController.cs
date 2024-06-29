using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using davidgyongyosi_ASP_2022231.Data;
using davidgyongyosi_ASP_2022231.Helpers;
using davidgyongyosi_ASP_2022231.Models;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace davidgyongyosi_ASP_2022231.Controllers
{
    public class GameController : Controller
    {
        private readonly UserManager<SiteUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly ILogger<HomeController> _logger;
        private readonly IGameRepository _repo;

        public GameController(UserManager<SiteUser> userManager, RoleManager<IdentityRole> roleManager, ILogger<HomeController> logger, IGameRepository repo)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _logger = logger;
            _repo = repo;
        }

        // GET: /<controller>/
        public IActionResult Index()
        {
            return View(_repo.ReadAll());
        }

        [Authorize]
        public IActionResult Add()
        {
            PopulateGenresDropDownList();
            var platfroms = _repo.ListPlatforms();
            ViewBag.Platforms = new MultiSelectList(platfroms, "Uid", "Name");

            return View();
        }

        [Authorize, HttpPost]
        public IActionResult Add(Game game, IFormFile pic, string[] platformIds)
        {
            PopulateGenresDropDownList();
            _repo.Create(game, pic, platformIds);
            return RedirectToAction("Index", "Home");
        }

        [Authorize(Roles = "Admin")]
        public IActionResult Edit(string id)
        {
            var game = _repo.Find(id);
            PopulateGenresDropDownList();
            return View(game);
        }

        [Authorize(Roles = "Admin"), HttpPost]
        public IActionResult Edit(Game game)
        {

            PopulateGenresDropDownList();
            _repo.Update(game);
            return RedirectToAction("Index", "Home");
        }

        [Authorize]
        public async Task<IActionResult> AddToLibrary(string id)
        {
            var game = _repo.Find(id);
            var user = await _userManager.GetUserAsync(this.User);
            _repo.AddToLib(game, user);

            return RedirectToAction("Index", "Home");
        }

        [Authorize]
        public async Task<IActionResult> RemoveFromLibrary(string id)
        {
            var game = _repo.Find(id);
            var user = await _userManager.GetUserAsync(this.User);
            _repo.RemoveFromLib(game, user);

            return RedirectToAction("Index", "Home");
        }

        public async Task<IActionResult> UnSubscribe(string id)
        {
            var game = _repo.Find(id);
            var user = await _userManager.GetUserAsync(this.User);
            _repo.Update(game);
            return RedirectToAction("Index", "Home");
        }

        public IActionResult GetImage(string id)
        {
            var game = _repo.Find(id);
            return new FileContentResult(game.Data, game.ContentType);
        }

        private void PopulateGenresDropDownList()
        {
            var genres = _repo.ListGenres();
            ViewBag.GenreID = new SelectList(genres, "Uid", "Name");
        }

        [Authorize(Roles = "Admin")]
        public IActionResult Delete(string id)
        {
            var item = _repo.Find(id);
            _repo.Delete(item);
            return RedirectToAction("AdminEdit", "Home");
        }

    }
}

