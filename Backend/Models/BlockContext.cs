using Backend.Models;
using Microsoft.EntityFrameworkCore;

public class BlockContext : DbContext
{
    public BlockContext(DbContextOptions<BlockContext> options) : base(options) { }

    public DbSet<Block> Blocks { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Block>().ToTable("Blocks");
        base.OnModelCreating(modelBuilder);
    }
}
