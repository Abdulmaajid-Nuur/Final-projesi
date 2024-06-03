using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using System;
using System.Threading.Tasks;
using WebApplication.Identity;
using WebApplication.Models;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using System.Collections.Generic;
using System.Security.Claims;

namespace WebApplication.Controllers
{

    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private UserManager<User> _userManager;

        

        public AdminController(UserManager<User> userManager)
        {
            _userManager= userManager;
        }

        public IActionResult Index()
        {
 
            return View();
        }

        [HttpGet]
        public IActionResult AddFirma()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddFirma(AccountModel model,IFormFile file)
        {

            var extension = Path.GetExtension(file.FileName);
            var newImageName = Guid.NewGuid().ToString() + extension;
            var location = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/img/", newImageName);

            using (var stream = new FileStream(location, FileMode.Create))
            {
                file.CopyTo(stream);
            }

           

            var user = new User
            {

                FirstName = model.RegisterModel.Name,
                Email = model.RegisterModel.Email,
                UserName = model.RegisterModel.UserName,
                Password = model.RegisterModel.Password,
                ProfilePhoto=newImageName,
                PhoneNumber=model.RegisterModel.Phone,
                UserType=1


            };
            var result = await _userManager.CreateAsync(user, model.RegisterModel.Password);

            if (result.Succeeded)
            {

                await _userManager.AddToRoleAsync(user, "Firma");
                
                return RedirectToAction("firmalar", "Admin");
            }
            ModelState.AddModelError("", "Bilinmeyen Bir Hata Oluştu Tekrar Deneyiniz");
            return View(model);
        }

        public IActionResult EditFirma(string id)
        {
      

            var user = _userManager.Users.Where(i => i.Id == id).FirstOrDefault();

            return View(user);
        }
        [HttpPost]
        public async Task<IActionResult> EditFirma(User user,IFormFile file)
        {
            var user1=_userManager.Users.Where(i=>i.Id== user.Id).FirstOrDefault();


            if(file==null)
            {


                user1.FirstName = user.FirstName;
                user1.UserName = user.UserName;
                user1.Email = user.Email;
                user1.PhoneNumber = user.PhoneNumber;
                if (user.Password != null)
                {
                    // PasswordHasher'ı kullanarak şifreyi hashle
                    user1.PasswordHash = _userManager.PasswordHasher.HashPassword(user1, user.Password);
                }

                var result = await _userManager.UpdateAsync(user1);
                if (result.Succeeded)
                {
                    return Redirect("/admin/editFirma/" + user.Id);

                }
            }
            else
            {
                var extension = Path.GetExtension(file.FileName);
                var newImageName = Guid.NewGuid().ToString() + extension;
                var location = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/img/", newImageName);
                using (var stream = new FileStream(location, FileMode.Create))
                {
                    file.CopyTo(stream);
                }
                user1.FirstName = user.FirstName;
                user1.UserName = user.UserName;
                user1.Email = user.Email;
                user1.PhoneNumber = user.PhoneNumber;
                user1.ProfilePhoto = newImageName;
                if (user.Password != null)
                {
                    // PasswordHasher'ı kullanarak şifreyi hashle
                    user1.PasswordHash = _userManager.PasswordHasher.HashPassword(user1, user.Password);
                }

                var result = await _userManager.UpdateAsync(user1);
                if(result.Succeeded)
                {
                    return Redirect("/admin/editFirma/" + user.Id);

                }

                

            }

            return View(user);


        }

        public IActionResult Firmalar()
        {
           var firmalar= _userManager.Users.Where(i => i.UserType == 1).ToList();

            return View(firmalar);
        }

        public async Task<IActionResult>  DeleteFirma(string id)
        {
            var firma = await _userManager.FindByIdAsync(id);
            if (firma != null)
            {
                await _userManager.DeleteAsync(firma);
                return RedirectToAction("firmalar");
            }
           
            return Redirect("firmalar");
        }
    }
}
