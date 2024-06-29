using System.Diagnostics;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using davidgyongyosi_ASP_2022231.Data;
using davidgyongyosi_ASP_2022231.Helpers;
using davidgyongyosi_ASP_2022231.Models;

namespace davidgyongyosi_ASP_2022231.Controllers;

public class HomeController : Controller
{
    private readonly UserManager<SiteUser> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly ILogger<HomeController> _logger;
    private readonly ApplicationDbContext _db;

    public HomeController(UserManager<SiteUser> userManager, RoleManager<IdentityRole> roleManager, ILogger<HomeController> logger, ApplicationDbContext db)
    {
        _userManager = userManager;
        _roleManager = roleManager;
        _logger = logger;
        _db = db;
    }

    public IActionResult Index(int? pageNumber)
    {
        var games = from g in _db.Games select g;
        int pageSize = 12;

        return View(PagedList<Game>.CreateList(games, pageNumber ?? 1, pageSize));
    }

    public IActionResult Delete(string uid)
    {
        var item = _db.Games.FirstOrDefault(t => t.Uid == uid);
        if (item != null)
        {
            _db.Games.Remove(item);
            _db.SaveChanges();
        }
        return RedirectToAction(nameof(Index));
    }

    public IActionResult GetImage(string userid)
    {
        var user = _userManager.Users.FirstOrDefault(t => t.Id == userid);
        return new FileContentResult(user.PictureData, user.PictureContentType);
    }

    [Authorize]
    public async Task<IActionResult> Privacy()
    {
        var user = await _userManager.GetUserAsync(this.User);
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }

    //-------------------------
    // Delegate Admin 
    //-------------------------
    /*
    public async Task<IActionResult> DelegateAdmin()
    {
        var user = await _userManager.GetUserAsync(this.User);
        var role = new IdentityRole()
        {
            Name = "Admin"
        };
        if (!await _roleManager.RoleExistsAsync("Admin"))
        {
            await _roleManager.CreateAsync(role);
        }
        await _userManager.AddToRoleAsync(user, "Admin");

        return RedirectToAction(nameof(Index));
    }
    */

    [Authorize(Roles = "Admin")]
    public IActionResult AdminEdit()
    {
        return View(_db.Games);
    }

    [Authorize(Roles = "Admin")]
    public IActionResult Users()
    {
        return View(_userManager.Users);
    }

    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> RemoveAdmin(string uid)
    {
        var user = _userManager.Users.FirstOrDefault(t => t.Id == uid);
        await _userManager.RemoveFromRoleAsync(user, "Admin");
        return RedirectToAction(nameof(Users));
    }

    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> GrantAdmin(string uid)
    {
        var user = _userManager.Users.FirstOrDefault(t => t.Id == uid);
        await _userManager.AddToRoleAsync(user, "Admin");
        return RedirectToAction(nameof(Users));
    }

    [Authorize(Roles = "Admin")]
    public IActionResult DeleteUser(string uid)
    {
        var user = _db.Users.FirstOrDefault(t => t.Id == uid);
        if (user != null)
        {
            _db.Users.Remove(user);
            _db.SaveChanges();
        }
        return RedirectToAction(nameof(Index));
    }
}

