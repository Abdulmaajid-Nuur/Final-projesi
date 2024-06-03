
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using WebApplication.Identity;
using WebApplication.Models;

namespace WebApplication.Controllers
{

    public class ExpeditionController : Controller
    {
        private UserManager<User> _userManager;

        public ExpeditionController(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        public IActionResult AddExpedition()
        {
            using (var context=new Context())
            {
                var cities = context.Cities.ToList();
                var districts = context.Districts.ToList();

                ViewBag.cities = cities;
                ViewBag.districts = districts;
            }

            return View();
        }
        [HttpPost]
        public IActionResult AddExpedition(Expedition expedition)
        {
            using (var db=new Context())

            {
                Expedition exp = new Expedition()
                {
                    Kalkis=expedition.Kalkis,
                    Varis=expedition.Varis,
                    KalkisTime=expedition.KalkisTime,
                    VarisTime=expedition.VarisTime,
                    Price=expedition.Price,
                    Plus=expedition.Plus,
                    UserId=User.Identity.Name,
                    Status=1,
                    Created_at=DateTime.Now,
                    Updated_at=DateTime.Now
                };

                db.Expeditions.Add(exp);
                db.SaveChanges();

                return RedirectToAction("expedition/addExpedition");
            }
        }

        public IActionResult GetDistrict(int cityId)
        {
            try
            {
                using (var db = new Context())
                {
                    var districts = db.Districts.Where(i => i.CityId == cityId).ToList();
                    return Json(districts);
                }
            }
            catch (Exception ex)
            {
                // Hata yakalama
                return BadRequest("İlçe bilgileri alınamadı: " + ex.Message);
            }
        }

        [HttpPost]
        public IActionResult Expeditions(string kalkis, string varis,DateTime tarih)
        {
            
            using (var db = new Context())
            {
                var seferler = db.Expeditions.Where(i => i.Kalkis == kalkis && i.Varis == varis && i.KalkisTime.Year>=tarih.Year && i.KalkisTime.Month>=tarih.Month && i.KalkisTime.Day>=tarih.Day && i.KalkisTime.Year >= DateTime.Now.Year && i.KalkisTime.Month >= DateTime.Now.Month && i.KalkisTime.Day >= DateTime.Now.Day && i.KalkisTime.Hour>= DateTime.Now.Hour).ToList();
                var biletler = db.Tickets.ToList();
                var users = _userManager.Users.ToList();
               

                ViewBag.userId = _userManager.GetUserId(User);
                return View((seferler,biletler,users));
            }


        }
    }
}
