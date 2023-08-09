using NotebookAPI.Models;

namespace NotebookAPI.Data.DTOs;

public class ReadNoteDto
{
    public int Id { get; set; }
    public string? Title { get; set; }
    public DateTime LastModified { get; set; }
    public string? Directory { get; set; }
    public int Ranking { get; set; }
    public ICollection<ReadNoteTagDto> NoteTags { get; set; }
    
}