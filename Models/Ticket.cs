using System;
using System.Collections.Generic;
using WebApplication.Identity;

namespace WebApplication.Models
{
    public class Ticket
    {
        public Ticket()
        {
              BiletAlimTarihi=DateTime.Now;
        }
        public int Id { get; set; }
        public string Title { get; set; } = "Baki Arslan Bilet Bilgilendirmesi";
        public string Description { get; set; }
        public double  Price { get; set; }
        public int SeatNo { get; set; }
        public string Name { get; set; }
        public string SurName { get; set; }
        public string TelNo { get; set; }
        public string TcNo { get; set; }
        public string Email { get; set; }
        public bool Gender { get; set; }
        public string  KullaniciID { get; set; }
        public string CardNumber { get; set; }
        public string CardName { get; set; }
        public string ExpreditionMonth { get; set; }
        public string ExpreditionYear { get; set; }
        public string PnrNo { get; set; }
        public string Kalkis { get; set; }
        public string Varis { get; set; }
        public string UyeId { get; set; }
        public DateTime KalkisTime { get; set; }
        public DateTime BiletAlimTarihi { get; set; }
        public int CVV { get; set; }
        public User Users { get; set; }
        public int ExpeditionId { get; set; }
        public Expedition Expeditions { get; set; }
    }
}
