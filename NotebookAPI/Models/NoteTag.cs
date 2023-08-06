namespace NotebookAPI.Models;

public class NoteTag
{
    public int? NoteId { get; set; }
    public virtual Note Note { get; set; }
    public int? TagId { get; set; }
    public virtual Tag Tag { get; set; }
}