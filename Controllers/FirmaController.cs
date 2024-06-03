using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Data;
using System.Linq;
using WebApplication.Identity;
using WebApplication.Models;

namespace WebApplication.Controllers
{
    [Authorize(Roles = "Firma")]
    public class FirmaController : Controller
    {
        private UserManager<User> _userManager;

        public FirmaController(UserManager<User> userManager)
        {
            _userManager = userManager;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult GetSefers()
        {
            using (var db = new Context())
            {
                var sefers = db.Expeditions.Where(i => i.UserId == _userManager.GetUserId(User) && i.KalkisTime >= DateTime.Now).ToList();
                var biletler = db.Tickets.ToList();



                ViewBag.userId = _userManager.GetUserId(User);
                return View((sefers, biletler));

            }

        }

        [HttpPost]
        public IActionResult UpdateTicket(Ticket model)
        {
            using (var db = new Context())
            {
                var ticket = db.Tickets.Where(i => i.Id == model.Id).FirstOrDefault();

                if (ticket != null)
                {
                    ticket.Name = model.Name;
                    ticket.SurName = model.SurName;
                    ticket.Email = model.Email;
                    ticket.TelNo = model.TelNo;
                    ticket.TcNo = model.TcNo;
                    ticket.Gender = model.Gender;

                    db.Tickets.Update(ticket);
                    db.SaveChanges();

                }
                return Redirect("/firma/getsefers");

            }
        }

        public IActionResult DeleteTicket(int id)
        {
            using (var db = new Context())
            {
                var ticket = db.Tickets.Where(i => i.Id == id).FirstOrDefault();

                db.Tickets.Remove(ticket);
                db.SaveChanges();

                return Redirect("/firma/getSefers");
            }
        }


        [HttpPost]
        public IActionResult TicketBuy(Ticket ticket)
        {
            using (var db = new Context())
            {
                Ticket ticket1 = new Ticket()
                {
                    Name = ticket.Name,
                    SurName = ticket.SurName,
                    TelNo = ticket.TelNo,
                    TcNo = ticket.TcNo,
                    Description = ticket.Description,
                    Title = ticket.Title,
                    Price = ticket.Price,
                    SeatNo = ticket.SeatNo,
                    CardName = ticket.CardName,
                    CardNumber = ticket.CardNumber,
                    Expeditions = ticket.Expeditions,
                    CVV = ticket.CVV,
                    ExpreditionMonth = ticket.ExpreditionMonth,
                    ExpreditionYear = ticket.ExpreditionYear,
                    ExpeditionId = ticket.ExpeditionId,
                    KullaniciID = ticket.KullaniciID,
                    Email = ticket.Email,
                    Gender = ticket.Gender,
                };
                db.Tickets.Add(ticket1);
                db.SaveChanges();
                return RedirectToAction("GetSefers", "Firma");
            }

        }

        public IActionResult GetSefersLast()
        {
            using (var db = new Context())
            {
                var sefers = db.Expeditions.Where(i => i.UserId == _userManager.GetUserId(User) && i.KalkisTime<DateTime.Now).ToList();
                var biletler = db.Tickets.ToList();



                ViewBag.userId = _userManager.GetUserId(User);
                return View((sefers, biletler));

            }
        }


        public IActionResult AddExpedition()
        {
            using (var context = new Context())
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
            using (var db = new Context())

            {
                Expedition exp = new Expedition()
                {
                    Kalkis = expedition.Kalkis,
                    Varis = expedition.Varis,
                    KalkisTime = expedition.KalkisTime,
                    VarisTime = expedition.VarisTime,
                    Price = expedition.Price,
                    Plus = expedition.Plus,
                    UserId = _userManager.GetUserId(User),
                    Status = 1,
                    Created_at = DateTime.Now,
                    Updated_at = DateTime.Now
                };

                db.Expeditions.Add(exp);
                db.SaveChanges();

                return Redirect("/firma/getsefers");
            }
        }

        public JsonResult GetFirmaPP()
        {
            using (var db=new Context())
            {
                var user = _userManager.Users.Where(i=>i.Id== _userManager.GetUserId(User)).FirstOrDefault();
                return Json(user.ProfilePhoto);

            }
        }

        public IActionResult DeleteExpedition(int id)
        {
            using (var db=new Context())
            {
                var expedition = db.Expeditions.Where(i => i.Id == id).FirstOrDefault();
                db.Expeditions.Remove(expedition);
                db.SaveChanges();
                return RedirectToAction("getsefers");
            }
        }
    }
}
