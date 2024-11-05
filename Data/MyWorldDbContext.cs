using System.Security.Cryptography;
using InsertKph.Models;
using Microsoft.EntityFrameworkCore;

namespace InsertKph.Data 
{
    public class MyWorldDbContext : DbContext
    {
        public MyWorldDbContext(DbContextOptions<MyWorldDbContext> options):base(options)
        {

        }

        public DbSet<IpdProgressNote> IpdProgressNote {get; set;}
        public DbSet<IpdProgressNoteItem> IpdProgressNoteItems {get; set;}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Define composite key in IpdProgressNoteItem
            modelBuilder.Entity<IpdProgressNoteItem>()
                .HasKey(i => new { i.progress_note_id }); // Composite key

            // Configure the one-to-many relationship between IpdProgressNote and IpdProgressNoteItem
            modelBuilder.Entity<IpdProgressNote>()
                .HasMany(p => p.IpdProgressNoteItems)
                .WithOne(i => i.IpdProgressNote)
                .HasForeignKey(i => new { i.progress_note_id })  // Composite foreign key
                .OnDelete(DeleteBehavior.Cascade); // Set delete behavior (optional)

        // Optionally, configure indexes or other constraints if necessary
            modelBuilder.Entity<IpdProgressNote>()
                .HasKey(i => i.progress_note_id);
             // Optionally, configure an index on progress_note_id
            modelBuilder.Entity<IpdProgressNote>()
                .HasIndex(p => p.progress_note_id); // Index for efficient querying
        }
    }
}