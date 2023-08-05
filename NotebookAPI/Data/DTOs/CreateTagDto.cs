using System.ComponentModel.DataAnnotations;

namespace NotebookAPI.Data.DTOs
{
    public class CreateTagDto
    {
        [Required]
        public string Name { get; set; }
    }
}