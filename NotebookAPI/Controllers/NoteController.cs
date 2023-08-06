using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using NotebookAPI.Data;
using NotebookAPI.Data.DTOs;
using NotebookAPI.Models;

namespace NotebookAPI.Controllers;

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
        return CreatedAtAction(nameof(GetNote), new { Id = note.Id }, note);
    }

    [HttpGet]
    public IEnumerable<ReadNoteDto> GetNotes()
    {
        var notesListDto = _mapper.Map<List<ReadNoteDto>>(_context.Notes.ToList());
        return notesListDto;
    }

    [HttpGet("{id}")]
    public IActionResult GetNote(int id)
    {
        var note = _context.Notes.FirstOrDefault(note => note.Id == id);
        if (note == null)
            return NotFound();

        var noteDto = _mapper.Map<ReadNoteDto>(note);
        return Ok(noteDto);
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
    public async Task<IActionResult> UpdateNote(int id, [FromBody] UpdateNoteDto noteDto)
    {
        var note = _context.Notes.FirstOrDefault(note => note.Id == id);
        if (note == null) return NotFound();

        _mapper.Map(noteDto, note);
        note.LastModified = DateTime.Now;
        await _context.SaveChangesAsync();

        return NoContent();
    }
}