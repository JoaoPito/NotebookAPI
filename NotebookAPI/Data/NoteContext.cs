using Microsoft.EntityFrameworkCore;
using NotebookAPI.Models;

namespace NotebookAPI.Data;

public class NoteContext : DbContext
{
    public DbSet<Note> Notes { get; set; }
    public DbSet<Tag> Tags { get; set; }
    public DbSet<NoteTag> NoteTags { get; set; }

    public NoteContext(DbContextOptions opts) : base(opts)
    {

    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.Entity<NoteTag>().HasKey(noteTag => new { noteTag.NoteId, noteTag.TagId });

        builder.Entity<NoteTag>()
                .HasOne(noteTag => noteTag.Note)
                .WithMany(note => note.NoteTags)
                .HasForeignKey(noteTag => noteTag.NoteId);

        builder.Entity<NoteTag>()
                .HasOne(noteTag => noteTag.Tag)
                .WithMany(tag => tag.NoteTags)
                .HasForeignKey(noteTag => noteTag.TagId);
    }

    
}