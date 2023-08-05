using System.ComponentModel.DataAnnotations;

namespace NotebookAPI.Models;

public class Tag
{
    [Required]
    [Key]
    public int Id { get; set; }
    [Required]
    public string Name { get; set; }
    public List<Note> Notes { get; set; }
    public List<NoteTag> NoteTags { get; set; }
}
