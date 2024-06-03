using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebApplication.Models;
using System;
using System.Security.Principal;
using System.Threading.Tasks;
using WebApplication.Identity;
using System.Linq;

namespace ParselyaUI.Controllers
{
    public class AccountController : Controller
    {
        private UserManager<User> _userManager;
        private SignInManager<User> _signInManager;

        public AccountController(UserManager<User> userManager, SignInManager<User> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        //[HttpGet]
        //public IActionResult Login()
        //{
        //	return View();
        //}

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(AccountModel model)
        {
            var users =  _userManager.Users.Count();
          

            var user = await _userManager.FindByNameAsync(model.LoginModel.UserName);
            if (user == null)
            {
                ModelState.AddModelError("", "Kullanıcı Bulunamadı");
                return RedirectToAction("index","home");
            }

            var result = await _signInManager.PasswordSignInAsync(user, model.LoginModel.Password, true, false);

            if (result.Succeeded)
            {
                if (users == 1)
                {
                    await _userManager.AddToRoleAsync(user,"Admin");
                }
                return Redirect("/home/index");
            }

            return View(model);
        }

        //[HttpGet]
        //public IActionResult Register()
        //{
        //	return View();
        //}

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(AccountModel model)
        {

            var user = new User
            {

                //FirstName = model.Name,
                Email = model.RegisterModel.Email,
                UserName = model.RegisterModel.UserName,
                Password = model.RegisterModel.Password,


            };
            var result = await _userManager.CreateAsync(user, model.RegisterModel.Password);

            if (result.Succeeded)
            {
                return RedirectToAction("index", "home");
            }
            ModelState.AddModelError("", "Bilinmeyen Bir Hata Oluştu Tekrar Deneyiniz");
            return View(model);
        }

        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();

            return RedirectToAction("index", "home");
        }

        public IActionResult Accessdenied()
        {
            return View();
        }


    }
}
