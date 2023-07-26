using System.ComponentModel.DataAnnotations;

namespace NotebookAPI.Data.DTOs
{
    public class UpdateNoteDto
    {
        [Required(ErrorMessage = "The title field is required")]
        public string? Title { get; set; }
        [Required(ErrorMessage = "The directory field is required")]
        public string? Directory { get; set; }
    }
}