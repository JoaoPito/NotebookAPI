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
public class NoteTagController : ControllerBase
{
        private readonly NoteContext _context;
        private readonly IMapper _mapper;
    public NoteTagController(NoteContext context, IMapper mapper)
    {
            this._mapper = mapper;
            this._context = context;
    }

    [HttpPost]
    public async Task<IActionResult> CreateNoteTag([FromBody] CreateNoteTagDto noteTagDto)
    {
        var noteTag = _mapper.Map<NoteTag>(noteTagDto);
        await _context.NoteTags.AddAsync(noteTag);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetNoteTagById), new { noteId = noteTag.NoteId, tagId = noteTag.TagId}, noteTag);
    }

    [HttpGet]
    public async Task<IActionResult> GetAllNoteTags()
    {
        var noteTags = await _context.NoteTags.ToListAsync();
        var noteTagsDto = _mapper.Map<List<ReadNoteTagDto>>(noteTags);
        return Ok(noteTagsDto);
    }

    [HttpGet("{noteId}/{tagId}")]
    public async Task<IActionResult> GetNoteTagById(int noteId, int tagId)
    {
        var noteTag = await _context.NoteTags.FirstOrDefaultAsync(nt => (nt.NoteId == noteId) && (nt.TagId == tagId));
        if(noteTag == null) return NotFound();

        var noteTagDto = _mapper.Map<ReadNoteTagDto>(noteTag);

        return Ok(noteTagDto);
    }

    [HttpPut("{noteId}/{tagId}")]
    public async Task<IActionResult> UpdateNoteTag(int noteId, int tagId, [FromBody] UpdateNoteTagDto noteTagDto)
    {
        var noteTag = await _context.NoteTags.FirstOrDefaultAsync(nt => nt.NoteId == noteId && nt.TagId == tagId);
        if(noteTag == null) return NotFound();

        _mapper.Map(noteTagDto, noteTag);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    [HttpPatch("{noteId}/{tagId}")]
    public async Task<IActionResult> UpdateNoteTagField(int noteId, int tagId, [FromBody] JsonPatchDocument patch)
    {
        var oldNoteTag = await _context.NoteTags.FirstOrDefaultAsync(nt => nt.NoteId == noteId && nt.TagId == tagId);
        if(oldNoteTag == null) return NotFound();

        var newNoteTagDto = _mapper.Map<UpdateNoteTagDto>(oldNoteTag);
        patch.ApplyTo(newNoteTagDto);

        if(!TryValidateModel(newNoteTagDto))
            return ValidationProblem(ModelState);

        var newNoteTag = _mapper.Map<NoteTag>(newNoteTagDto);

        _context.Remove(oldNoteTag);

        if(!_context.NoteTags.Contains(newNoteTag))
            await _context.AddAsync(newNoteTag);

        await _context.SaveChangesAsync();

        return NoContent();
    }

    [HttpDelete("{noteId}/{tagId}")]
    public async Task<IActionResult> DeleteNoteTag(int noteId, int tagId)
    {
        var noteTag = await _context.NoteTags.FirstOrDefaultAsync(nt => nt.NoteId == noteId && nt.TagId == tagId);
        if(noteTag == null) return NotFound();

        _context.NoteTags.Remove(noteTag);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}