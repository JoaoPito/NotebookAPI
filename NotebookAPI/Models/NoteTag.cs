using System.ComponentModel.DataAnnotations;

namespace NotebookAPI.Models;

public class NoteTag
{
    public int NoteId { get; set; }
    public int TagId { get; set; }
    public Note Note { get; set; }
    public Tag Tag { get; set; }
}
