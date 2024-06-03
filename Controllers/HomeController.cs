using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using WebApplication.Identity;
using WebApplication.Models;

namespace WebApplication.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private UserManager<User> _userManager;
        private RoleManager<IdentityRole> _role;
        public HomeController(ILogger<HomeController> logger, UserManager<User> userManager, RoleManager<IdentityRole> role)
        {
            _logger = logger;
            _role = role;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            using (var db = new Context())
            {
                if (_role.Roles.Count()==0)
                {
                    var rol = await _role.CreateAsync(new IdentityRole("Admin"));
                    var rol2 = await _role.CreateAsync(new IdentityRole("Firma"));
                }
                var cities = db.Cities.ToList();
                
                ViewBag.Cities = cities;
            }

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
