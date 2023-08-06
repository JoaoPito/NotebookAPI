using NotebookAPI.Models;

namespace NotebookAPI.Data.DTOs;

public class ReadTagDto
{
    public int Id { get; set; }
    public string Name { get; set; } = "";
    public ICollection<ReadNoteTagDto> NoteTags { get; set; }
}