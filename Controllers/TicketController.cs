using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using WebApplication.Identity;
using WebApplication.Models;

namespace WebApplication.Controllers
{
    public class TicketController : Controller
    {
        private UserManager<User> _userManager;
        public TicketController(UserManager<User> userManager)
        {
                _userManager = userManager;
        }
        public IActionResult MyTravels()
        {
            using(var db=new Context())
            {
               var userId= _userManager.GetUserId(User);
                var seferlerim = db.Tickets.Where(i=>i.UyeId==userId).ToList();
                var users = _userManager.Users.ToList();

                return View((seferlerim,users));
            }
           
        }

        [HttpPost]
        public IActionResult TicketBuy(Ticket ticket)
        {
            using(var db = new Context())
            {
                Ticket ticket1 = new Ticket()
                {
                    Name = ticket.Name,
                    SurName=ticket.SurName,
                    TelNo=ticket.TelNo,
                    TcNo=ticket.TcNo,
                    Description=ticket.Description,
                    Title=ticket.Title,
                    Price=ticket.Price,
                    SeatNo = ticket.SeatNo,               
                    CardName=ticket.CardName,
                    CardNumber=ticket.CardNumber,
                     Expeditions=ticket.Expeditions,
                     CVV=ticket.CVV,
                     ExpreditionMonth=ticket.ExpreditionMonth,
                     ExpreditionYear = ticket.ExpreditionYear,
                     ExpeditionId = ticket.ExpeditionId,
                     KullaniciID = ticket.KullaniciID,
                     Email=ticket.Email,
                      Gender=ticket.Gender,
                      PnrNo=ticket.PnrNo,
                      KalkisTime=ticket.KalkisTime,
                      Kalkis=ticket.Kalkis,
                      UyeId=ticket.UyeId,
                      Varis=ticket.Varis,
                };
                db.Tickets.Add(ticket1);
                db.SaveChanges();
                return  RedirectToAction("Index","Home");
            }
          
        }
    }
}
