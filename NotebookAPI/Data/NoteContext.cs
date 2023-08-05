using Microsoft.EntityFrameworkCore;
using NotebookAPI.Models;

namespace NotebookAPI.Data;

public class NoteContext : DbContext
{
    public DbSet<Note> Notes { get; set; }
    public DbSet<Tag> Tags { get; set; }

    public NoteContext(DbContextOptions opts) : base(opts)
    {

    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.Entity<Note>()
            .HasMany(note => note.Tags)
            .WithMany(tag => tag.Notes)
            .UsingEntity<NoteTag>(
                ntTag => ntTag.HasOne<Tag>(nt => nt.Tag)
                              .WithMany(t => t.NoteTags).HasForeignKey(nt => nt.TagId),
                ntNote => ntNote.HasOne<Note>(nt => nt.Note)
                                .WithMany(n => n.NoteTags).HasForeignKey(nt => nt.NoteId)
            );
    }

    
}