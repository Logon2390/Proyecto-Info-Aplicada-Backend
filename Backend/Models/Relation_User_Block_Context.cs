using Backend.Models;
using Microsoft.EntityFrameworkCore;

namespace Backend.Models
{
    public class Relation_User_Block_Context : DbContext
    {
        public Relation_User_Block_Context(DbContextOptions<Relation_User_Block_Context> options) : base(options) { }

        public DbSet<Relation_User_Block> User_Block { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Relation_User_Block>().ToTable("User_Block");
            modelBuilder.Entity<Relation_User_Block>().HasKey(r => new { r.UserId, r.BlockId });  // Clave primaria compuesta
            base.OnModelCreating(modelBuilder);
        }
    }
}
