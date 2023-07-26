using Microsoft.EntityFrameworkCore;
using NotebookAPI.Models;

namespace NotebookAPI.Data;

public class NoteContext : DbContext
{
    public NoteContext(DbContextOptions opts) : base(opts)
    {

    }
    public DbSet<Note> Notes { get; set; }
}