using System.ComponentModel.DataAnnotations;

namespace NotebookAPI.Data.DTOs
{
    public class CreateTagDto
    {
        [Required(ErrorMessage = "A name for the tag is required")]
        public string Name { get; set; } = "";
    }
}