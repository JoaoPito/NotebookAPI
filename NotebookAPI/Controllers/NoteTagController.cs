using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
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
    public IActionResult GetAllNoteTags()
    {
        var noteTagsDto = _mapper.Map<List<ReadNoteTagDto>>(_context.NoteTags.ToList());
        return Ok(noteTagsDto);
    }

    [HttpGet("{noteId}/{tagId}")]
    public IActionResult GetNoteTagById(int noteId, int tagId)
    {
        var noteTag = _context.NoteTags.FirstOrDefault(nt => (nt.NoteId == noteId) && (nt.TagId == tagId));
        if(noteTag == null) return NotFound();

        var noteTagDto = _mapper.Map<ReadNoteTagDto>(noteTag);

        return Ok(noteTagDto);
    }

    [HttpPut("{noteId}/{tagId}")]
    public IActionResult UpdateNoteTag(int noteId, int tagId, [FromBody] UpdateNoteTagDto noteTagDto)
    {
        var noteTag = _context.NoteTags.FirstOrDefault(nt => nt.NoteId == noteId && nt.TagId == tagId);
        if(noteTag == null) return NotFound();

        _mapper.Map(noteTagDto, noteTag);
        _context.SaveChanges();

        return NoContent();
    }

    [HttpPatch("{noteId}/{tagId}")]
    public IActionResult UpdateNoteTagField(int noteId, int tagId, [FromBody] JsonPatchDocument patch)
    {
        var noteTag = _context.NoteTags.FirstOrDefault(nt => nt.NoteId == noteId && nt.TagId == tagId);
        if(noteTag == null) return NotFound();

        var noteTagDto = _mapper.Map<UpdateNoteTagDto>(noteTag);
        patch.ApplyTo(noteTagDto);

        if(!TryValidateModel(noteTagDto))
            return ValidationProblem(ModelState);

        _mapper.Map(noteTagDto, noteTag);
        _context.SaveChanges();

        return NoContent();
    }

    [HttpDelete("{noteId}/{tagId}")]
    public IActionResult DeleteNoteTag(int noteId, int tagId)
    {
        var noteTag = _context.NoteTags.FirstOrDefault(nt => nt.NoteId == noteId && nt.TagId == tagId);
        if(noteTag == null) return NotFound();

        _context.NoteTags.Remove(noteTag);
        _context.SaveChanges();

        return NoContent();
    }
}