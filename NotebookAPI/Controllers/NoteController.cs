using Microsoft.AspNetCore.Mvc;
using NotebookAPI.Models;

namespace NotebookAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class NoteController : ControllerBase
    {
        private static List<Note> notes = new();

        public NoteController()
        {

        }

        [HttpPost]
        public IActionResult AddNote([FromBody] Note note)
        {
            notes.Add(note);
            return CreatedAtAction(nameof(GetNote), new { Id= note.Id }, note);
        }

        [HttpGet]
        public IEnumerable<Note> GetNotes()
        {
            return notes;
        }

        [HttpGet("{id}")]
        public IActionResult GetNote(int id)
        {
            var note = notes.FirstOrDefault(note => note.Id == id);
            if (note == null)
                return NotFound();
            return Ok(note);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteNote(int id)
        {
            var note = notes.FirstOrDefault(note => note.Id == id);
            if(note == null) return NotFound();

            notes.Remove(note);
            return NoContent();
        }

        [HttpPut("{id}")]
        public IActionResult UpdateNote(int id, [FromBody] Note note)
        {
            var oldNote = notes.FirstOrDefault(note => note.Id == id);
            if (oldNote == null) return NotFound();

            notes.Remove(oldNote);
            notes.Add(note);
            return NoContent();
        }


    }
}