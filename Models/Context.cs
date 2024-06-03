using Microsoft.EntityFrameworkCore;

namespace WebApplication.Models
{
    public class Context:DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("server=DESKTOP-8UV1678;database=BiletDB;integrated security=true;");
        }

        public DbSet<Expedition> Expeditions { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<District> Districts { get; set; }
        public DbSet<Ticket> Tickets { get; set; }
    }
}
