using ConcertWebApi.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using System.Threading;

namespace ConcertWebApi.DAL
{
    public class DataBaseContext : DbContext
    {
        public DataBaseContext(DbContextOptions<DataBaseContext> options) : base(options)
        {

        }

        public DbSet<Ticket> Tickets { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Ticket>().HasIndex(t => t.EntranceGate).IsUnique();
        }
    }
}
