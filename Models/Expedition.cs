using System;
using System.Collections.Generic;
using WebApplication.Identity;

namespace WebApplication.Models
{
    public class Expedition
    {
        public int Id { get; set; }

        public double Price { get; set; }

        public string Name { get; set; }

        public string Kalkis { get; set; }
        public string Varis { get; set; }
        public string PnrNo { get; set; }
        public DateTime KalkisTime { get; set; }
        public DateTime VarisTime { get; set; }

        public bool Plus { get; set; }

        public string UserId { get; set; }

        public List<User> Users { get; set; }

        public int Status { get; set; }

        public DateTime Created_at { get; set; }
        public DateTime Updated_at { get; set; }

    }
}
