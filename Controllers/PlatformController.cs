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
    public class PlatformController : Controller
    {
        private readonly UserManager<SiteUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly ILogger<HomeController> _logger;
        private readonly IPlatformRepository _repo;

        public PlatformController(UserManager<SiteUser> userManager, RoleManager<IdentityRole> roleManager, ILogger<HomeController> logger, IPlatformRepository repo)
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
        public IActionResult Add(Platform platform)
        {
            _repo.Create(platform);
            return RedirectToAction(nameof(Index));
        }

        [Authorize(Roles = "Admin")]
        public IActionResult Edit(string id)
        {
            var platform = _repo.Find(id);
            return View(platform);
        }

        [Authorize(Roles = "Admin"), HttpPost]
        public IActionResult Edit(Platform platform)
        {
            _repo.Update(platform);
            return RedirectToAction("Index", "Platform");
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