using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using NotebookAPI.Data;
using NotebookAPI.Data.DTOs;
using NotebookAPI.Models;

namespace NotebookAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class NoteController : ControllerBase
    {
        private NoteContext _context;
        private readonly IMapper _mapper;

        public NoteController(NoteContext context, IMapper mapper)
        {
            _mapper = mapper;
            _context = context;
        }

        [HttpPost]
        public async Task<IActionResult> AddNote([FromBody] CreateNoteDto noteDto)
        {
            var note = _mapper.Map<Note>(noteDto);
            note.LastModified = DateTime.Now;
            _context.Notes.Add(note);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetNote), new { Id= note.Id }, note);
        }

        [HttpGet]
        public IEnumerable<Note> GetNotes()
        {
            return _context.Notes;
        }

        [HttpGet("{id}")]
        public IActionResult GetNote(int id)
        {
            var note = _context.Notes.FirstOrDefault(note => note.Id == id);
            if (note == null)
                return NotFound();
            return Ok(note);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteNote(int id)
        {
            var note = _context.Notes.FirstOrDefault(note => note.Id == id);
            if(note == null) return NotFound();

            _context.Notes.Remove(note);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateNote(int id, [FromBody] Note note)
        {
            var oldNote = _context.Notes.FirstOrDefault(note => note.Id == id);
            if (oldNote == null) return NotFound();

            _context.Notes.Remove(oldNote);

            note.LastModified = DateTime.Now;
            _context.Notes.Add(note);
            await _context.SaveChangesAsync();

            return NoContent();
        }


    }
}