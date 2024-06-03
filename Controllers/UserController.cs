using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Scaffolding.Metadata;
using System.IO;
using System;
using System.Linq;
using System.Threading.Tasks;
using WebApplication.Identity;
using WebApplication.Models;

namespace WebApplication.Controllers
{
    [Authorize]
    public class UserController : Controller
    {
        private UserManager<User> _userManager;

        public UserController(UserManager<User> userManager)
        {
            _userManager = userManager;
        }
    

        public IActionResult Profile() {
            var userId = _userManager.GetUserId(User);
            var user = _userManager.Users.Where(i=>i.Id==userId).FirstOrDefault();
             return View(user);
        }
        [HttpPost]
        public async Task<IActionResult> Profile(User user,IFormFile file)
        {

                var userId = _userManager.GetUserId(User);
                var userss = _userManager.Users.Where(i => i.Id == userId).FirstOrDefault();
                userss.UserName=user.UserName;
                userss.Email=user.Email;
                userss.PhoneNumber=user.PhoneNumber;
                userss.Password=user.Password;
                userss.FirstName=user.FirstName; 
                userss.LastName=user.LastName;
            if (file != null && file.Length > 0)
            {
                var extension = Path.GetExtension(file.FileName);
                var newFileName = Guid.NewGuid() + extension;
                var location = "";

                location = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/img/", newFileName);



                var stream = new FileStream(location, FileMode.Create);
                file.CopyTo(stream);

                userss.ProfilePhoto = newFileName;
            }
               

            var result=   await _userManager.UpdateAsync(userss);
            if (result.Succeeded)
            {
                return RedirectToAction("Profile", "User");

            }
            else
            {
                return View();
            }

        }

        public JsonResult GetUserPP()
        {
            using (var db = new Context())
            {
                var user = _userManager.Users.Where(i => i.Id == _userManager.GetUserId(User)).FirstOrDefault();
                return Json(user.ProfilePhoto);

            }
        }
    }
}
