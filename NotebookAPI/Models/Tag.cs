using System.ComponentModel.DataAnnotations;

namespace NotebookAPI.Models;

public class Tag
{
    [Required]
    [Key]
    public int Id { get; set; }
    [Required]
    public string Name { get; set; } = "";
    public virtual ICollection<NoteTag> NoteTags { get; set; }
}
