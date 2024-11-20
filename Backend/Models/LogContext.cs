using Microsoft.EntityFrameworkCore;

namespace Backend.Models
{
    public class LogContext : DbContext
    {
        public LogContext(DbContextOptions<LogContext> options) : base(options)
        {
        }

        public DbSet<Log> Logs { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Log>().HasIndex(c => c.Id).IsUnique();
        }
    }
}
