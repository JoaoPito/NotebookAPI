using System.ComponentModel.DataAnnotations;

namespace NotebookAPI.Models;

public class Note
{
    [Required]
    [Key]
    public int Id { get; set; }
    [Required]
    public string? Title { get; set; }
    [Required]
    public DateTime LastModified { get; set; }
    [Required]
    public string? Directory { get; set; }
    public virtual ICollection<NoteTag> NoteTags { get; set; }
}