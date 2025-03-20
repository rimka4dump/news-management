using Microsoft.EntityFrameworkCore;
using news_management.Models;

namespace news_management.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext()
        {

        }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public virtual DbSet<News> News { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<News>()
                .Property(n => n.date)
                .HasDefaultValueSql("CONVERT(date, GETDATE())");
        }
    }
}