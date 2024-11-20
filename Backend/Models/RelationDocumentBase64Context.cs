using Microsoft.EntityFrameworkCore;

namespace Backend.Models
{
    public class RelationDocumentBase64Context : DbContext
    {
        public RelationDocumentBase64Context(DbContextOptions<RelationDocumentBase64Context> options) : base(options) { }

        public DbSet<RelationDocumentBase64> Document_Base64 { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<RelationDocumentBase64>().ToTable("Document_Base64").HasKey(r => r.DocumentId);
            base.OnModelCreating(modelBuilder);
        }
    }
}