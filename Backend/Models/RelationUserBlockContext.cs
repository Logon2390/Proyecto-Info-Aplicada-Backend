using Backend.Models;
using Microsoft.EntityFrameworkCore;

namespace Backend.Models
{
    public class RelationUserBlockContext : DbContext
    {
        public RelationUserBlockContext(DbContextOptions<RelationUserBlockContext> options) : base(options) { }

        public DbSet<RelationUserBlock> User_Block { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<RelationUserBlock>().ToTable("User_Block");
            modelBuilder.Entity<RelationUserBlock>().HasKey(r => new { r.UserId, r.BlockId });
            base.OnModelCreating(modelBuilder);
        }
    }
}
