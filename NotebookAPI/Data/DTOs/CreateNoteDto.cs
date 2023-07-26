using System.ComponentModel.DataAnnotations;

namespace NotebookAPI.Data.DTOs
{
    public class CreateNoteDto
    {
        [Required(ErrorMessage = "The title field is required")]
        public string? Title { get; set; }
        [Required(ErrorMessage = "The directory field is required")]
        public string? Directory { get; set; }
    }
}