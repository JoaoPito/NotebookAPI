using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
    public async Task<IEnumerable<ReadNoteDto>> GetNotes([FromQuery] int page=0, [FromQuery] int step=25,
                                            [FromQuery] string searchTerm="",
                                            [FromQuery] int? withTag = null)
    {
        var notesList = await _context.Notes
            .Where(n => 
                n.Title.Contains(searchTerm) 
                && (withTag == null || _context.NoteTags.Contains(new NoteTag{NoteId=n.Id, TagId=withTag})))
            .Skip(page * step)
            .Take(step)
            .OrderBy(n => n.Ranking)
            .ToListAsync();

        var notesListDto = _mapper.Map<List<ReadNoteDto>>(notesList);
        return notesListDto;
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetNote(int id)
    {
        var note = _context.Notes.FirstOrDefault(note => note.Id == id);
        if (note == null)
            return NotFound();

        note.Ranking++;
        await _context.SaveChangesAsync();

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

    [HttpPatch("{id}")]
        public async Task<IActionResult> UpdateTagField(int id, [FromBody]JsonPatchDocument<UpdateNoteDto> patch)
        {
            var note = _context.Notes.FirstOrDefault(n => n.Id == id);
            if(note == null) return NotFound();

            var noteDto = _mapper.Map<UpdateNoteDto>(note);
            patch.ApplyTo(noteDto, ModelState);

            if(!TryValidateModel(noteDto))
                return ValidationProblem(ModelState);

            _mapper.Map(noteDto, note);
            note.LastModified = DateTime.Now;
            await _context.SaveChangesAsync();
            return NoContent();
        }
}