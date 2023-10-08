using Microsoft.EntityFrameworkCore;
using NotesApp.Models.DB.Entities;

namespace NotesApp.Models.DB
{
    public class NoteDbContext : DbContext
    {
        public DbSet<Note> Notes { get; set; }

        public NoteDbContext(DbContextOptions<NoteDbContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Note>(note => note.HasKey(property => property.Id));
            base.OnModelCreating(builder);
        }
    }
}
