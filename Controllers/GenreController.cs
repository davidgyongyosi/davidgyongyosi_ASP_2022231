using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using davidgyongyosi_ASP_2022231.Data;
using davidgyongyosi_ASP_2022231.Models;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace davidgyongyosi_ASP_2022231.Controllers
{
    public class GenreController : Controller
    {
        private readonly UserManager<SiteUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly ILogger<HomeController> _logger;
        private readonly IGenreRepository _repo;

        public GenreController(UserManager<SiteUser> userManager, RoleManager<IdentityRole> roleManager, ILogger<HomeController> logger, IGenreRepository repo)
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

        [Authorize(Roles = "Admin")]
        public IActionResult Add()
        {
            return View();
        }

        [Authorize(Roles = "Admin"), HttpPost]
        public IActionResult Add(Genre genre)
        {
            _repo.Create(genre);
            return RedirectToAction(nameof(Index));
        }

        [Authorize(Roles = "Admin")]
        public IActionResult Edit(string id)
        {
            var genre = _repo.Find(id);
            return View(genre);
        }

        [Authorize(Roles = "Admin"), HttpPost]
        public IActionResult Edit(Genre genre)
        {
            _repo.Update(genre);
            return RedirectToAction("Index", "Genre");
        }

        [Authorize(Roles = "Admin")]
        public IActionResult Delete(string uid)
        {
            var item = _repo.Find(uid);
            _repo.Delete(item);
            return RedirectToAction("AdminEdit", "Home");
        }

    }
}

